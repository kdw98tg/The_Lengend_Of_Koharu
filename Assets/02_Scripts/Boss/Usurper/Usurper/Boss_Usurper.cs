using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Usurper : MonoBehaviour
{
    private Boss_UsurperStateMachine m_StateMachine = null;
    private Boss_UsurperSkill m_Skill = null;
    private Boss_UsurperStatus m_Status = null;

    private void Awake()
    {
        m_StateMachine = GetComponent<Boss_UsurperStateMachine>();
        m_Skill = GetComponent<Boss_UsurperSkill>();
        m_Status = GetComponent<Boss_UsurperStatus>();
    }


    private void Start()
    {
        m_StateMachine.Init(Scream);
    }

    

    private void Scream()
    {
        m_Skill.ThunderPlane(m_Status.ThunderPlaneIntervalTime);
    }

}
