using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossState
{
    protected string m_Name;
    protected Boss m_Boss;
    protected NavMeshAgent m_Agent;
    
    public BossState(string _name, Boss _boss)
    {
        m_Name = _name;
        m_Boss = _boss;
    }

    public string Name
    {
        get { return m_Name; }
    }


    public abstract void EnterState();
    public abstract void CheckState();
    public abstract void Action();
    public abstract void ExitState();

}
