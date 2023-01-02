using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace_SoulEater : BossState
{
    public Trace_SoulEater(Boss _boss) : base("Trace", _boss) { }

    public override void EnterState()
    {
       ( m_Boss as Boss_SoulEater).SetTraceMode();
    }
    public override void Action()
    {
        m_Boss.Agent.destination = m_Boss.PlayerTr.position;//player의 포지션을 쫓아감
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
        else if (m_Boss.CurrentHp / m_Boss.MaxHp < 0.3f && (m_Boss as Boss_SoulEater).Slept == false )//한번 잔 상태면 Sleep상태로 안감, 플레이어 사망후 재도전시 설정 필요할듯
        {
            Debug.Log("SleepAgain");
            m_Boss.SetState((m_Boss as Boss_SoulEater).Sleep);
        }

        //죽은상태 분기
        if (m_Boss.CurrentHp <= 0f)
        {
            Debug.Log("SoulEater Die");
            m_Boss.SetState((m_Boss as Boss_SoulEater).Die);
        }

        //Damage 분기문
        //if(!(m_Boss as Boss_SoulEater).FirstDamaged && m_Boss.CurrentHp/ m_Boss.MaxHp < 0.7f)
        //{
        //    Debug.Log("firstDamage");
        //    m_Boss.SetState((m_Boss as Boss_SoulEater).Damaged);
        //}
        //else if (!(m_Boss as Boss_SoulEater).FirstDamaged && m_Boss.CurrentHp / m_Boss.MaxHp < 0.5f)
        //{
        //    Debug.Log("SecondDamage");
        //    m_Boss.SetState((m_Boss as Boss_SoulEater).Damaged);
        //}
        //else if (!(m_Boss as Boss_SoulEater).FirstDamaged && m_Boss.CurrentHp / m_Boss.MaxHp < 0.2f)
        //{
        //    Debug.Log("ThirdDamage");
        //    m_Boss.SetState((m_Boss as Boss_SoulEater).Damaged);
        //}


    }

    public override void ExitState()
    {
        if ((m_Boss as Boss_SoulEater).Slept == true)
        {
            m_Boss.Anim.SetBool("isRun", false);
        }
        else if ((m_Boss as Boss_SoulEater).Slept == false)
        {
            Debug.Log("이거도 찍히냐?");
            m_Boss.Anim.SetBool("isWalk", false);
        }
    }
}
