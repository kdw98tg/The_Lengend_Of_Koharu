using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

// 싱글톤 필요할지 고민해보기. PlayerStatus는 싱글톤이어야 함.
public class Player : MonoBehaviour
{
    #region Delegate
    public delegate Vector3 Vector3VoidDelegate();
    private Vector3VoidDelegate getCamForwardDelegate = null;
    private Vector3VoidDelegate getCamRightDelegate = null;
    private Vector3VoidDelegate getInputWasdDelegate = null;

    #endregion

    #region Script
    private PlayerStateMachine m_StateMachine = null;
    public PlayerStateMachine StateMachine
    { get { return m_StateMachine; } }

    private PlayerMove m_Move = null;
    public PlayerMove Move
    { get { return m_Move; } }

    private PlayerRotate m_Rotate = null;
    public PlayerRotate Rotate
    { get { return m_Rotate; } }

    private CameraTarget m_CameraTarget = null;
    public CameraTarget CameraTarget
    { get { return m_CameraTarget; } }

    private PlayerStatus m_Status = null;
    public PlayerStatus Status
    { get { return m_Status; } }

    private PlayerAnim m_Anim = null;
    public PlayerAnim Anim
    { get { return m_Anim; } }

    private PlayerAction m_Action = null;
    public PlayerAction Action
    { get { return m_Action; } }

    private PlayerAnimEvent m_AnimEvent = null;
    public PlayerAnimEvent AnimEvent
    { get { return m_AnimEvent; } }

    private PlayerCollider m_Collider = null;
    public PlayerCollider Collider
    { get { return m_Collider; } }

    private Weapon m_Weapon = null;
    public Weapon Weapon
    { get { return m_Weapon; } }

    #endregion 

    private void Awake()
    {
        m_StateMachine = this.GetComponent<PlayerStateMachine>();
        m_Move = this.GetComponent<PlayerMove>();
        m_Rotate = this.GetComponent<PlayerRotate>();
        m_Status = this.GetComponent<PlayerStatus>();
        m_Anim = this.GetComponent<PlayerAnim>();
        m_Action = this.GetComponent<PlayerAction>();
        m_AnimEvent = this.GetComponentInChildren<PlayerAnimEvent>();
        m_Collider = this.GetComponentInChildren<PlayerCollider>();
        m_Weapon = this.GetComponentInChildren<Weapon>();
        m_CameraTarget = this.GetComponentInChildren<CameraTarget>();
    }

    private void Start()
    {
        m_StateMachine.Init(OnEvadeCallback, OnJumpCallback, OnComboAttackCallback, OnNomalStateCallback,
                            OnNextAttackCallback, OnSmashCallbackCallback, OnElementBurstCallback, OnKnockdownCallback, 
                            OffKnockdownCallback, SetKnockdown, IsSteminaEnough, GetCurAnimTime, GetCanNextActionTime);

        m_Move.Init(OnMoveCallback, GetCamForward, GetCamRight, GetInputWasd, GetMoveSpeed,
                          GetMaxSlopeAngle, GetColliderRadius);

        m_Rotate.Init(GetPlayerMoveDir, GetCamForward, IsInputWasd);

        m_AnimEvent.Init(m_Weapon);

        m_Collider.Init(HitSlap, HitBite, HitTailAttack, HitTailAttackGround, HitHeadbutt, HitThunderPlane, HitWhirlwind,
                        HitSmallDragon);

        m_Weapon.Init(EnemyAttackCallback, GetCurDamage, GetCurElementIdx);
    }

    #region InputManagerCallback
    public void InputLeftClickCallback()
    {
        m_StateMachine.InputLeftClickCallback();
    }
    public void InputRightClickCallback()
    {
        m_StateMachine.InputRightClickCallback();
    }
    public void InputElementBurstCallback()
    {
        m_StateMachine.InputElementBurstCallback();
    }
    public void InputEvadeCallback(bool _isInputWASD)
    {
        m_StateMachine.InputEvadeCallback(_isInputWASD);
    }
    public void InputJumpCallback()
    {
        m_StateMachine.InputJumpCallback();
    }
    #endregion



    public Vector3 GetCamForward()
    {
        return getCamForwardDelegate();
    }
    public Vector3 GetCamRight()
    {
        return getCamRightDelegate();
    }
    public Vector3 GetInputWasd()
    {
        return getInputWasdDelegate();
    }
    public Vector3 GetPlayerMoveDir()
    {
        return m_Move.MoveDir;
    }

    public Vector3 GetPos()
    {
        return this.transform.position;
    }





    #region PlayerMove
    private void OnMoveCallback(Vector3 _velocity)
    {
        m_Anim.SetOnMoveAnim(_velocity, GetBaseMoveSpeed(), GetCurMoveSpeed());
    }
    private float GetMoveSpeed()
    {
        return m_Status.CurMoveSpeed;
    }
    private float GetBaseMoveSpeed()
    {
        return m_Status.BaseMoveSpeed;
    }
    private float GetCurMoveSpeed()
    {
        return m_Status.CurMoveSpeed;
    }
    private float GetMaxSlopeAngle()
    {
        return m_Status.MaxSlopeAngle;
    }
    private float GetColliderRadius()
    {
        return m_Collider.GetRadius();
    }
    #endregion

    #region PlayerRotate
    public bool IsInputWasd()
    {
        if (GetInputWasd() != Vector3.zero)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region PlayerAction
    // 특정 상태가 되었을 때 각 클래스에서 처리는 그 클래스 내부에서 하도록 구현.
    // Player에서 직접 PlayerStatus.Set 어쩌고 하는거보다 각자 클래스에게 맞겨버리기.

    // 20221220 : 조건처리가 필요한 경우 여기서 처리해야 할 것 같음. 
    //            그럼 각자 클래스에 맞기는게 아니라 필요한 함수만 호출하는 식이 됨.
    //            어떻게 수정할지 생각해보기

    // NomalState가 됬을 때 
    private void OnNomalStateCallback()
    {
        m_Status.OnNomalStateCallback();
        m_Move.OnNomalStateCallback();
        m_Rotate.OnNomalStateCallback();
        m_Anim.OnNomalStateCallback();
    }

    // ComboAttackState가 됬을 때
    private void OnComboAttackCallback()
    {
        m_Status.OnComboAttackStateCallback();
        m_Move.OnComboAttackStateCallback();
        m_Rotate.OnComboAttackStateCallback();
        m_Anim.OnComboAttackStateCallback();
    }

    // Evade를 사용했을 때 처리
    private void OnEvadeCallback()
    {
        m_Status.OnEvadeStateCallback();
        m_Move.OnEvadeStateCallback();
        m_Rotate.OnEvadeStateCallback();
        m_Anim.OnEvadeStateCallback();
        m_Collider.OnEvadeStateCallback(m_Status.EvadeStartTime, m_Status.EvadeDurationTime);
    }

    private void OnJumpCallback()
    {
        m_Move.OnJumpCallback();
        m_Status.OnJumpCallback();
        m_Rotate.OnJumpCallback();
        m_Anim.OnJumpCallback();
        m_Collider.OnJumpCallback(m_Status.JumpStartTime, m_Status.JumpDurationTime);
    }

    private void OnNextAttackCallback()
    {
        m_Move.OnNextAttackCallback();
        m_Rotate.OnNextAttackCallback();
        m_Anim.OnNextAttackCallback();
    }

    private void OnSmashCallbackCallback()
    {
        m_Move.OnSmashCallback();
        m_Rotate.OnSmashCallback();
        m_Anim.OnSmashCallback();
    }

    private void OnElementBurstCallback()
    {
        m_Move.OnElementBurstCallback();
        m_Rotate.OnElementBurstCallback();
        m_Anim.OnElementBurstCallback();
        m_Action.OnElementBurstCallback(m_Status.CurElementIdx, m_Status.ElementStack,
                                                m_Status.ElementBurstDamage);
    }

    private void OnKnockdownCallback()
    {
        m_Collider.OnKnockdownCallback();
    }
    private void OffKnockdownCallback()
    {
        m_Collider.OffKnockdownCallback();
    }

    private void SetKnockback(Vector3 _fromKnockbackPos)
    {
        m_Status.OnKnockbackCallback();
        m_Move.OnKnockbackCallback(_fromKnockbackPos);
        m_Rotate.OnKnockbackCallback();
        m_Anim.OnKnockbackCallback();
        m_StateMachine.SetKnockback();
    }

    private void SetKnockdown(Vector3 _fromKnockdownPos)
    {
        m_Status.OnKnockdownCallback();
        m_Move.OnKnockdownCallback(_fromKnockdownPos);
        m_Rotate.OnKnockdownCallback();
        m_Anim.OnKnockdownCallback();
        m_StateMachine.SetKnockdown();
    }


    // ElementStack 관련
    private void PlusElementStackCallback()
    {
        m_Status.PlusElementStack();
    }
    private bool IsElementStackEmpty()
    {
        return m_Status.IsElementStackEmpty();
    }

    private bool IsSteminaEnough()
    {
        return m_Status.IsSteminaEnough();
    }

    // 공격 시간 관련
    private float GetCanNextActionTime()
    {
        return m_Status.CanNextActionTime;
    }

    private float GetCurAnimTime()
    {
        return m_Anim.GetCurAnimTime();
    }

    #endregion

    #region Weapon

    private void EnemyAttackCallback()
    {
        m_Status.EnemyAttackCallback();
    }

    private float GetCurDamage()
    {
        return m_Status.CurDamage;
    }

    private int GetCurElementIdx()
    {
        return m_Status.CurElementIdx;
    }
    #endregion

    #region Boss_Usurper

    private void HitSlap(Vector3 _slapPos)
    {
        SetKnockdown(_slapPos);

        // 체력깎고 등등 
    }

    private void HitBite(Vector3 _bitePos)
    {
        SetKnockback(_bitePos);

        // 체력깎고 등등 
    }

    private void HitTailAttack(Vector3 _tailAttackPos)
    {
        SetKnockback(_tailAttackPos);
    }

    private void HitTailAttackGround(Vector3 _tailAttackGroundPos)
    {
        SetKnockdown(_tailAttackGroundPos);
    }

    private void HitHeadbutt(Vector3 _headbuttPos)
    {
        SetKnockdown(_headbuttPos);
    }

    private void HitThunderPlane(Vector3 _planePos)
    {

    }

    private void HitWhirlwind(Vector3 _windPos)
    {

    }

    #endregion

    #region Boss_SoulEater

    private void HitSmallDragon(Vector3 _boomPos)
    {
        SetKnockback(_boomPos);
    }

    #endregion


    public void Init(Vector3VoidDelegate _getCamForwardCallback, Vector3VoidDelegate _getCamRightCallback,
                    Vector3VoidDelegate _getInputWasdCallback)
    {
        getCamForwardDelegate = _getCamForwardCallback;
        getCamRightDelegate = _getCamRightCallback;
        getInputWasdDelegate = _getInputWasdCallback;
    }
}
