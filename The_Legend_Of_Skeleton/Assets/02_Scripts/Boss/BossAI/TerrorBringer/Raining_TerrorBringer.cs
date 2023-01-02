using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raining_TerrorBringer : BossState
{
    public Raining_TerrorBringer(Boss _boss) : base("Raining", _boss) { }
    private bool mb_raining = false;
    public override void EnterState()
    {
        
        Debug.Log("Enter Raining");
        m_Boss.Anim.SetBool("isRaining", true);
        m_Boss.Agent.isStopped = true;
    }
    public override void Action()
    {
        
        if (mb_raining == true)
        {
            (m_Boss as Boss_TerrorBringer).RainingTime += Time.deltaTime;
            Debug.Log("rainingTime" + (m_Boss as Boss_TerrorBringer).RainingTime);
            return;
        }//랜덤위치에 산성비 소환
        Debug.Log("Is Raining");
        (m_Boss as Boss_TerrorBringer).StartCoroutine((m_Boss as Boss_TerrorBringer).RainingCoroutine());
        mb_raining = true;
    }
    public override void CheckState()
    {
        if ((m_Boss as Boss_TerrorBringer).RainingTime > (m_Boss as Boss_TerrorBringer).MaxRainingTime)
        {
            m_Boss.SetState((m_Boss as Boss_TerrorBringer).Trace);
        }
        else if ((m_Boss as Boss_TerrorBringer).CurrentHp <= 0f)
        {
            m_Boss.SetState((m_Boss as Boss_TerrorBringer).Die);
            return;
        }
    }
    public override void ExitState()
    {
        Debug.Log("Exit Raining");
        m_Boss.Anim.SetBool("isRaining", false);
        (m_Boss as Boss_TerrorBringer).IsRaining = true;
        m_Boss.Agent.isStopped = false;
    }
}
