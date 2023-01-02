using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace_Kurniawan : BossState
{
    public Trace_Kurniawan(Boss _boss) : base("Trace", _boss){}

    private Vector3 m_PlayerPos = Vector3.zero;
    private bool isLookPlayer = false;
    

    public override void EnterState()
    {
        Debug.Log($"{m_Boss.name} TracePlayer 입장");
        m_Boss.Anim.SetBool("IsTrace", true);
        m_Boss.Agent.speed = m_Boss.TraceSpeed;
        
        m_Boss.Agent.stoppingDistance = 2.5f;

        (m_Boss as Boss_Kurniawan).EnterTraceCallback();
    }

    public override void ExitState()
    {
        Debug.Log($"{m_Boss.name} TracePlayer 퇴장");
        m_Boss.Anim.SetBool("IsTrace", false);
        
        m_Boss.Agent.stoppingDistance = 0.1f;

        (m_Boss as Boss_Kurniawan).ExitTraceCallback();
    }

    public override void Action()
    {
       
        RaycastHit hitInfo;
        
        //int layerMask = ((m_Boss as Boss_Kurniawan).FOV.PlayerLayer | (m_Boss as Boss_Kurniawan).FOV.ObstacleLayer);
        Debug.DrawLine(m_Boss.transform.position, m_Boss.PlayerTr.position, Color.blue);
        
        Vector3 direction = m_Boss.PlayerTr.position - m_Boss.transform.position;
        
        if(Physics.Raycast(m_Boss.transform.position , direction,
               out hitInfo, (m_Boss as Boss_Kurniawan).TraceDetectRange)) //(보스위치, 플레이어 에서 보스로 보는 방향벡터, out 맞은 플레이어 ,범위는 traceDetectRange)
        {
            isLookPlayer = true;
            m_PlayerPos = hitInfo.transform.position;
        }
        else
        {
            isLookPlayer = false;
        }
        m_Boss.Agent.destination = m_PlayerPos;
    }

    public override void CheckState()
    {
        if (isLookPlayer)
        {
            float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);

            if (distance <= (m_Boss as Boss_Kurniawan).AttackRange)
            {
                m_Boss.SetState((m_Boss as Boss_Kurniawan).Attack);
                return;
            }
        }
        else
        {
            float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);

            if (distance > (m_Boss as Boss_Kurniawan).TraceDetectRange)
            {
                m_Boss.SetState((m_Boss as Boss_Kurniawan).Idle);
                return;
            }
        }
       
    }
}
