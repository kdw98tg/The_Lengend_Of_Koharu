using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Attack_Kurniawan : BossState
{
    public Attack_Kurniawan(Boss _boss) : base("Attack", _boss){} //생성자

    public override void EnterState()
    {
        Debug.Log("Attack_Kurniawan 입장");
        m_Boss.Anim.SetBool("IsAttack", true);
    }

    public override void ExitState()
    {
        Debug.Log("Attack_Kurniawan 퇴장");
        m_Boss.Anim.SetBool("IsAttack", false);
        
    }

    public override void Action()
    {
        
    }

    public override void CheckState()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);

        if (distance > (m_Boss as Boss_Kurniawan).AttackRange)
        {
            m_Boss.SetState((m_Boss as Boss_Kurniawan).Trace);
            return;
        }
    }

  

}
