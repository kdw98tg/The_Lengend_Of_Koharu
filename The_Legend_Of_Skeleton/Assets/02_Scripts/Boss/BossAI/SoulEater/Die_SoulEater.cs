using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Die_SoulEater : BossState
{
    public Die_SoulEater(Boss _boss) : base("Boss", _boss) { }
    public override void Action()
    {
        (m_Boss as Boss_SoulEater).DeadAndDestroy();
    }
    public override void CheckState()
    {
        
    }

    public override void EnterState()
    {
        Debug.Log("Enter Die");
        m_Boss.Anim.SetTrigger("isDie");
        m_Boss.Agent.isStopped = true;
    }

    public override void ExitState()
    {

    }
}
