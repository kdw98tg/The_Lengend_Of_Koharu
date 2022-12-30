using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    protected BoxCollider m_Collider = null;
    protected Renderer m_Renderer = null;

    protected void Awake()
    {
        m_Collider = GetComponent<BoxCollider>();
        m_Renderer = GetComponent<Renderer>();
    }

    public void OnCollider()
    {
        m_Collider.enabled = true;
        m_Renderer.enabled = true;
    }
    public void OffCollider()
    {
        m_Collider.enabled = false;
        m_Renderer.enabled = false;
    }
    public BoxCollider GetCollider()
    {
        return m_Collider;
    }
}
