using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace_TerrorBringer : BossState
{
    public Trace_TerrorBringer(Boss _boss) : base("Trace", _boss) { }
    public override void EnterState()
    {
        Debug.Log("Enter Trace");

        (m_Boss as Boss_TerrorBringer).NormalTrace();

    }
    public override void Action()
    {
        m_Boss.Agent.destination = m_Boss.PlayerTr.position;
    }

    public override void CheckState()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        if (distance < (m_Boss as Boss_TerrorBringer).EnterAttackRange)//공격사거리 안이라면 공격상태
        {
            m_Boss.SetState((m_Boss as Boss_TerrorBringer).Attack);
        }
        else if (distance >= (m_Boss as Boss_TerrorBringer).IdleRange)//IdleRange 밖이라면 Idel 상태
        {
            Debug.Log("Idle돌입가자");
            m_Boss.SetState((m_Boss as Boss_TerrorBringer).Idle);
        }
        else if (m_Boss.CurrentHp <= 0f)//체력이 0이라면 죽은상태
        {
            Debug.Log("TerrorBringer Die");
            m_Boss.SetState((m_Boss as Boss_TerrorBringer).Die);
        }

    }

    public override void ExitState()
    {
        Debug.Log("Exit Trace");
        m_Boss.Anim.SetBool("isWalk", false);

    }
}
