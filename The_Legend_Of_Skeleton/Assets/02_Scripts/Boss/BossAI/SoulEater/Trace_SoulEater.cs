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
        m_Boss.Agent.destination = m_Boss.PlayerTr.position;//player�� �������� �Ѿư�
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
        else if (m_Boss.CurrentHp / m_Boss.MaxHp < 0.3f && (m_Boss as Boss_SoulEater).Slept == false )//�ѹ� �� ���¸� Sleep���·� �Ȱ�, �÷��̾� ����� �絵���� ���� �ʿ��ҵ�
        {
            Debug.Log("SleepAgain");
            m_Boss.SetState((m_Boss as Boss_SoulEater).Sleep);
        }

        //�������� �б�
        if (m_Boss.CurrentHp <= 0f)
        {
            Debug.Log("SoulEater Die");
            m_Boss.SetState((m_Boss as Boss_SoulEater).Die);
        }

        //Damage �б⹮
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
            Debug.Log("�̰ŵ� ������?");
            m_Boss.Anim.SetBool("isWalk", false);
        }
    }
}
