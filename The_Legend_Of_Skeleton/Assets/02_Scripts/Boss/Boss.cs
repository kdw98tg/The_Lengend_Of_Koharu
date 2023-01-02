using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public abstract class Boss : MonoBehaviour
{
    public delegate void VoidVoidDelegate();
    private VoidVoidDelegate attackDelegate = null;

    #region Inspector

    [Header("Speed")]
    [SerializeField]
    protected float m_IdleSpeed = 1f;
    public float IdleSpeed
    {
        get { return m_IdleSpeed; }
    }

    // 추적속도
    [SerializeField]
    protected float m_TraceSpeed = 1f;
    public float TraceSpeed
    {
        get { return m_TraceSpeed; }
    }


    #endregion



    #region Member_variable

    [SerializeField]
    protected Transform m_PlayerTr = null; // 플레이어 위치 저장
    public Transform PlayerTr
    {
        get { return m_PlayerTr; }
        set { m_PlayerTr = value; }
    }

    [SerializeField] protected Transform m_BossRespawn = null; // 보스 리스폰 위치 저장
    public Transform BossRespawn
    {
        get { return m_BossRespawn; }
        set { m_BossRespawn = value; }
    }

    protected NavMeshAgent m_Agent = null;
    public NavMeshAgent Agent
    {
        get { return m_Agent; }
    }

    protected BossState m_CurState = null;
    public BossState CurState
    {
        get { return m_CurState; }
    }

    protected Animator m_Anim = null;
    public Animator Anim
    {
        get { return m_Anim; }
    }
    [SerializeField]protected float m_currentHp;
    public float CurrentHp
    {
        get { return m_currentHp; }
        set { m_currentHp = value; }
    }

    [SerializeField] private float m_maxHp;
    public float MaxHp { get { return m_maxHp; } }

 

    #endregion


    protected virtual void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Anim = GetComponent<Animator>();
        m_currentHp = m_maxHp;
    }

    protected void OnEnable()
    {
        m_CurState = GetInitialState(); // m_Idle
        if (m_CurState != null)
        {
            m_CurState.EnterState();
        }
    }

    // 매 프레임 상태를 체크하고, 상태에 맞는 액션을 수행한다.
    protected virtual void Update()
    {
        m_CurState.CheckState();
        m_CurState.Action();
    }

    // State 전환 시 호출. 현재 상태의 ExitState를 실행 후
    // 다음 State로 전환. 그리고 EnterState를 실행한다.
    public void SetState(BossState _state)
    {
        if (m_CurState != null)
        {
            m_CurState.ExitState();
        }

        m_CurState = _state;
        m_CurState.EnterState();
    }

    #region Delegate_Callback

    public void IsAttack()
    {
        attackDelegate?.Invoke();
        // ==
        // if (attackDelegate != null)
        //     attackDelegate();
    }

    public void SetDelegate(VoidVoidDelegate _attackCallback)
    {
        attackDelegate = _attackCallback;
    }

    #endregion

    protected abstract BossState GetInitialState();


}

