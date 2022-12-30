using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_UsurperStateMachine : Boss
{
    public delegate void VoidVoidtDelegate();
    private VoidVoidtDelegate screamDelegate = null;
    
    #region State
    private Idle_Usurper m_Idle = null;
    public Idle_Usurper Idle
    { get { return m_Idle; } }

    private Fight_Usurper m_Fight = null;
    public Fight_Usurper Fight
    { get { return m_Fight; } }

    private Trace_Usurper m_Trace = null;
    public Trace_Usurper Trace
    { get { return m_Trace; } }

    private Lazy_Usurper m_Lazy = null;
    public Lazy_Usurper Lazy
    { get { return m_Lazy; } }

    private Die_Usurper m_Die = null;
    public Die_Usurper Die
    { get { return m_Die; } }

    private Sturn_Usurper m_Sturn = null;
    public Sturn_Usurper Sturn
    { get { return m_Sturn; } }
    #endregion

    #region Member_variable
    private float m_ForwardAngle = 5f;
    public float ForwardAngle
    { get { return m_ForwardAngle; } }

    
    private float m_MeleeAttackDistance = 15f;
    public float MeleeAttackDistance
    { get { return m_MeleeAttackDistance; } }

    private float m_RangeAttackDistance = 50f;
    public float RangeAttackDistance
    { get { return m_RangeAttackDistance; } }

    private float m_CurSturnGauge = 0f;
    public float CurSturnGauge
    { get { return m_CurSturnGauge; } }

    private float m_InitHp = 100f;
    private float m_CurHp = 100f;
    public float CurHp
    { get { return m_CurHp; } }
    public float CurHpRatio
    { get { return (m_CurHp / m_InitHp) * 100f; } }

    private float m_RotSpeed = 5f;
    private float m_MoveSpeed = 3f;

    

    #endregion

    protected override void Awake()
    {
        base.Awake();
        m_Anim = GetComponentInChildren<Animator>();

        m_Idle = new Idle_Usurper(this);
        m_Fight = new Fight_Usurper(this);
        m_Trace = new Trace_Usurper(this);
        m_Lazy = new Lazy_Usurper(this);
        m_Die = new Die_Usurper(this);
        m_Sturn = new Sturn_Usurper(this);
    }

    #region Action
    public void LookAtTarget()
    {
        // 걷는모션 출력

        // 러프나 반복문으로 회전함수 만들기
        //this.transform.rotation = Quaternion.LookRotation(m_PlayerTr.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(m_PlayerTr.position - this.transform.position), m_RotSpeed * Time.deltaTime);
    }
    public void TraceTarget()
    {
        // 걷는모션 출력

        this.GetComponent<Rigidbody>().velocity = (m_PlayerTr.position - this.transform.position).normalized * m_MoveSpeed;
    }
    public void Bite()
    {
        
;        LookAtTarget();
        m_Anim.SetTrigger("isBite");
        StartCoroutine(ActionCoolTimeCoroutine(Random.Range(0f, 2f)));
    }
    public void Headbutt()
    {
        LookAtTarget();
        m_Anim.SetTrigger("isHeadbutt");
        StartCoroutine(ActionCoolTimeCoroutine(Random.Range(1f,3f)));
    }
    public void Breath()
    {
        m_Anim.SetTrigger("isBreath");
        StartCoroutine(ActionCoolTimeCoroutine(Random.Range(1f,3f)));
    }
    public void Scream()
    {
        m_Anim.SetTrigger("isScream");
        screamDelegate?.Invoke();
        StartCoroutine(ActionCoolTimeCoroutine(Random.Range(4f,6f)));
    }
    public void Slap()
    {
        LookAtTarget();
        m_Anim.SetTrigger("isSlap");
        StartCoroutine(ActionCoolTimeCoroutine(Random.Range(1f,2f)));
    }
    public void TailAttack()
    {
        m_Anim.SetTrigger("isTailAttack");
        StartCoroutine(ActionCoolTimeCoroutine(Random.Range(4f,6f)));
    }
  

    private IEnumerator ActionCoolTimeCoroutine(float _delay)
    {
        yield return new WaitForSeconds(_delay + m_Anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        m_Fight.SetIsAttacking(false);
    }

    
    #endregion
    protected override BossState GetInitialState()
    {
        return m_Idle;
    }


    public void Init(VoidVoidtDelegate _screamCallback)
    {
        screamDelegate = _screamCallback;
    }
}
