using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_SoulEater : BossState
{
    public Idle_SoulEater(Boss _boss) : base("Idle", _boss) { }
    [SerializeField] private float m_FightDistance = 30f;//상위 프로퍼티로 올리자

    public override void EnterState()
    {
        Debug.Log("Idle");
        //m_Boss.Anim.SetBool("isIdle", true);
    }
    public override void CheckState()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        if (distance <= m_FightDistance)//플레이어랑 거리가 30미만이면 Trace
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Trace);
        }
    }

    public override void Action()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        if (distance >= m_FightDistance && (m_Boss as Boss_SoulEater).CurrentHp <= (m_Boss as Boss_SoulEater).MaxHp)//전투 중에 보스지역 벗어나면 보스 제자리가고 체력참
        {
            (m_Boss as Boss_SoulEater).CurrentHp += Time.deltaTime;
        }
        if ((m_Boss as Boss_SoulEater).transform.position != (m_Boss as Boss_SoulEater).BossRespawn.position)
        {
            m_Boss.Agent.destination = (m_Boss as Boss_SoulEater).BossRespawn.position;
        }
    

    }

    public override void ExitState()
    {

    }

}
