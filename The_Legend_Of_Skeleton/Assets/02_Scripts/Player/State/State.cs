using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerStateMachine m_PSM = null;
    protected int m_ActionIdx = 0;
    protected float m_Timer = 0f;

    protected static bool mb_IsInputEvade = false;
    protected static bool mb_IsInputJump = false;
    protected static bool mb_IsInputWasd = false;

    public State(PlayerStateMachine _PSM, int _actionIdx)
    {
        m_PSM = _PSM;
        m_ActionIdx = _actionIdx;
    }
    public virtual void EnterState()
    {
        m_Timer = 0f;
    }
    public virtual void SetInputEvade(bool _isInputWasd)  // Input이 들어오면 true
    {
        mb_IsInputEvade = true;
        mb_IsInputWasd = _isInputWasd;
    }
    public virtual void SetInputJump()
    {
        mb_IsInputJump = true;
    }
  
    public abstract void ExitState();
    public virtual void CheckState()
    {
        CheckFinishActionTime();
        CheckEvade();
        CheckJump();
    }
    protected virtual void CheckEvade()
    {
        // 넉다운, 넉백, 점프중에는 회피불가능
        if (m_PSM.IsKnockdown()) return;
        if (m_PSM.IsKnockback()) return;
        if (m_PSM.IsJump()) return;
        if (m_PSM.IsEvade()) return;

        // 회피입력이 들어오고 방향키 입력이 들어왔다면, 스테미너가 충분하고 현재상태가 회피중, 점프중이 아니면
        if (mb_IsInputEvade && mb_IsInputWasd && m_PSM.IsSteminaEnough())
        {
            m_PSM.SetState(m_PSM.Evade);
            return;
        }
    }
    protected virtual void CheckJump()
    {
        if (m_PSM.IsKnockdown()) return;
        if (m_PSM.IsKnockback()) return;
        if (m_PSM.IsJump()) return;
        if (m_PSM.IsEvade()) return;

        if (mb_IsInputJump)
        {
            m_PSM.SetState(m_PSM.Jump);
            return;
        }
    }
    protected virtual void CheckFinishActionTime()
    {
        m_Timer += Time.deltaTime;

        // 액션타임이 지났고 현재상태가 노말상태가 아니라면
        if (m_Timer >= m_PSM.GetActionFinishTime() && m_PSM.IsNotNomalState())
        {
            m_PSM.SetState(m_PSM.NomalState);
            return;
        }
    }
}

public class Evade : State
{
    public Evade(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter Evade");
        m_PSM.EnterEvade();
        mb_IsInputEvade = false;
        mb_IsInputWasd = false;
        base.EnterState();
    }
    public override void ExitState()
    {
    }
    protected override void CheckFinishActionTime()
    {
        m_Timer += Time.deltaTime;
        // 회피모션이 끝났다면
        if (m_Timer >= 0.8f)
        {
            m_PSM.SetState(m_PSM.NomalState);
            return;
        }
    }
}

public class Jump : State
{
    public Jump(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter Jump");
        mb_IsInputJump = false;
        base.EnterState();
        m_PSM.EnterJump();
    }
    public override void ExitState()
    {
    }
    protected override void CheckFinishActionTime()
    {
        m_Timer += Time.deltaTime;
        // 회피모션이 끝났다면
        if (m_Timer >= 0.68f)
        {
            m_PSM.SetState(m_PSM.NomalState);
            return;
        }
    }
}

public class Hit : State
{
    public Hit(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter Hit");
        base.EnterState();
    }
    public override void ExitState()
    {
    }
}

public class Knockback : State
{
    public Knockback(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter Knockback");
        base.EnterState();
    }
    public override void ExitState()
    {
    }
}

public class Knockdown : State
{
    public Knockdown(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter KnockDown");
        base.EnterState();
        m_PSM.EnterKnockdown();
    }
    public override void ExitState()
    {
        m_PSM.ExitKnockdown();
    }
}

public class ElementBurst : State
{
    public ElementBurst(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter ElementBurst!");
        base.EnterState();
        m_PSM.EnterElementBurst();
    }
    public override void ExitState()
    {
    }
}


