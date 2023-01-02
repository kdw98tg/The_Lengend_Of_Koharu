using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator m_Animator = null;

    private void Awake()
    {
        m_Animator = this.GetComponentInChildren<Animator>();
    }

    
    public void SetOnMoveAnim(Vector3 _velocity, float _baseMoveSpeed, float _curMoveSpeed)
    {
        // PlayerMove ���� velocity �޾ƿͼ� Move �ִϸ��̼� �ӵ��� ����
        float moveAnimSpeed = GetMoveAnimSpeed(_velocity, _baseMoveSpeed, _curMoveSpeed);
        m_Animator.SetFloat("velocity", moveAnimSpeed);
    }

    public void OnJumpCallback()
    {
        m_Animator.SetTrigger("onJump");
    }

    public void OnNomalStateCallback()
    {
        m_Animator.SetTrigger("onNomal");
    }
    public void OnComboAttackStateCallback()
    {
        m_Animator.SetTrigger("onComboAttack");
    }

    public void OnNextAttackCallback()
    {
        m_Animator.SetTrigger("onNextAttack");
    }

    public void OnSmashCallback()
    {
        m_Animator.SetTrigger("onSmash");
    }

    public void OnElementBurstCallback()
    {
        m_Animator.SetTrigger("onElementBurst");
    }

    public void OnEvadeStateCallback()
    {
        m_Animator.SetTrigger("onEvade");
    }

    public void OnKnockbackCallback()
    {
        m_Animator.StopPlayback();
        m_Animator.SetTrigger("onKnockback");
    }

    public void OnKnockdownCallback()
    {
        m_Animator.StopPlayback();
        m_Animator.SetTrigger("onKnockdown");
    }

    // PlayerAction ���� �� ������ ������ �ð��� üũ�ϱ� ���� AnimTime�� �޾ư��� �Լ�
    public float GetCurAnimTime()
    {
        float curAnimTime = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        return curAnimTime;
    }

    // PlayerAnim ������ ������ �ִϸ��̼� ��ӽð��� �������ִ� �Լ�. 
    // 20221220 ��켮 : �����ؾ���. ���� ������� ������
    private void SetActionAnimSpeed(float _baseActionSpeed, float _curActionSpeed)
    {
        float actionAnimSpeed = 1f;
        m_Animator.SetFloat("actionSpeed", actionAnimSpeed);
    }

    // ĳ���� �̵��ӵ��� ���ϸ� �ִϸ��̼� ����ӵ��� �������ش�.
    private float GetMoveAnimSpeed(Vector3 _velocity, float _baseMoveSpeed, float _curMoveSpeed)
    {
        float moveAnimSpeed = _velocity == Vector3.zero ? 0f
            : 1.3f + (_curMoveSpeed - _baseMoveSpeed) * 0.08f;
        return moveAnimSpeed;
    }
}
