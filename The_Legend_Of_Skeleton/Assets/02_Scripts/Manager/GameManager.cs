using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ½Ì±ÛÅæÀ¸·Î ¸¸µé¾î¾ß ÇÔ
public class GameManager : MonoBehaviour
{
    [SerializeField] private Player m_Player = null;
    [SerializeField] private CameraController m_CameraController = null;
    private InputManager m_InputManager = null;

    private void Awake()
    {
        m_InputManager = GetComponentInChildren<InputManager>();
    }

    private void Start()
    {
        m_InputManager.Init(InputLeftClickCallback, InputRightClickCallback, InputElementBurstCallback,
                            InputEvadeCallback, InputJumpCallback);

        m_CameraController.Init(GetAxisX, GetAxisY, GetAxisMouseWheel);

        m_Player.Init(GetCamForward, GetCamRight, GetInputWasd);
    }

    #region InputCallback
    private void InputLeftClickCallback()
    {
        m_Player.InputLeftClickCallback();
    }
    private void InputRightClickCallback()
    {
        m_Player.InputRightClickCallback();
    }
    private void InputElementBurstCallback()
    {
        m_Player.InputElementBurstCallback();
    }
    private void InputEvadeCallback()
    {
        bool isInputWasd = !(GetInputWasd() == Vector3.zero);
        m_Player.InputEvadeCallback(isInputWasd);
    }
    private void InputJumpCallback()
    {
        m_Player.InputJumpCallback();
    }
    #endregion

    private float GetAxisX()
    {
        return m_InputManager.AxisX;
    }
    private float GetAxisY()
    {
        return m_InputManager.AxisY;
    }
    private float GetAxisMouseWheel()
    {
        return m_InputManager.AxisMouseWheel;
    }
    private Vector3 GetInputWasd()
    {
        return m_InputManager.InputWasd;
    }
    private Vector3 GetCamForward()
    {
        return m_CameraController.GetCamForward();
    }
    private Vector3 GetCamRight()
    {
        return m_CameraController.GetCamRight();
    }

}
