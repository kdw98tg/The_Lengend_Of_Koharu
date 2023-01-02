using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_TerrorBringer : BossState
{
    public Idle_TerrorBringer(Boss _boss) : base("Idle" , _boss) { }
    public override void EnterState()
    {
        Debug.Log("Enter Idle");
    }
    public override void Action()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        if(distance >= (m_Boss as Boss_TerrorBringer).IdleRange && (m_Boss as Boss_TerrorBringer).CurrentHp <= (m_Boss as Boss_TerrorBringer).MaxHp)
            //���ظ� ���� �����̰� Idle ���� ������ ������� ü�� ȸ��
        {
            (m_Boss as Boss_TerrorBringer).CurrentHp += Time.deltaTime;
        }
        if (distance >= (m_Boss as Boss_TerrorBringer).IdleRange && (m_Boss as Boss_TerrorBringer).transform.position != (m_Boss as Boss_TerrorBringer).BossRespawn.position)
        {
            m_Boss.Agent.destination = (m_Boss as Boss_TerrorBringer).BossRespawn.position;
        }
    }

    public override void CheckState()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        if(distance <= (m_Boss as Boss_TerrorBringer).IdleRange)//distance�� IdleRange���� ũ��
        {
            m_Boss.SetState((m_Boss as Boss_TerrorBringer).Trace);
        }
    }


    public override void ExitState()
    {
        Debug.Log("Exit Idle");
    }
}
