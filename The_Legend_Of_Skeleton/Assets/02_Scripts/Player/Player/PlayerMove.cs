using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : Move
{
    public delegate void VoidVector3Delegate(Vector3 _value);
    public delegate Vector3 Vector3VoidDelegate();
    public delegate float FloatVoidDelegate();
    private VoidVector3Delegate onMoveDelegate = null;
    private Vector3VoidDelegate getCamForwardDelegate = null;
    private Vector3VoidDelegate getCamRightDelegate = null;
    private Vector3VoidDelegate getInputWasdDelegate = null;
    private FloatVoidDelegate getMoveSpeedDelegate = null;
    private FloatVoidDelegate getMaxSlopeAngleDelegate = null;
    private FloatVoidDelegate getColliderRadiusDelegate = null;

    private bool mb_CanSetInputWasd = true;
    private float m_CheckGroundRayDistance = 2f;
    private int m_GroundLayer = 0;

    protected override void Awake()
    {
        base.Awake();
        m_GroundLayer = 1 << LayerMask.NameToLayer("GROUND");
    }

    protected override void FixedUpdate()
    {
        if (mb_CanSetInputWasd)
        {
            // WASD 인풋, 카메라Forward, Right 를 받아와서 MoveDir을 설정한다.
            SetMoveDirWithInput(getInputWasdDelegate(), getCamForwardDelegate(), getCamRightDelegate());
        }
        base.FixedUpdate();
    }

    public void OnNomalStateCallback()
    {
        mb_CanMove = true;
        mb_CanSetInputWasd = true;
    }
    public void OnComboAttackStateCallback()
    {
        mb_CanMove = true;
        mb_CanSetInputWasd = false;
    }
    public void OnEvadeStateCallback()
    {
        mb_CanMove = true;
        mb_CanSetInputWasd = false;
        SetMoveDirWithInput(getInputWasdDelegate(), getCamForwardDelegate(), getCamRightDelegate());
    }
    public void OnJumpCallback()
    {
        mb_CanMove = true;
        mb_CanSetInputWasd = true;
    }
    public void OnKnockbackCallback(Vector3 _fromKnockbackPos)
    {
        mb_CanMove = true;
        mb_CanSetInputWasd = false;
        m_MoveDir = (this.transform.position - _fromKnockbackPos).normalized;
    }
    public void OnKnockdownCallback(Vector3 _fromtKnockdownPos)
    {
        mb_CanMove = true;
        mb_CanSetInputWasd = false;
        m_MoveDir = (this.transform.position - _fromtKnockdownPos).normalized;
    }

    public void OnNextAttackCallback()
    {
        SetMoveDirWithInput(getInputWasdDelegate(), getCamForwardDelegate(), getCamRightDelegate());
    }
    public void OnSmashCallback()
    {
        SetMoveDirWithInput(getInputWasdDelegate(), getCamForwardDelegate(), getCamRightDelegate());
    }
    public void OnElementBurstCallback()
    {
        SetMoveDirWithInput(getInputWasdDelegate(), getCamForwardDelegate(), getCamRightDelegate());
    }

    protected override void OnMove()
    {
        // 경사면에 있는지 확인
        RaycastHit hitInfo;
        var isOnSlope = IsSlope(out hitInfo, getMaxSlopeAngleDelegate());
        var isGrounded = IsGrounded();

        // 다음프레임의 플레이어 위치에서 노말각도를 미리 계산해 갈 수 있는지 없는지 계산. 못가면 Vector3.zero;
        var velocity = GetAngleBetweenGroundNormalAndVector3UpInTheNextFrame(m_MoveDir, getMoveSpeedDelegate(), getColliderRadiusDelegate())
                        <= getMaxSlopeAngleDelegate() ? m_MoveDir : Vector3.zero;

        var gravity = Vector3.down * Mathf.Abs(m_Rb.velocity.y);

        // 경사에 있으면 방향벡터 투영, 아래방향 벡터(중력) 0, 미끄러지지 않기위해 useGravity false
        if (isGrounded && isOnSlope)
        {
            velocity = AdjustMoveDirOnSlope(m_MoveDir, hitInfo.normal);
            gravity = Vector3.zero;
            m_Rb.useGravity = false;
        }
        else
        {
            m_Rb.useGravity = true;
        }
        m_Rb.velocity = (velocity * getMoveSpeedDelegate()) + gravity;

        // Player -> OnMoveCallback
        onMoveDelegate?.Invoke(m_MoveDir);
    }

    // 카메라의 forward, right를 받아와서 xz평면에 투영시켜 이동할 방향벡터를 구한다.
    private void SetMoveDirWithInput(Vector2 _input, Vector3 _camForward, Vector3 _camRight)
    {
        var camForward2D = new Vector3(_camForward.x, 0f, _camForward.z).normalized;
        var camRight2D = new Vector3(_camRight.x, 0f, _camRight.z).normalized;

        m_MoveDir = ((camForward2D * _input.y) + (camRight2D * _input.x)).normalized;
    }

    // 발 앞뒤로 검사하기
    // 경사면 좀 더 자연스럽게 고민해보기
    private bool IsSlope(out RaycastHit _hitInfo, float _maxSlopeAngle)
    {
        // 바닥으로 레이를 쏴서 Vector3.up과 hitPoint의 노말벡터의 각도를 비교.
        // 각도가 0이 아니고 최고 이동가능각도 이하라면 true, 아니면 false
        if (Physics.Raycast(this.transform.position, Vector3.down, out _hitInfo,
                        m_CheckGroundRayDistance, m_GroundLayer))
        {
            var angle = Vector3.Angle(Vector3.up, _hitInfo.normal);
            return (angle != 0f) && (angle <= _maxSlopeAngle);
        }
        return false;
    }

    // Check박스가 땅과 접하고있다면 true
    private bool IsGrounded()
    {
        var boxSize = new Vector3(1f, 0.4f, 1f);

        // 플레이어 위치에서 박스사이즈 y 길이의 절반만큼 아래로 내려줌 -> 발 아래쪽에 0.4f 높이만큼 검사
        return Physics.CheckBox((this.transform.position - new Vector3(0f, (boxSize.y / 2), 0f)),
                                boxSize, Quaternion.identity, m_GroundLayer);
    }

    // 다음프레임 위치를 계산해서 갈 수 있는지 없는지 미리 계산하기위한 함수
    private float GetAngleBetweenGroundNormalAndVector3UpInTheNextFrame(Vector3 _moveDir, float _moveSpeed, float _colliderRadius)
    {
        // 다음프레임의 캐릭터 제일 앞 위치 = 내위치 + 콜라이더 radius/2 + 이동속도 * fxiedDeltaTime(fixedUpdate)
        var frontPositionInTheNextFrame = (this.transform.position + Vector3.up + (this.transform.forward * _colliderRadius / 2))
                                           + (_moveDir * _moveSpeed * Time.fixedDeltaTime);
        RaycastHit hitInfo;
        if (Physics.Raycast(frontPositionInTheNextFrame, Vector3.down, out hitInfo, m_CheckGroundRayDistance, m_GroundLayer))
        {
            return Vector3.Angle(Vector3.up, hitInfo.normal);
        }
        return 0f;
    }

    // 20221218 양우석 : 미끄럼벡터 투영. 나중에 직접 구현해볼 것
    private Vector3 AdjustMoveDirOnSlope(Vector3 _moveDir, Vector3 _normal)
    {
        if (_moveDir != Vector3.zero)
        {
            return Vector3.ProjectOnPlane(_moveDir, _normal).normalized;
        }
        return Vector3.zero;
    }



    public void Init(VoidVector3Delegate _onMoveCallback, Vector3VoidDelegate _getCamForward,
                            Vector3VoidDelegate _getCamRight, Vector3VoidDelegate _getInputWasd,
                            FloatVoidDelegate _getMoveSpeed, FloatVoidDelegate _getMaxSlopeAngle,
                            FloatVoidDelegate _getColliderRadius)
    {
        onMoveDelegate = _onMoveCallback;
        getCamForwardDelegate = _getCamForward;
        getCamRightDelegate = _getCamRight;
        getInputWasdDelegate = _getInputWasd;
        getMoveSpeedDelegate = _getMoveSpeed;
        getMaxSlopeAngleDelegate = _getMaxSlopeAngle;
        getColliderRadiusDelegate = _getColliderRadius;
    }
}
