using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : State
{
    protected static bool mb_IsNextAttackInput = false;
    protected static bool mb_IsInputSmash = false;
    protected static bool mb_IsChanging = false;

    public AttackState(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public void IsNextAttackInput()
    {
        mb_IsNextAttackInput = true;
    }
    public void IsSmashInput()
    {
        mb_IsInputSmash = true;
    }
    public override void ExitState()
    {
        mb_IsNextAttackInput = false;
        mb_IsInputSmash = false;
        mb_IsChanging = false;
    }
    public virtual void CheckNextAction(State _nextAttack, State _nextSmash)
    {
        // ���������� �� �ִ� �ð� ���� ~ �ִϸ��̼� ���� ����
        if (m_Timer >= m_PSM.GetCanNextActionTime())
        {
            // �����Է��� ���԰�, ���� �׼��� �غ����� �ƴ϶��
            if (mb_IsNextAttackInput && !mb_IsChanging)
            {
                mb_IsNextAttackInput = false;
                mb_IsChanging = true;
                m_PSM.SetState(_nextAttack);
            }

            // ���Ž��Է��� ���԰�, ���� �׼��� �غ����� �ƴ϶��
            else if (mb_IsInputSmash && !mb_IsChanging)
            {
                mb_IsInputSmash = false;
                mb_IsChanging = true;
                m_PSM.SetState(_nextSmash);
            }
        }
    }
}

public class NomalState : AttackState
{
    public NomalState(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter NomalState");
        m_PSM.EnterNomalState();
        base.EnterState();
    }
    
    public override void CheckState()
    {
        base.CheckState();

        // �����Է��� ���´ٸ�
        if (mb_IsNextAttackInput)
        {
            m_PSM.SetState(m_PSM.FirstAttack);

            mb_IsNextAttackInput = false;
            return;
        }
    }
}

public class FirstAttack : AttackState
{
    public FirstAttack(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter First Attack");
        m_PSM.EnterComboAttack();
        base.EnterState();
    }

    public override void CheckState()
    {
        base.CheckState();
        CheckNextAction(m_PSM.SecondAttack, m_PSM.FirstSmash);
    }
}

public class SecondAttack : AttackState
{
    public SecondAttack(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter Second Attack");
        m_PSM.EnterNextAttack();
        base.EnterState();
    }

    public override void CheckState()
    {
        base.CheckState();
        CheckNextAction(m_PSM.ThirdAttack, m_PSM.SecondSmash);
    }
}

public class ThirdAttack : AttackState
{
    public ThirdAttack(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter Third State");
        m_PSM.EnterNextAttack();
        base.EnterState();
    }

    public override void CheckState()
    {
        base.CheckState();
        CheckNextAction(m_PSM.FourthAttack, m_PSM.ThirdSmash);
    }
}

public class FourthAttack : AttackState
{
    public FourthAttack(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter Fourth Attack");
        m_PSM.EnterNextAttack();
        base.EnterState();
    }
 
    public override void CheckState()
    {
        base.CheckState();
        // ���������� �� �ִ� �ð� ���� ~ ����Ÿ�� ���� 
        if (m_Timer >= m_PSM.GetCanNextActionTime()) 
        {
            // ���Ž��Է��� ���Դٸ�
            if (mb_IsInputSmash)
            {
                m_PSM.SetState(m_PSM.FourthSmash);
                mb_IsInputSmash = false;
            }
        }
    }
}
