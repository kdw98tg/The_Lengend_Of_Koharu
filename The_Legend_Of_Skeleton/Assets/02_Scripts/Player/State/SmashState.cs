using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmashState : State
{
    protected static bool mb_IsInputElementBurst = false;
    protected static bool mb_IsChanging = false;

    public SmashState(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }
    public override void ExitState()
    {
        mb_IsInputElementBurst = false;
        mb_IsChanging = false;
    }
    public void CheckSmashState()
    {
        if (m_Timer >= m_PSM.GetCanNextActionTime())
        {
            if (mb_IsInputElementBurst && !mb_IsChanging)
            {
                mb_IsInputElementBurst = false;
                mb_IsChanging = true;
                m_PSM.SetState(m_PSM.ElementBurst);
            }
        }
    }
    public void CheckElementBurst()
    {
        mb_IsInputElementBurst = true;
    }
}



public class FirstSmash : SmashState
{
    public FirstSmash(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }

    public override void EnterState()
    {
        Debug.Log("Enter First Smash");
        m_PSM.EnterSmash();
        base.EnterState();
    }
    
    public override void CheckState()
    {
        base.CheckState();
        CheckSmashState();
    }
}

public class SecondSmash : SmashState
{
    public SecondSmash(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }
    public override void EnterState()
    {
        Debug.Log("Enter Second Smash");
        m_PSM.EnterSmash();
        base.EnterState();
    }
   
    public override void CheckState()
    {
        base.CheckState();
        CheckSmashState();
    }
}
public class ThirdSmash : SmashState
{
    public ThirdSmash(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }
    public override void EnterState()
    {
        Debug.Log("Enter Third Smash");
        m_PSM.EnterSmash();
        base.EnterState();
    }
    
    public override void CheckState()
    {
        base.CheckState();
        CheckSmashState();
    }
}

public class FourthSmash : SmashState
{
    public FourthSmash(PlayerStateMachine _PSM, int _actionIdx) : base(_PSM, _actionIdx) { }
    public override void EnterState()
    {
        Debug.Log("Enter Fourth Smash");
        m_PSM.EnterSmash();
        base.EnterState();
        //m_PlayerAction.SetOnElementBurst();
    }
    
    public override void CheckState()
    {
        base.CheckState();
        CheckSmashState();
    }
}