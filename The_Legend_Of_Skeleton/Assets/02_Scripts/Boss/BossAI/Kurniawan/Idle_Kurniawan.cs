using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle_Kurniawan : BossState
{
    public Idle_Kurniawan(Boss _boss) : base("Idle", _boss){}
    private Vector3 respawnPos; // 리스폰위치
    private Vector3 curentPos; // 현재위치
    
    public override void EnterState()
    {
        Debug.Log($"{m_Boss.name} Idle 입장"); // 디버그 확인
        m_Boss.Anim.SetBool("IsIdle", true); // Idle 애니 On
        
        m_Boss.Agent.speed = m_Boss.IdleSpeed;
        respawnPos = m_Boss.BossRespawn.position;
        
        (m_Boss as Boss_Kurniawan).EnterIdleCallback();
    }

    public override void ExitState()
    {
        Debug.Log($"{m_Boss.name} Idle 퇴장");
        m_Boss.Anim.SetBool("IsIdle", false);
    }

    public override void Action()
    {
        if (curentPos != respawnPos)
        {
            m_Boss.Agent.destination = respawnPos;
        }
    }

    public override void CheckState()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
      
        if (distance <= (m_Boss as Boss_Kurniawan).TraceDetectRange)
        {
            m_Boss.SetState((m_Boss as Boss_Kurniawan).Trace);
            return;
        }
    }
  
}
