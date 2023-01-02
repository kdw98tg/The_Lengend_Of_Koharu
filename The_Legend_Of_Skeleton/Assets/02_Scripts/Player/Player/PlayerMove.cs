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
            // WASD ��ǲ, ī�޶�Forward, Right �� �޾ƿͼ� MoveDir�� �����Ѵ�.
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
        // ���鿡 �ִ��� Ȯ��
        RaycastHit hitInfo;
        var isOnSlope = IsSlope(out hitInfo, getMaxSlopeAngleDelegate());
        var isGrounded = IsGrounded();

        // ������������ �÷��̾� ��ġ���� �븻������ �̸� ����� �� �� �ִ��� ������ ���. ������ Vector3.zero;
        var velocity = GetAngleBetweenGroundNormalAndVector3UpInTheNextFrame(m_MoveDir, getMoveSpeedDelegate(), getColliderRadiusDelegate())
                        <= getMaxSlopeAngleDelegate() ? m_MoveDir : Vector3.zero;

        var gravity = Vector3.down * Mathf.Abs(m_Rb.velocity.y);

        // ��翡 ������ ���⺤�� ����, �Ʒ����� ����(�߷�) 0, �̲������� �ʱ����� useGravity false
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

    // ī�޶��� forward, right�� �޾ƿͼ� xz��鿡 �������� �̵��� ���⺤�͸� ���Ѵ�.
    private void SetMoveDirWithInput(Vector2 _input, Vector3 _camForward, Vector3 _camRight)
    {
        var camForward2D = new Vector3(_camForward.x, 0f, _camForward.z).normalized;
        var camRight2D = new Vector3(_camRight.x, 0f, _camRight.z).normalized;

        m_MoveDir = ((camForward2D * _input.y) + (camRight2D * _input.x)).normalized;
    }

    // �� �յڷ� �˻��ϱ�
    // ���� �� �� �ڿ������� ����غ���
    private bool IsSlope(out RaycastHit _hitInfo, float _maxSlopeAngle)
    {
        // �ٴ����� ���̸� ���� Vector3.up�� hitPoint�� �븻������ ������ ��.
        // ������ 0�� �ƴϰ� �ְ� �̵����ɰ��� ���϶�� true, �ƴϸ� false
        if (Physics.Raycast(this.transform.position, Vector3.down, out _hitInfo,
                        m_CheckGroundRayDistance, m_GroundLayer))
        {
            var angle = Vector3.Angle(Vector3.up, _hitInfo.normal);
            return (angle != 0f) && (angle <= _maxSlopeAngle);
        }
        return false;
    }

    // Check�ڽ��� ���� ���ϰ��ִٸ� true
    private bool IsGrounded()
    {
        var boxSize = new Vector3(1f, 0.4f, 1f);

        // �÷��̾� ��ġ���� �ڽ������� y ������ ���ݸ�ŭ �Ʒ��� ������ -> �� �Ʒ��ʿ� 0.4f ���̸�ŭ �˻�
        return Physics.CheckBox((this.transform.position - new Vector3(0f, (boxSize.y / 2), 0f)),
                                boxSize, Quaternion.identity, m_GroundLayer);
    }

    // ���������� ��ġ�� ����ؼ� �� �� �ִ��� ������ �̸� ����ϱ����� �Լ�
    private float GetAngleBetweenGroundNormalAndVector3UpInTheNextFrame(Vector3 _moveDir, float _moveSpeed, float _colliderRadius)
    {
        // ������������ ĳ���� ���� �� ��ġ = ����ġ + �ݶ��̴� radius/2 + �̵��ӵ� * fxiedDeltaTime(fixedUpdate)
        var frontPositionInTheNextFrame = (this.transform.position + Vector3.up + (this.transform.forward * _colliderRadius / 2))
                                           + (_moveDir * _moveSpeed * Time.fixedDeltaTime);
        RaycastHit hitInfo;
        if (Physics.Raycast(frontPositionInTheNextFrame, Vector3.down, out hitInfo, m_CheckGroundRayDistance, m_GroundLayer))
        {
            return Vector3.Angle(Vector3.up, hitInfo.normal);
        }
        return 0f;
    }

    // 20221218 ��켮 : �̲������� ����. ���߿� ���� �����غ� ��
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
