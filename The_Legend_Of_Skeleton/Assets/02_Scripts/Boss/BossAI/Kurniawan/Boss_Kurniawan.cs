using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Boss_Kurniawan : Boss //몸통 클래스 스테이트를 바꿔주는
{
    VoidVoidDelegate enterIdleDelegate = null;
    VoidVoidDelegate enterTraceDelegate = null;
    VoidVoidDelegate exitTraceDelegate = null;
    
    #region BossState
    private Idle_Kurniawan m_Idle = null;
    public Idle_Kurniawan Idle
    {
        get { return m_Idle; }
    }

    private Trace_Kurniawan m_Trace = null;
    public Trace_Kurniawan Trace
    {
        get { return m_Trace; }
    }
    
    private Attack_Kurniawan m_Attack = null;
    public Attack_Kurniawan Attack
    {
        get { return m_Attack; }
    }
    
    private Die_Kurniawan m_Die = null;
    public Die_Kurniawan Die
    {
        get { return m_Die; }
    }
    #endregion
    
    #region Inspector
    [Space]
    [Header("Range")]
    [SerializeField] protected float m_IdleDetectRange = 20f;
    public float IdleDetectRange
    {
        get { return m_IdleDetectRange; }
    }
    

    [SerializeField] protected float m_TraceDetectRange = 20f;
    public float TraceDetectRange
    {
        get { return m_TraceDetectRange; }
    }

    [SerializeField] protected float m_AttackRange = 3f;
    public float AttackRange
    {
        get { return m_AttackRange; }
    }

    [Space]
    [Header("Detect Angle")]
    [SerializeField] protected float m_IdleDetectAngle = 120f;
    public float IdleDetectAngle
    {
        get { return m_IdleDetectAngle; }
    }

    [SerializeField] protected float m_TraceDetectAngle = 180f;
    public float TraceDetectAngle
    {
        get { return m_TraceDetectAngle; }
    }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        m_Idle = new Idle_Kurniawan(this);//this넣는게 생성자에 이 클래스를 집어넣음
        m_Trace = new Trace_Kurniawan(this);
        m_Attack = new Attack_Kurniawan(this);
        m_Die = new Die_Kurniawan(this);
    }

    private void Start()
    {
        StartCoroutine(BossPattern());
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            int randomReact = Random.Range(0, 3);
            switch (randomReact)
            {
                case 0:
                    Anim.SetFloat("DoReact",2.9f);
                    break;
                case 1:
                    Anim.SetFloat("DoReact",1.9f);
                    break;
                case 2:
                    Anim.SetFloat("DoReact",0.9f);
                    break;
            }   
        }
        
        
    }


    private IEnumerator BossPattern()
    {
        yield return new WaitForSeconds(0.3f);
        
        int randomAction = Random.Range(0, 3);
        switch (randomAction)
        {
            case 0:
                StartCoroutine(Kcik());
                break;
            case 1:
                StartCoroutine(Melee());
                break;
            case 2:
                StartCoroutine(Combo());
                break;
        }

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator Kcik()
    {
        Anim.SetTrigger("DoKick");
        yield return new WaitForSeconds(2f);
        StartCoroutine(BossPattern());
    }
    
    private IEnumerator Melee()
    {
        Anim.SetTrigger("DoMelee");
        yield return new WaitForSeconds(2f);
        StartCoroutine(BossPattern());
    }
    
    private IEnumerator Combo()
    {
        Anim.SetTrigger("DoCombo");
        yield return new WaitForSeconds(2f);
        StartCoroutine(BossPattern());
    }
    
    
    
    
    
    
    

    protected override BossState GetInitialState()
    {
        return m_Idle;
    }

    #region Delegate_Callback
    public void EnterIdleCallback()
    {
        UnityEngine.Debug.Log("EnterIdleCallback");
        enterIdleDelegate?.Invoke();
    }
    
    public void EnterTraceCallback()
    {
        UnityEngine.Debug.Log("EnterTraceCallback");
        enterTraceDelegate?.Invoke();
    }
    
    public void ExitTraceCallback()
    {
        UnityEngine.Debug.Log("ExitTraceCallback");
        exitTraceDelegate?.Invoke();
    }


    public void SetDelegate(VoidVoidDelegate _enterPatrolCallback, VoidVoidDelegate _enterTracePlayerCallback, VoidVoidDelegate _exitTracePlayerCallback)
    {
        enterIdleDelegate = _enterPatrolCallback;
        enterTraceDelegate = _enterTracePlayerCallback;
        exitTraceDelegate = _exitTracePlayerCallback;
        
    }
    #endregion
}
