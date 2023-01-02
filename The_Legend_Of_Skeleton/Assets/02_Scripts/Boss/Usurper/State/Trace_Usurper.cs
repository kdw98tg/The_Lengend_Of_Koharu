using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace_Usurper : BossState
{
    public Trace_Usurper(Boss _boss) : base("Trace", _boss) { }

    public override void Action()
    {
        m_Boss.Agent.destination = m_Boss.PlayerTr.position;
    }

    public override void CheckState()
    {
        Vector3 dir = (m_Boss.PlayerTr.position - m_Boss.transform.position).normalized;
        float angle = Vector3.Angle(m_Boss.transform.forward, dir);

        if (m_Boss.Agent.remainingDistance <= (m_Boss as Boss_UsurperStateMachine).MeleeAttackDistance
            && angle <= (m_Boss as Boss_UsurperStateMachine).ForwardAngle) 
        {
            m_Boss.SetState((m_Boss as Boss_UsurperStateMachine).Fight);
        }
    }

    public override void EnterState()
    {
        Debug.Log("Usurper : Enter Trace");
        m_Boss.Agent.isStopped = false;
        m_Boss.Anim.SetBool("isTrace", true);
    }

    public override void ExitState()
    {
        m_Boss.Anim.SetBool("isTrace", false);
    }
}
