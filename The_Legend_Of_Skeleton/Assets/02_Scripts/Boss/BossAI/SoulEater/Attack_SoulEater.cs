using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack_SoulEater : BossState
{
    public Attack_SoulEater(Boss _boss) : base("Attack", _boss) { }

    public override void Action()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        Vector3 dir = (m_Boss.PlayerTr.position - m_Boss.transform.position).normalized;
        float angle = Vector3.Angle(m_Boss.transform.forward, dir);

        //���� ������ ���� ����
        if ((m_Boss as Boss_SoulEater).IsAttacked == true)
        {
            //m_Boss.Agent.isStopped = true;
            //Debug.Log("Count");
           // Debug.Log("  (m_Boss as Boss_SoulEater).AttackDelay " + (m_Boss as Boss_SoulEater).AttackDelay);
            (m_Boss as Boss_SoulEater).AttackDelay += Time.deltaTime;
        }
        if ((m_Boss as Boss_SoulEater).AttackDelay > 3f)
        {
            Debug.Log("isAttacked false");
            (m_Boss as Boss_SoulEater).IsAttacked = false;
            (m_Boss as Boss_SoulEater).AttackDelay = 0f;
        }
        //Debug.Log(angle);


        if (angle <= (m_Boss as Boss_SoulEater).ForwardAngle && distance < 5f && (m_Boss as Boss_SoulEater).IsAttacked == false) //������ ����
        {
            Debug.Log("forward attack bite");
            m_Boss.Anim.SetTrigger("isAttack");
            m_Boss.Agent.isStopped = true;
            (m_Boss as Boss_SoulEater).IsAttacked = true;
            //(m_Boss as Boss_SoulEater).HitBox_Jaw.BoxColliderEnabled(true);
        }
        else if (distance <= 15f && distance > 5f && (m_Boss as Boss_SoulEater).IsAttacked == false)//�����ϴ� �밡�� ��ȯ
        {
            int randomIdx = Random.Range(0,10);
            Debug.Log(randomIdx);
            if(randomIdx < 5)
            {
            Debug.Log("long distance attack");
            m_Boss.Anim.SetTrigger("isAttack_long");

            (m_Boss as Boss_SoulEater).LongRangeAttack();
            (m_Boss as Boss_SoulEater).IsAttacked = true;
            }
            else 
            {
                Debug.Log("와라");
                (m_Boss as Boss_SoulEater).SetTraceMode();
                (m_Boss as Boss_SoulEater).IsAttacked = true;
            }

        }
    }


    public override void CheckState()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        if (m_Boss.CurrentHp / m_Boss.MaxHp < 0.3f && (m_Boss as Boss_SoulEater).Slept == false)
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Sleep);
        }
        else if (distance > 20f)
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Trace);
        }
        //�������� �б�
        else if (m_Boss.CurrentHp <= 0f)
        {
            Debug.Log("SoulEater Die");
            m_Boss.SetState((m_Boss as Boss_SoulEater).Die);
        }
    }

    public override void EnterState()
    {
        Debug.Log("Fighting");
    }

    public override void ExitState()
    {
        Debug.Log("ExitFighting");
    }


}
