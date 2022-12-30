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
        //�� �ٷ� �ȹٲ���
        m_Boss.Agent.destination = m_Boss.PlayerTr.position;//player�� �������� �Ѿư�
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);


    }

    public override void CheckState()
    {
        //Debug.Log("efse" + (m_Boss.CurrentHp / m_Boss.MaxHp));
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position); //�÷��̾�� �Ÿ��� �缭 10 �̸��̸� ���ݸ��� ����
        if (distance < 15f)
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Attack);
        }
        else if (distance >= 30f)
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Idle);
        }


        if (m_Boss.CurrentHp / m_Boss.MaxHp < 0.3f && (m_Boss as Boss_SoulEater).Slept == false)//�ѹ� �� ���¸� Sleep���·� �Ȱ�, �÷��̾� ����� �絵���� ���� �ʿ��ҵ�
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
            Debug.Log("�̰ŵ� ������?");
            m_Boss.Anim.SetBool("isWalk", false);
        }
    }
}
