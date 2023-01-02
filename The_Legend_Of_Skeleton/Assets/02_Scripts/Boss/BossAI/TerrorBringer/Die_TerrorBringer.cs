using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Die_TerrorBringer : BossState
{
    private bool mb_isDie = false;
    public Die_TerrorBringer(Boss _boss) : base("Die", _boss) { }
    public override void EnterState()
    {
        m_Boss.Agent.isStopped = true;
        Debug.Log("Enter Die");
        m_Boss.Anim.SetBool("isDie", true);
    }
    public override void Action()
    {
        if (mb_isDie == true)
            return;

        (m_Boss as Boss_TerrorBringer).StartCoroutine((m_Boss as Boss_TerrorBringer).DieCoroutine(5f));//5ÃÊÈÄ ÆÄ±«

    }


    public override void CheckState()
    {

    }


    public override void ExitState()
    {
    }
}
