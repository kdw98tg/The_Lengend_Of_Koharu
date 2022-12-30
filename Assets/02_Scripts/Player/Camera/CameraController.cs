using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public delegate float FloatVoidDelegate();
    private FloatVoidDelegate getAxisXDelegate = null;
    private FloatVoidDelegate getAxisYDelegate = null;
    private FloatVoidDelegate getAxisMouseWheelDelegate = null;

    #region Inspector
    [SerializeField] private CameraTarget m_CameraTarget = null;
    [SerializeField] float m_MinDistance = 2f;
    [SerializeField] float m_MaxDistance = 10f;
    [SerializeField] float m_WheelSpeed = 500f;
    [SerializeField] float m_XRotSpeed = 75f;
    [SerializeField] float m_YRotSpeed = 75f;
    [SerializeField] float m_YMinLimit = 5f;
    [SerializeField] float m_YMaxLimit = 75f;
    #endregion

    private float m_AxisX = 0f;
    private float m_AxisY = 0f;
    private float m_Distance = 0f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        m_Distance = Vector3.Distance(this.transform.position, m_CameraTarget.GetPosition());
        Vector3 angles = this.transform.eulerAngles;
        m_AxisX = angles.x;
        m_AxisY = angles.y;
    }

    private void Update()
    {
        if (m_CameraTarget == null) return;

        // InputManager에서 Axis 받아와서 회전
        m_AxisX += getAxisXDelegate() * m_XRotSpeed * Time.deltaTime;
        m_AxisY -= getAxisYDelegate() * m_YRotSpeed * Time.deltaTime;
        m_AxisY = ClampAngle(m_AxisY, m_YMinLimit, m_YMaxLimit);

        this.transform.rotation = Quaternion.Euler(m_AxisY, m_AxisX, 0f);

        // InputManager에서 마우스휠 Axis 받아와서 카메라거리 설정
        m_Distance -= getAxisMouseWheelDelegate() * m_WheelSpeed * Time.deltaTime;
        m_Distance = Mathf.Clamp(m_Distance, m_MinDistance, m_MaxDistance);
    }

    private void LateUpdate()
    {
        if (m_CameraTarget == null) return;

        this.transform.position = this.transform.rotation * new Vector3(0f, 0f, -m_Distance) + m_CameraTarget.GetPosition();
    }

    public Vector3 GetCamForward()
    {
        return this.transform.forward;
    }
    public Vector3 GetCamRight()
    {
        return this.transform.right;
    }

    public void Init(FloatVoidDelegate _getAxisX, FloatVoidDelegate _getAxisY,
                        FloatVoidDelegate _getAxisMouseWheel)
    {
        getAxisXDelegate = _getAxisX;
        getAxisYDelegate = _getAxisY;
        getAxisMouseWheelDelegate = _getAxisMouseWheel;
    }

    // Y값 최소,최대 설정
    private float ClampAngle(float _angle, float _min, float _max)
    {
        if (_angle < -360f)
        {
            _angle += 360f;
        }
        if (_angle > 360f)
        {
            _angle -= 360f;
        }
        return Mathf.Clamp(_angle, _min, _max);
    }
}
