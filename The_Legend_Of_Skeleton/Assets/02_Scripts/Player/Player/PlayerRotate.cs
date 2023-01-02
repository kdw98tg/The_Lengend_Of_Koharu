using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotate : MonoBehaviour
{
    public delegate Vector3 Vector3VoidDelegate();
    public delegate bool BoolVoidDelegate();
    private Vector3VoidDelegate getPlayerMoveDirDelegate = null;
    private Vector3VoidDelegate getCamForwardDelegate = null;
    private BoolVoidDelegate isInputWasdDelegate = null;
    private bool mb_CanRotate = true;
    public bool CanRotate
    { set { mb_CanRotate = value; } }

    private Vector3 postDir = Vector3.zero;

    // PlayerMove�� ����ȭ�ϱ� ���� FixedUpdate
    // ����ȭ �ϴ� ��� : 1. LateUpdate    2. �÷��̾� Ŭ�������� ���������� ȣ��
    private void LateUpdate()
    {
        if (!mb_CanRotate) return;

        SetForward(isInputWasdDelegate(), getPlayerMoveDirDelegate(), getCamForwardDelegate());
    }

    public void OnNomalStateCallback()
    {
        mb_CanRotate = true;
    }
    public void OnComboAttackStateCallback()
    {
        mb_CanRotate = false;
    }
    public void OnEvadeStateCallback()
    {
        SetForward(isInputWasdDelegate(), getPlayerMoveDirDelegate(), getCamForwardDelegate());
        mb_CanRotate = false;
    }
    public void OnKnockbackCallback()
    {
        mb_CanRotate = false;
    }
    public void OnKnockdownCallback()
    {
        mb_CanRotate = false;
    }
    public void OnJumpCallback()
    {
        mb_CanRotate = true;
    }
    public void OnNextAttackCallback()
    {
        SetForward(isInputWasdDelegate(), getPlayerMoveDirDelegate(), getCamForwardDelegate());
    }
    public void OnSmashCallback()
    {
        SetForward(isInputWasdDelegate(), getPlayerMoveDirDelegate(), getCamForwardDelegate());
    }
    public void OnElementBurstCallback()
    {
        SetForward(isInputWasdDelegate(), getPlayerMoveDirDelegate(), getCamForwardDelegate());
    }

    private void SetForward(bool _isInputWasd, Vector3 _moveDir, Vector3 _camForward)
    {
        // Input�� zero �� �ƴ϶�� �����̴� ������ �ٶ󺻴�.
        if (_isInputWasd)
        {
            this.transform.forward = _moveDir;
        }
        else    // Input�� zero��� ī�޶� Forward2D ������ �ٶ󺻴�.
        {
            Vector3 camForward2D = new Vector3(_camForward.x, 0f, _camForward.z).normalized;
            this.transform.forward = camForward2D;
        }

        postDir = _moveDir;
    }

    public void Init(Vector3VoidDelegate _getPlayerMoveDir, Vector3VoidDelegate _getCamForward,
                        BoolVoidDelegate _getInputWasd)
    {
        getPlayerMoveDirDelegate = _getPlayerMoveDir;
        isInputWasdDelegate = _getInputWasd;
        getCamForwardDelegate = _getCamForward;
    }
}