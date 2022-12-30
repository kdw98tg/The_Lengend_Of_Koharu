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


        //Debug.Log(angle);

        if(distance < (m_Boss as Boss_SoulEater).TraceDistance)
        {
            Debug.Log("isStopped");
            m_Boss.Agent.isStopped = true;
            m_Boss.Anim.SetBool("isWalk", false);
        }
        else
        {
            Debug.Log("notisStopped");
            m_Boss.Agent.isStopped = false;
            m_Boss.Anim.SetBool("isWalk", true);
            m_Boss.Agent.destination = m_Boss.PlayerTr.position;
        }

        if (angle <= (m_Boss as Boss_SoulEater).ForwardAngle && distance < 5f) //깨물기 공격
        {
            Debug.Log("forward attack bite");
            m_Boss.Anim.SetTrigger("isAttack");
            //m_Boss.Anim.SetTrigger("isAttack_Bite");
            m_Boss.Agent.isStopped = true;
        }
        //else
        //{
        //    m_Boss.SetState((m_Boss as Boss_SoulEater).Trace);
        //}


        if(distance < 13f && distance > 5f)//용가리 소환, 불덩이를 날릴까 말까
        {
            Debug.Log("long distance attack");
            //GameObject smallDragon = (m_Boss as Boss_SoulEater).SmallDragon;
            //드래곤 프리팹 소환
        }
        //else
        //{
        //    m_Boss.SetState((m_Boss as Boss_SoulEater).Trace);
        //}


    }


    public override void CheckState()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        if (m_Boss.CurrentHp/m_Boss.MaxHp < 0.3f && (m_Boss as Boss_SoulEater).Slept == false)
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Sleep);
        }

        if (distance > 20f )
        {
            m_Boss.SetState((m_Boss as Boss_SoulEater).Trace);
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
