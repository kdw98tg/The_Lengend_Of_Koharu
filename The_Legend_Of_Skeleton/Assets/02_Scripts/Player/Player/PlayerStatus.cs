using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 가지고 있는 모든 수치데이터들을 관리하는 클래스.
// DB의 역할을 함. 나중에 대체될 수 있음.
public class PlayerStatus : MonoBehaviour
{
    [Header("Move Speed")]

    [SerializeField] private float m_CurMoveSpeed = 5f;
    public float CurMoveSpeed
    { get { return m_CurMoveSpeed; } }

    public float TestSpeed { get; set; }

    [SerializeField] private float m_BaseMoveSpeed = 5f;
    public float BaseMoveSpeed
    { get { return m_BaseMoveSpeed; } }

    [SerializeField] private float m_EvadeMoveSpeed = 0f;
    public float EvadeMoveSpeed
    { get { return m_EvadeMoveSpeed; } }

    [SerializeField] private float m_AttackMoveSpeed = 0.5f;
    public float AttackMoveSpeed
    { get { return m_AttackMoveSpeed; } }

    [SerializeField] private float m_MaxSlopeAngle = 45f;
    public float MaxSlopeAngle
    { get { return m_MaxSlopeAngle; } }

    private float m_KnockbackMoveSpeed = 3f;
    private float m_KnockdownMoveSpeed = 1f;

    private float m_MultipleMoveSpeed = 1f;


    [Space]
    [Header("Attack")]
    #region Damage
    [SerializeField] private float m_CurDamage = 0f;
    public float CurDamage { get { return m_CurDamage; } }

    [SerializeField] private float m_FirstAttackDamage = 1f;
    public float FirstAttackDamage
    { get { return m_FirstAttackDamage; } }

    [SerializeField] private float m_SecondAttackDamage = 2f;
    public float SecondAttackDamage
    { get { return m_SecondAttackDamage; } }

    [SerializeField] private float m_ThirdAttackDamage = 3f;
    public float ThirdAttackDamage
    { get { return m_ThirdAttackDamage; } }

    [SerializeField] private float m_FourthAttackDamage = 4f;
    public float ForuthAttackDamage
    { get { return m_FourthAttackDamage; } }

    [SerializeField] private float m_FirstSmashDamage = 1f;
    public float FirstSmashDamage
    { get { return m_FirstSmashDamage; } }

    [SerializeField] private float m_SecondSmashDamage = 2f;
    public float SecondSmashDamage
    { get { return m_SecondSmashDamage; } }

    [SerializeField] private float m_ThirdSmashDamage = 3f;
    public float ThirdSmashDamage
    { get { return m_ThirdSmashDamage; } }

    [SerializeField] private float m_FourthSmashDamage = 4f;
    public float FourthSmashDamage
    { get { return m_FourthSmashDamage; } }

    [SerializeField] private float m_ElementBurstDamage = 1f;
    public float ElementBurstDamage
    { get { return m_ElementBurstDamage; } }

    private int m_CurElementIdx = 0;
    public int CurElementIdx
    { get { return m_CurElementIdx; } }

    private int m_ElementStack = 0;
    public int ElementStack
    { get { return m_ElementStack; } }

    #endregion Damage

    [Space]
    [Header("Action Speed")]
    #region ActionSpeed
    // 액션 State 관련 속도
    [SerializeField] private float m_ActionTime = 1.2f;
    public float ActionTime
    { get { return m_ActionTime; } }

    [SerializeField] private float m_CanNextActionTime = 0.3f;
    public float CanNextActionTime
    { get { return m_CanNextActionTime; } }

    private float m_BaseActionSpeed = 1f;
    public float BaseActionSpeed
    { get { return m_BaseActionSpeed; } }

    private float m_CurActionSpeed = 1f;
    public float CurActionSpeed
    { get { return m_CurActionSpeed * m_MultipleActionSpeed; } }

    [SerializeField] private float m_MultipleActionSpeed = 1f;  // 디버그용
    public float MultipleActionSpeed
    {
        get { return m_MultipleActionSpeed; }
        set { m_MultipleActionSpeed = value; }
    }
    #endregion


    [Space]
    [Header("Stemina")]
    private float m_MaxStemina = 100f;
    private float m_CurStemina = 0f;
    public float CurStemina
    { get { return m_CurStemina; } }
    [SerializeField] private float m_UseEvadeStemina = 10f;
    public float UseEvadeStemina
    { set { m_UseEvadeStemina = value; } }

    [SerializeField] private float m_SteminaRecoveryPerSecond = 5f;

    [Space]
    [Header("FeverGauge")]
    [SerializeField] private float m_MaxFiverGauge = 100f;
    
    private float m_CurFeverGauge = 0f;
    public float CurFeverGauge
    { get { return m_CurFeverGauge; } }

    [SerializeField] private float m_PlusFeverGaugeValueByOneAttack = 10f;


    [Space]
    [Header("Evade")]
    [SerializeField] private float m_EvadeStartTime = 0.1f;
    public float EvadeStartTime
    { get { return m_EvadeStartTime; } }

    [SerializeField] private float m_EvadeDurationTime = 0.5f;
    public float EvadeDurationTime
    { get { return m_EvadeDurationTime; } }

    [Space]
    [Header("Jump")]
    [SerializeField] private float m_JumpStartTime = 0.1f;
    public float JumpStartTime
    { get { return m_JumpStartTime; } }

    [SerializeField] private float m_JumpDurationTime = 0.5f;
    public float JumpDurationTime
    { get { return m_JumpDurationTime; } }

    private void Start()
    {
        m_CurStemina = m_MaxStemina;
    }

    private void Update()
    {
        m_CurStemina += m_SteminaRecoveryPerSecond * Time.deltaTime;
        if (m_CurStemina >= m_MaxStemina)
        {
            m_CurStemina = m_MaxStemina;
        }
    }

    public void PlusElementStack()
    {
        ++m_ElementStack;
    }

    public bool IsElementStackEmpty()
    {
        if (m_ElementStack == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsSteminaEnough()
    {
        if (m_CurStemina >= 10f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnNomalStateCallback()
    {
        m_CurMoveSpeed = m_BaseMoveSpeed * m_MultipleMoveSpeed;
    }

    public void OnComboAttackStateCallback()
    {
        // 20221218 양우석 공격중일때 움직이는 속도는 이동속도 영향 받아야하는지 고민해보기
        m_CurMoveSpeed = m_AttackMoveSpeed/* * m_MultipleMoveSpeed*/;
    }

    public void OnEvadeStateCallback()
    {
        UseSteminaForEvade();
        m_CurMoveSpeed = m_EvadeMoveSpeed * m_MultipleMoveSpeed;
    }

    public void OnJumpCallback()
    {
        m_CurMoveSpeed = m_BaseMoveSpeed * m_MultipleMoveSpeed;
    }
    public void OnKnockbackCallback()
    {
        m_CurMoveSpeed = m_KnockbackMoveSpeed;
    }

    public void OnKnockdownCallback()
    {
        m_CurMoveSpeed = m_KnockdownMoveSpeed;
    }

    public void EnemyAttackCallback()
    {
        m_CurFeverGauge += m_PlusFeverGaugeValueByOneAttack;
    }

    private void UseSteminaForEvade()
    {
        m_CurStemina -= m_UseEvadeStemina;
        Debug.Log(m_CurStemina);
    }

    

}
