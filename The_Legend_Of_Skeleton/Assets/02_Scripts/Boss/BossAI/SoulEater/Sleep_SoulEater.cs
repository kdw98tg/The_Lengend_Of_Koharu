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
        //��� ���°� �ƴ϶�� �ڴ� �ִϸ��̼��� ����ǰ�
        //ü���� ����������.
        //10�ʰ� ������ �׸��д�.

        {
            (m_Boss as Boss_SoulEater).Sleeping();
            //Debug.Log("CurrentHp : " + (m_Boss as Boss_SoulEater).CurrentHp);
            sleepingTime += Time.deltaTime;
            if (sleepingTime > (m_Boss as Boss_SoulEater).SleepingTime)//checkstate�� �̽��ؾ������ʳ�..?

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
        //�������� �б�
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
        (m_Boss as Boss_SoulEater).StartCoroutine((m_Boss as Boss_SoulEater).SpawnMonsters(3, 2.5f, 1f));//�����, ��������, �ӵ���ŭ. ���ݸ���
    }

    public override void ExitState()
    {
        Debug.Log("Exit Sleeping");
        m_Boss.Agent.isStopped = false;
    }
}
