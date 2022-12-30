using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SoulEater : Boss
{
    #region State
    private Idle_SoulEater m_Idle = null;
    public Idle_SoulEater Idle { get { return m_Idle; } }

    private Patrol_SoulEater m_Patrol = null;
    public Patrol_SoulEater Patrol { get { return m_Patrol; } }

    private Trace_SoulEater m_Trace = null;
    public Trace_SoulEater Trace { get { return m_Trace; } }

    private Attack_SoulEater m_Attack = null;
    public Attack_SoulEater Attack { get { return m_Attack; } }

    private Sleep_SoulEater m_Sleep = null;
    public Sleep_SoulEater Sleep { get { return m_Sleep; } }

    private Damaged_SoulEater m_Damaged = null;
    public Damaged_SoulEater Damaged { get { return m_Damaged; } }

    private Die_SoulEater m_Die = null;
    public Die_SoulEater Die { get { return m_Die; } }

    #endregion


    #region Member_Variable
    private float forwardAngle = 30f;
    public float ForwardAngle { get { return forwardAngle; } }

    [SerializeField] private GameObject m_smallDragon = null;
    public GameObject SmallDragon { get { return m_smallDragon; } }

    [SerializeField] private float m_traceDistance;
    public float TraceDistance { get { return m_traceDistance; } }

   [SerializeField]private bool mb_slept = false;
    public bool Slept { get { return mb_slept; } set { mb_slept = value; } }

    [SerializeField]private float m_sleepingTime = 10f;
    public float SleepingTime { get { return m_sleepingTime; } }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        m_Idle = new Idle_SoulEater(this);
        m_Patrol = new Patrol_SoulEater(this);
        m_Trace = new Trace_SoulEater(this);
        m_Sleep = new Sleep_SoulEater(this);
        m_Attack = new Attack_SoulEater(this);
        m_Die = new Die_SoulEater(this);

    }

    public void Sleeping()
    {
        Anim.SetBool("isSleeping", true);
        Agent.isStopped = true;
        CurrentHp += Time.deltaTime;
    }



    protected override BossState GetInitialState()
    {
        return m_Idle;
    }
}
