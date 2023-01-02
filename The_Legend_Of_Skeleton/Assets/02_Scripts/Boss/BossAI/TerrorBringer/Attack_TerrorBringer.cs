using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_TerrorBringer : BossState
{
    public Attack_TerrorBringer(Boss _boss) : base("Attack", _boss) { }
    public override void EnterState()
    {
        Debug.Log("Enter Attack");
    }
    public override void Action()
    {
        if ((m_Boss as Boss_TerrorBringer).IsAttacked == true)
            return;

        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        Vector3 dir = (m_Boss.PlayerTr.position - m_Boss.transform.position).normalized;
        float angle = Vector3.Angle(m_Boss.transform.forward, dir);
    

        if (angle <= (m_Boss as Boss_TerrorBringer).ForwardAngle && distance <= (m_Boss as Boss_TerrorBringer).ShortAttackRange) 
        {
            //근거리 공격
            Debug.Log("Short Attack");
            m_Boss.Anim.SetTrigger("isClawAttack");
            (m_Boss as Boss_TerrorBringer).IsAttacked = true;
            return;
        }



        if (distance <= (m_Boss as Boss_TerrorBringer).LongAttackRange && distance > (m_Boss as Boss_TerrorBringer).ShortAttackRange)
        {
            //원거리 공격
            int randomIdx = Random.Range(0, 10);
            Debug.Log("Long Attack" + randomIdx);
            if (randomIdx <= 5)
            {
                Debug.Log("돌진 공격");
                (m_Boss as Boss_TerrorBringer).StartCoroutine((m_Boss as Boss_TerrorBringer).RushAttack());
                (m_Boss as Boss_TerrorBringer).IsAttacked = true;

                return;
                //for (int i = 0; i<100; ++i)
                //m_Boss.transform.position = Vector3.Lerp(m_Boss.transform.position, m_Boss.PlayerTr.position, 0.01f);
            }
            else if (randomIdx <= 10)
            {
                Debug.Log("독성 공격");
                (m_Boss as Boss_TerrorBringer).StartCoroutine((m_Boss as Boss_TerrorBringer).ToxicAttack());
                (m_Boss as Boss_TerrorBringer).IsAttacked = true;
                return;
            }
            // else 
            // {
            //    Debug.Log("Attack End, Enter Trace");
            //    m_Boss.SetState((m_Boss as Boss_TerrorBringer).Trace);
            // }
            //(m_Boss as Boss_TerrorBringer).IsAttacked = true;
            return;
        }


    }

    public override void CheckState()
    {

        if ((m_Boss as Boss_TerrorBringer).CurrentHp / (m_Boss as Boss_TerrorBringer).MaxHp <= 0.4f && !(m_Boss as Boss_TerrorBringer).IsRaining)
        {
            m_Boss.SetState((m_Boss as Boss_TerrorBringer).Raining);
            return;
        }
        else if ((m_Boss as Boss_TerrorBringer).CurrentHp <= 0f)
        {
            m_Boss.SetState((m_Boss as Boss_TerrorBringer).Die);
            return;
        }
    }


    public override void ExitState()
    {
        Debug.Log("Exit Attack");
        (m_Boss as Boss_TerrorBringer).IsAttacked = false;
    }
}
