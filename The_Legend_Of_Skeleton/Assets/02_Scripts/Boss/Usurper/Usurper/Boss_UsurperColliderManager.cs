using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_UsurperColliderManager : MonoBehaviour
{
    private SlapCollider m_SlapCollider = null;
    private BiteCollider m_BiteCollider = null;
    private HeadbuttCollider m_HeadbuttCollider = null;
    private TailAttackCollider m_TailAttackCollider = null;
    private TailAttackGroundCollider m_TailAttackGroundCollider = null;
    private FrontHandCollider[] m_FrontHands = null;

    private void Awake()
    {
        m_SlapCollider = this.GetComponentInChildren<SlapCollider>();
        m_BiteCollider = this.GetComponentInChildren<BiteCollider>();
        m_HeadbuttCollider = this.GetComponentInChildren<HeadbuttCollider>();
        m_TailAttackCollider = this.GetComponentInChildren<TailAttackCollider>();
        m_TailAttackGroundCollider = this.GetComponentInChildren<TailAttackGroundCollider>();
        m_FrontHands = this.GetComponentsInChildren<FrontHandCollider>();
    }
    private void Start()
    {
        m_SlapCollider.OffCollider();
        m_BiteCollider.OffCollider();
        m_HeadbuttCollider.OffCollider();
        m_TailAttackCollider.OffCollider();
        m_TailAttackGroundCollider.OffCollider();
    }


    public void OnSlapCollider()
    {
        m_SlapCollider.OnCollider();
    }
    public void OffSlapCollider()
    {
        m_SlapCollider.OffCollider();
    }

    public void OnBiteCollider()
    {
        m_BiteCollider.OnCollider();
    }
    public void OffBiteCollider()
    {
        m_BiteCollider.OffCollider();
    }

    public void OnHeadbuttCollider()
    {
        m_HeadbuttCollider.OnCollider();
    }
    public void OffHeadbuttCollider()
    {
        m_HeadbuttCollider.OffCollider();
    }

    public void OnTailAttackCollider()
    {
        m_TailAttackCollider.OnCollider();
    }
    public void OffTailAttackCollider()
    {
        m_TailAttackCollider.OffCollider();
    }

    public void OnTailAttackGroundCollider()
    {
        m_TailAttackGroundCollider.OnCollider();
    }
    public void OffTailAttackGroundCollider()
    {
        m_TailAttackGroundCollider.OffCollider();
    }

    public void OnFrontHandCollider()
    {
        for (int i = 0; i < m_FrontHands.Length; ++i)
        {
            m_FrontHands[i].OnCollider();
        }
    }
    public void OffFrontHandCollider()
    {
        for (int i = 0; i < m_FrontHands.Length; ++i)
        {
            m_FrontHands[i].OffCollider();
        }
    }




}
