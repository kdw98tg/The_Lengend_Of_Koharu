using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Usurper : BossState
{
    public Idle_Usurper(Boss _boss) : base("Idle", _boss) { }

    private float m_FightDistance = 50f;

    public override void Action()
    {
    }

    public override void CheckState()
    {
        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);

        if(distance <= m_FightDistance)
        {
            m_Boss.SetState((m_Boss as Boss_UsurperStateMachine).Fight);
        }
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }
}
