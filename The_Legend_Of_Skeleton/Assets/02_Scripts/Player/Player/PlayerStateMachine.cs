using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    #region Delegate
    public delegate void VoidFloatFloatDelegate(float _vlaue1, float _value2);
    public delegate void VoidVector3Delegate(Vector3 _value);
    public delegate void VoidVoidDelegate();
    public delegate bool BoolVoidDelegate();
    public delegate float floatVoidDelegate();
    private VoidVoidDelegate enterEvadeDelegate = null;
    private VoidVoidDelegate enterJumpDelegate = null;
    private VoidVoidDelegate enterNomalStateDelegate = null;
    private VoidVoidDelegate enterComboAttackDelegate = null;
    private VoidVoidDelegate enterNextAttackDelegate = null;
    private VoidVoidDelegate enterSmashDelegate = null;
    private VoidVoidDelegate enterElementBurstDelegate = null;
    private VoidVoidDelegate enterKnockdownDelegate = null;
    private VoidVoidDelegate exitKnockdownDelegate = null;
    private VoidVector3Delegate setKnockdownDelegate = null;
    private BoolVoidDelegate isSteminaEnoughDelegate = null;
    private floatVoidDelegate getCurAnimTimeDelegate = null;
    private floatVoidDelegate getCanNextActionTimeDelegate = null;
    #endregion

    #region State
    private State m_CurState = null;
    public State CurState
    { get { return m_CurState; } }

    private NomalState m_NomalState = null;
    public NomalState NomalState
    { get { return m_NomalState; } }

    private FirstAttack m_FirstAttack = null;
    public FirstAttack FirstAttack
    { get { return m_FirstAttack; } }

    private SecondAttack m_SecondAttack = null;
    public SecondAttack SecondAttack
    { get { return m_SecondAttack; } }

    private ThirdAttack m_ThirdAttack = null;
    public ThirdAttack ThirdAttack
    { get { return m_ThirdAttack; } }

    private FourthAttack m_FourthAttack = null;
    public FourthAttack FourthAttack
    { get { return m_FourthAttack; } }

    private FirstSmash m_FirstSmash = null;
    public FirstSmash FirstSmash
    { get { return m_FirstSmash; } }

    private SecondSmash m_SecondSmash = null;
    public SecondSmash SecondSmash
    { get { return m_SecondSmash; } }

    private ThirdSmash m_ThirdSmash = null;
    public ThirdSmash ThirdSmash
    { get { return m_ThirdSmash; } }

    private FourthSmash m_FourthSmash = null;
    public FourthSmash FourthSmash
    { get { return m_FourthSmash; } }

    private ElementBurst m_ElementBurst = null;
    public ElementBurst ElementBurst
    { get { return m_ElementBurst; } }

    private Evade m_Evade = null;
    public Evade Evade
    { get { return m_Evade; } }

    private Jump m_Jump = null;
    public Jump Jump
    { get { return m_Jump; } }

    private Knockback m_Knockback = null;
    public Knockback Knockback
    { get { return m_Knockback; } }

    private Knockdown m_Knockdown = null;
    public Knockdown Knockdown
    { get { return m_Knockdown; } }
    #endregion


    private void Awake()
    {
        m_NomalState = new NomalState(this, 0);
        m_FirstAttack = new FirstAttack(this, 1);
        m_SecondAttack = new SecondAttack(this, 2);
        m_ThirdAttack = new ThirdAttack(this, 3);
        m_FourthAttack = new FourthAttack(this, 4);

        m_FirstSmash = new FirstSmash(this, 5);
        m_SecondSmash = new SecondSmash(this, 6);
        m_ThirdSmash = new ThirdSmash(this, 7);
        m_FourthSmash = new FourthSmash(this, 8);

        m_ElementBurst = new ElementBurst(this, 9);
        m_Evade = new Evade(this, 10);
        m_Jump = new Jump(this, 11);
        m_Knockback = new Knockback(this, 12);
        m_Knockdown = new Knockdown(this, 13);

        SetState(m_NomalState);
    }

    private void Update()
    {
        m_CurState.CheckState();
    }

    #region SetState
    public void SetState(State _state)
    {
        if (m_CurState != null)
        {
            m_CurState.ExitState();
        }

        m_CurState = _state;
        m_CurState.EnterState();
    }

   

    // 현재상태가 일반공격중이면 좌클, 우클 입력을 받아서 다음 상태로 연계한다.
    public void InputLeftClickCallback()
    {
        if ((m_CurState as AttackState) == null) return;

        (m_CurState as AttackState).IsNextAttackInput();
    }

    public void InputRightClickCallback()
    {
        if ((m_CurState as AttackState) == null) return;

        (m_CurState as AttackState).IsSmashInput();
    }

    // 현재상태가 스매시중이면 E 입력받아서 ElementBurst로 연계한다.
    public void InputElementBurstCallback()
    {
        if ((m_CurState as SmashState) == null) return;

        (m_CurState as SmashState).CheckElementBurst();
    }

    public void InputEvadeCallback(bool _isInputWasd)
    {
        if (m_CurState != m_Evade)
        {
            m_CurState.SetInputEvade(_isInputWasd);
        }
    }

    public void InputJumpCallback()
    {
        if (m_CurState != m_Jump)
        {
            m_CurState.SetInputJump();
        }
    }

    public void SetKnockback()
    {
        if(m_CurState == Jump)
        {
            SetState(m_Knockdown);
            setKnockdownDelegate?.Invoke(Vector3.zero);
        }
        else if (m_CurState != m_Knockback)
        {
            SetState(m_Knockback);
        }
    }

    public void SetKnockdown()
    {
        if(m_CurState != m_Knockdown)
        {
            SetState(m_Knockdown);
        }
    }

    public void EnterNomalState()
    {
        enterNomalStateDelegate?.Invoke();
    }

    public void EnterComboAttack()
    {
        enterComboAttackDelegate?.Invoke();
    }

    public void EnterNextAttack()
    {
        enterNextAttackDelegate?.Invoke();
    }

    public void EnterSmash()
    {
        enterSmashDelegate?.Invoke();
    }

    public void EnterElementBurst()
    {
        enterElementBurstDelegate?.Invoke();
    }

    public void EnterEvade()
    {
        enterEvadeDelegate?.Invoke();
    }

    public void EnterJump()
    {
        enterJumpDelegate?.Invoke();
    }
  
    public void EnterKnockdown()
    {
        enterKnockdownDelegate?.Invoke();
    }
    public void ExitKnockdown()
    {
        exitKnockdownDelegate?.Invoke();
    }

    public bool IsNotNomalState()
    {
        if (m_CurState != m_NomalState) return true;
        else return false;
    }

    public bool IsEvade()
    {
        if (m_CurState == m_Evade) return true;
        else return false;
    }

    public bool IsJump()
    {
        if (m_CurState == m_Jump) return true;
        else return false;
    }

    public bool IsKnockback()
    {
        if (m_CurState == m_Knockback) return true;
        else return false;
    }

    public bool IsKnockdown()
    {
        if (m_CurState == m_Knockdown) return true;
        else return false;
    }

    public bool IsSteminaEnough()
    {
        bool isSteminaEnough = false;
        if (isSteminaEnoughDelegate != null)
        {
            isSteminaEnough = isSteminaEnoughDelegate();
        }
        return isSteminaEnough;
    }

    #endregion








    #region ActionTime
    public float GetActionFinishTime()
    {
        // State 종료시간 : 현재 액션의 애니메이션 시간
        return getCurAnimTimeDelegate();
    }

    public float GetCanNextActionTime()
    {
        float canNextActionTime = 0f;
        canNextActionTime = getCanNextActionTimeDelegate();
        return canNextActionTime;
    }
    #endregion

    #region Delegate
    // Player에서 설정
    public void Init(VoidVoidDelegate _enterEvadeCallback, VoidVoidDelegate _enterJumpCallback,
                     VoidVoidDelegate _enterComboAttackCallback, VoidVoidDelegate _enterNomalStateCallback,
                     VoidVoidDelegate _enterNextAttackCallback, VoidVoidDelegate _enterSmashCallback,
                     VoidVoidDelegate _enterElementBurstCallback, VoidVoidDelegate _enterKnockdownCallback,
                     VoidVoidDelegate _exitKnockdownCallback, VoidVector3Delegate _setKnockdownCallback,
                     BoolVoidDelegate _isSteminaEnough,
                     floatVoidDelegate _getCurAnimTime, floatVoidDelegate _getCanNextActionTime)
    {
        enterEvadeDelegate = _enterEvadeCallback;
        enterJumpDelegate = _enterJumpCallback;
        enterNomalStateDelegate = _enterNomalStateCallback;
        enterComboAttackDelegate = _enterComboAttackCallback;
        enterNextAttackDelegate = _enterNextAttackCallback;
        enterSmashDelegate = _enterSmashCallback;
        enterElementBurstDelegate = _enterElementBurstCallback;
        enterKnockdownDelegate = _enterKnockdownCallback;
        exitKnockdownDelegate = _exitKnockdownCallback;
        setKnockdownDelegate = _setKnockdownCallback;
        isSteminaEnoughDelegate = _isSteminaEnough;
        getCurAnimTimeDelegate = _getCurAnimTime;
        getCanNextActionTimeDelegate = _getCanNextActionTime;
    }
    #endregion
}

