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

    // PlayerMove와 동기화하기 위해 FixedUpdate
    // 동기화 하는 방법 : 1. LateUpdate    2. 플레이어 클래스에서 순차적으로 호출
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
        // Input이 zero 가 아니라면 움직이는 방향을 바라본다.
        if (_isInputWasd)
        {
            this.transform.forward = _moveDir;
        }
        else    // Input이 zero라면 카메라 Forward2D 방향을 바라본다.
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