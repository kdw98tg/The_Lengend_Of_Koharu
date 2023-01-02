using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_TerrorBringer : Boss
{

    #region State
    private Idle_TerrorBringer m_Idle = null;
    public Idle_TerrorBringer Idle { get { return m_Idle; } }

    private Trace_TerrorBringer m_Trace = null;
    public Trace_TerrorBringer Trace { get { return m_Trace; } }

    private Attack_TerrorBringer m_Attack = null;
    public Attack_TerrorBringer Attack { get { return m_Attack; } }

    private Raining_TerrorBringer m_Raining = null;
    public Raining_TerrorBringer Raining { get { return m_Raining; } }

    private Damaged_TerrorBringer m_Damaged = null;
    public Damaged_TerrorBringer Damaged { get { return m_Damaged; } }

    private Die_TerrorBringer m_Die = null;
    public Die_TerrorBringer Die { get { return m_Die; } }

    #endregion

    #region Member_Variable

    [SerializeField] private float m_idleRange = 30f;
    public float IdleRange { get { return m_idleRange; } }

    [SerializeField] private float m_enterAttackRange = 15f;
    public float EnterAttackRange { get { return m_enterAttackRange; } }

    private float m_forwardAngle = 30f;
    public float ForwardAngle { get { return m_forwardAngle; } }

    [SerializeField]private bool m_isAttacked = false;
    public bool IsAttacked { get { return m_isAttacked; } set { m_isAttacked = value; } }

    private float m_longAttackRange = 14f;
    public float LongAttackRange { get { return m_longAttackRange; } }

    private float m_shortAttackRange = 5f;
    public float ShortAttackRange { get { return m_shortAttackRange; } }

    private float m_attackDelay ;
    public float AttackDelay { get { return m_attackDelay; } set { m_attackDelay = value; } }

    private bool m_isRaining = false;
    public bool IsRaining { get { return m_isRaining; } set { m_isRaining = value; } }

    private float m_rainingTime = 0;
    public float RainingTime { get { return m_rainingTime; } set { m_rainingTime = value; } }

    [SerializeField]private float m_maxRainingTime = 10f;
    public float MaxRainingTime { get { return m_maxRainingTime; }  }

    [SerializeField] private GameObject rainingPrefab;
    [SerializeField] private Transform rainingPos;

    private float m_rushCount = 250;


    [SerializeField] private GameObject m_ToxicObject;
    [SerializeField] private Transform m_toxicPos;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        m_Idle = new Idle_TerrorBringer(this);
        m_Trace = new Trace_TerrorBringer(this);
        m_Attack = new Attack_TerrorBringer(this);
        m_Raining = new Raining_TerrorBringer(this);
        m_Die = new Die_TerrorBringer(this);
    }
    public void NormalTrace()
    {
        Debug.Log("NormalTrace");
        Agent.isStopped = false;
        Anim.SetBool("isWalk", true);
        //this.Agent.isStopped = false;
    }
    public IEnumerator RushAttack()
    {
        Agent.isStopped = true;
        Anim.SetBool("isRushAttack", true);
        yield return new WaitForSeconds(2f);
        for(int i = 0; i<= m_rushCount ; ++i)//이렇게 해도되나...?
        {
            transform.position += transform.forward * 0.1f;
            yield return new WaitForSeconds(0.01f);
            if(i == m_rushCount)
            Anim.SetBool("isRushAttack",false);
            
        }
        
        yield return null;
    }
    public IEnumerator ToxicAttack()
    {
        Debug.Log("toxic attack");
        Agent.isStopped = true;
        Anim.SetTrigger("isToxicAttack");
        yield return new WaitForSeconds(2f);
        GameObject go = Instantiate(m_ToxicObject, m_toxicPos.position, m_toxicPos.rotation);
        go.transform.SetParent(m_toxicPos);
        Destroy(go, 6f);
        
        //IsAttacked = true;
    }
    public IEnumerator RainingCoroutine()
    {
        //float theta = Random.Range(0f, 360f);
        //float radius = Random.Range(2f, 20f);
        //Vector3 RandomPos = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0f) * radius;
        yield return new WaitForSeconds(1f);
        for(int i = 0; i< 3; ++i)
        {
            GameObject go = Instantiate(rainingPrefab, rainingPos.position, rainingPos.rotation);
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator DieCoroutine(float _destroyTime)
    {
        Debug.Log("DieCoroutine");
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }


    protected override BossState GetInitialState()
    {
        return m_Idle;
    }
}
