using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public delegate void VoidVoidDelegate();
    public delegate float FloatVoidDelegate();
    public delegate int IntVoidDelegate();
    private VoidVoidDelegate attackEnemyDelegate = null;
    private FloatVoidDelegate getDamageDelegate = null;
    private IntVoidDelegate getElementIdxDelegate = null;

    private BoxCollider m_WeaponCollider = null;
    private MeshRenderer m_MeshRenderer = null;

    private void Awake()
    {
        m_WeaponCollider = GetComponent<BoxCollider>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        m_WeaponCollider.enabled = false;
        m_MeshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("ENEMY"))
        {
            // ���߿� Enemy �� ����ȣ��޴� �Լ� �������ֱ�
            _other.GetComponent<Enemy>().Attacked(getDamageDelegate(), getElementIdxDelegate());
            attackEnemyDelegate();
        }
    }

    public void OnCollider()
    {
        m_WeaponCollider.enabled = true;
        m_MeshRenderer.enabled = true;
    }

    public void OffCollider()
    {
        m_WeaponCollider.enabled = false;
        m_MeshRenderer.enabled = false;
    }

    public void Init(VoidVoidDelegate _attackEnemyCallback, FloatVoidDelegate _getDamageCallback, IntVoidDelegate _getElementIdxCallback)
    {
        attackEnemyDelegate = _attackEnemyCallback;
        getDamageDelegate = _getDamageCallback;
        getElementIdxDelegate = _getElementIdxCallback;
    }
}
