using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public delegate void VoidVoidDelegate();
    private VoidVoidDelegate inputLeftClickDelegate = null;
    private VoidVoidDelegate inputRightClickDelegate = null;
    private VoidVoidDelegate inputElementBurstDelegate = null;
    private VoidVoidDelegate inputEvadeDelegate = null;
    private VoidVoidDelegate inputJumpDelegate = null;

    private float m_AxisX = 0f;
    public float AxisX
    { get { return m_AxisX; } }

    private float m_AxisY = 0f;
    public float AxisY
    { get { return m_AxisY; } }

    private float m_AxisMouseWheel = 0f;
    public float AxisMouseWheel
    { get { return m_AxisMouseWheel; } }

    private Vector2 m_InputWasd = Vector2.zero;
    public Vector2 InputWasd
    { get { return m_InputWasd; } }

    private void Update()
    {
        m_AxisX = Input.GetAxis("Mouse X");
        m_AxisY = Input.GetAxis("Mouse Y");
        m_AxisMouseWheel = Input.GetAxis("Mouse ScrollWheel");
    }

    private void SetInputWasd(InputAction.CallbackContext _context)
    {
        m_InputWasd = _context.ReadValue<Vector2>();
    }
    public void InputLeftClick(InputAction.CallbackContext _conext)
    {
        inputLeftClickDelegate?.Invoke();
    }
    public void InputRightClick(InputAction.CallbackContext _context)
    {
        inputRightClickDelegate?.Invoke();
    }
    public void InputElementBurst(InputAction.CallbackContext _context)
    {
        inputElementBurstDelegate?.Invoke();
    }
    public void InputEvade(InputAction.CallbackContext _context)
    {
        inputEvadeDelegate?.Invoke();
    }
    public void InputJump(InputAction.CallbackContext _context)
    {
        inputJumpDelegate?.Invoke();
    }


    // GameManager에서 세팅해줌. 뭐가 연결될지 모르지만 여튼 버튼누르면 연결된게 작동함.
    public void Init(VoidVoidDelegate _inputLeftClickCallback, VoidVoidDelegate _inputRightClickCallback,
                      VoidVoidDelegate _inputElementBurstCallback, VoidVoidDelegate _inputEvadeCallback,
                      VoidVoidDelegate _inputJumpCallback)
    {
        InputActionMap playerActionMap = this.GetComponent<PlayerInput>().actions.FindActionMap("PlayerActions");
        // Player Move
        InputAction wasdAction = playerActionMap.FindAction("Move");

        // Player Action
        InputAction leftClickAction = playerActionMap.FindAction("LeftClick");
        InputAction rightClickAction = playerActionMap.FindAction("RightClick");
        InputAction elementBurstAction = playerActionMap.FindAction("ElementBurst");
        InputAction evadeAction = playerActionMap.FindAction("Evade");
        InputAction jumpAction = playerActionMap.FindAction("Jump");

        wasdAction.started += SetInputWasd;
        wasdAction.performed += SetInputWasd;
        wasdAction.canceled += SetInputWasd;

        leftClickAction.started += InputLeftClick;
        rightClickAction.started += InputRightClick;
        elementBurstAction.started += InputElementBurst;
        evadeAction.started += InputEvade;
        jumpAction.started += InputJump;

        inputLeftClickDelegate = _inputLeftClickCallback;
        inputRightClickDelegate = _inputRightClickCallback;
        inputElementBurstDelegate = _inputElementBurstCallback;
        inputEvadeDelegate = _inputEvadeCallback;
        inputJumpDelegate = _inputJumpCallback;
    }
}
