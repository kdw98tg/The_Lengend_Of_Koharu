using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Move : MonoBehaviour
{
    protected Rigidbody m_Rb = null;
    protected Vector3 m_MoveDir = Vector3.zero;
    public Vector3 MoveDir
    { get { return m_MoveDir; } }

    protected bool mb_CanMove = true;

    protected virtual void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        if (!mb_CanMove) return;

        OnMove();
    }
    protected abstract void OnMove();
}
