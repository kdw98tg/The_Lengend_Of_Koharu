using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Boss_SoulEater : Boss
{
    #region State
    private Idle_SoulEater m_Idle = null;
    public Idle_SoulEater Idle { get { return m_Idle; } }

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
    [SerializeField]private float m_idleRange;
    public float IdleRange { get { return m_idleRange; } }

    private float m_forwardAngle = 30f;
    public float ForwardAngle { get { return m_forwardAngle; } }

    [SerializeField] private GameObject m_smallDragon = null;
    public GameObject SmallDragon { get { return m_smallDragon; } }

    [SerializeField] private float m_traceDistance;
    public float TraceDistance { get { return m_traceDistance; } }

    [SerializeField] private bool mb_slept = false;
    public bool Slept { get { return mb_slept; } set { mb_slept = value; } }

    [SerializeField] private float m_sleepingTime = 10f;
    public float SleepingTime { get { return m_sleepingTime; } }

    [SerializeField] private bool mb_isAttacked = false;
    public bool IsAttacked { get { return mb_isAttacked; } set { mb_isAttacked = value; } }

    [SerializeField] private float m_attackDelay;
    public float AttackDelay { get { return m_attackDelay;} set { m_attackDelay = value; } }

    private BoxCollider boxCollider;

    private bool m_firstDamaged = false;
    public bool FirstDamaged { get { return m_firstDamaged; } set { m_firstDamaged = value; } }

    private bool m_secondDamaged = false;
    public bool SecondDamaged { get { return m_secondDamaged; } set { m_secondDamaged = value; } }

    private bool m_thirdDamaged = false;
    public bool ThirdDamaged { get { return m_thirdDamaged; } set { m_thirdDamaged = value; } }

    [SerializeField] private SpawnPoint m_spawnPoint = null;
    [SerializeField] private Transform m_rightSide = null;
    [SerializeField] private Transform m_leftSide = null;

    [SerializeField] private HitBox_Jaw hitBox_Jaw;
    public HitBox_Jaw HitBox_Jaw { get { return hitBox_Jaw; }  }

    #endregion

    protected override void Awake()
    {

        base.Awake();
        m_Idle = new Idle_SoulEater(this);
        m_Trace = new Trace_SoulEater(this);
        m_Sleep = new Sleep_SoulEater(this);
        m_Attack = new Attack_SoulEater(this);
        m_Die = new Die_SoulEater(this);
        boxCollider = GetComponent<BoxCollider>();

    }
    private void Start()
    {
        hitBox_Jaw.BoxColliderEnabled(false);
    }
    public void Sleeping()
    {
        Anim.SetBool("isSleeping", true);
        Agent.isStopped = true;
        CurrentHp += Time.deltaTime;
    }

public void SetTraceMode()
{
    if ((Slept == true))
        {
            TraceAfterSleep();
        }
        else 
        {
            NormalTrace();
        }
}
    public void TraceAfterSleep()
    {
        Debug.Log("Range Trace");
        this.Anim.SetBool("isRun", true);
        //this.Anim.SetBool("isWalk", false);
        this.Agent.isStopped = false;
        this.Agent.speed = 5f;
    }
    public void NormalTrace()
    {
        Debug.Log("Trace");
        this.Anim.SetBool("isWalk", true);
        this.Agent.isStopped = false;
        //m_Boss.Agent.stoppingDistance = 10f;
    }
    public void DeadAndDestroy()
    {
        Debug.Log("deadCoroutine");
        StopAllCoroutines();
        //�ڽ� �ݶ��̴� ����
        DisableCollider();
        Destroy(gameObject, 5f);
    }
    private void DisableCollider()
    {
        boxCollider.enabled = false;
    }
    public IEnumerator SpawnMonsters(int _monsterPopulation , float _moveSpeed, float _spawnDelay)
    {
        Debug.Log("SpawnMonster");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _monsterPopulation; ++i)
        {
            GameObject go = Instantiate(m_smallDragon, m_spawnPoint.SpawnPointTr().position, m_spawnPoint.SpawnPointTr().rotation); //�� �������� �迭�� �ޱ�
            go.GetComponent<NavMeshAgent>().speed = _moveSpeed;

            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public void LongRangeAttack()
    {
        Agent.isStopped = true;
        GameObject go1 = Instantiate(m_smallDragon, m_rightSide.position, m_rightSide.rotation);
        GameObject go2 = Instantiate(m_smallDragon, m_leftSide.position, m_leftSide.rotation);

        
        go1.GetComponent<SmallDragon_SoulEater>().SetPlayer(m_PlayerTr.GetComponent<Player>());
        go2.GetComponent<SmallDragon_SoulEater>().SetPlayer(m_PlayerTr.GetComponent<Player>());
    }

    


    protected override BossState GetInitialState()
    {
        return m_Idle;
    }
}
