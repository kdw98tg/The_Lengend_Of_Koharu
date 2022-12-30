using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace_SoulEater : BossState
{
    public Trace_SoulEater(Boss _boss) : base("Trace", _boss) { }

    public override void EnterState()
    {
        if ((m_Boss as Boss_SoulEater).Slept == true)
        {
            Debug.Log("Range Trace");
            m_Boss.Anim.SetBool("isRun", true);
            m_Boss.Agent.isStopped = false;
            m_Boss.Agent.speed = 5f;
        }
        else
        {
            Debug.Log("Trace");
            m_Boss.Anim.SetBool("isWalk", true);
            m_Boss.Agent.isStopped = false;
            //m_Boss.Agent.stoppingDistance = 10f;
        }
    }
    public override void Action()
    {
        //왜 바로 안바뀌지
        m_Boss.Agent.destination = m_Boss.PlayerTr.position;//player의 포지션을 쫓아감
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);


    }

    public override void CheckState()
    {
        //Debug.Log("efse" + (m_Boss.CurrentHp / m_Boss.MaxHp));
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position); //플레이어와 거리를 재서 10 미만이면 공격모드로 변경
        if (distance < 15f)
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Attack);
        }
        else if (distance >= 30f)
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Idle);
        }


        if (m_Boss.CurrentHp / m_Boss.MaxHp < 0.3f && (m_Boss as Boss_SoulEater).Slept == false)//한번 잔 상태면 Sleep상태로 안감, 플레이어 사망후 재도전시 설정 필요할듯
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Sleep);
        }
    }

    public override void ExitState()
    {
        if ((m_Boss as Boss_SoulEater).Slept == true)
        {
            m_Boss.Anim.SetBool("isRun", false);
        }
        else
        {
            Debug.Log("이거도 찍히냐?");
            m_Boss.Anim.SetBool("isWalk", false);
        }
    }
}
