using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep_SoulEater : BossState
{
    public Sleep_SoulEater (Boss _boss) : base("Sleep", _boss) { }
    //private float m_sleepingTime;
    private bool mb_slept = false;
    private float sleepingTime;
    
    public override void Action()
    {
        if (!(m_Boss as Boss_SoulEater).Slept)
        //잤던 상태가 아니라면 자는 애니메이션이 실행되고
        //체력이 서서히찬다.
        //10초가 지나면 그만둔다.

        {
            (m_Boss as Boss_SoulEater).Sleeping();
            //Debug.Log("CurrentHp : " + (m_Boss as Boss_SoulEater).CurrentHp);
            sleepingTime += Time.deltaTime;
            if (sleepingTime > (m_Boss as Boss_SoulEater).SleepingTime)//checkstate로 이식해야하지않나..?

            {
                Debug.Log("sleeping end");
                m_Boss.Anim.SetBool("isSleeping", false);
                m_Boss.SetState((m_Boss as Boss_SoulEater).Idle);
                (m_Boss as Boss_SoulEater).Slept = true;
            }

        }
    }

    public override void CheckState()
    {
        //죽은상태 분기
        if (m_Boss.CurrentHp <= 0f)
        {
            Debug.Log("SoulEater Die");
            m_Boss.SetState((m_Boss as Boss_SoulEater).Die);
        }
    }

    public override void EnterState()
    {
        Debug.Log("Sleeping...");
        m_Boss.Anim.SetBool("isSleeping", true);
        m_Boss.Agent.isStopped = true;
        (m_Boss as Boss_SoulEater).StartCoroutine((m_Boss as Boss_SoulEater).SpawnMonsters(3, 2.5f, 1f));//몇마리를, 목적지로, 속도만큼. 간격마다
    }

    public override void ExitState()
    {
        Debug.Log("Exit Sleeping");
        m_Boss.Agent.isStopped = false;
    }
}
