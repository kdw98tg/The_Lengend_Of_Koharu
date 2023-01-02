using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaged_SoulEater : BossState
{
    public Damaged_SoulEater(Boss _boss) : base("Damaged", _boss) { }

    public override void Action()
    {

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
        //m_Boss.Anim.SetTrigger("isDamaged");
    }

    public override void ExitState()
    {

    }
}
