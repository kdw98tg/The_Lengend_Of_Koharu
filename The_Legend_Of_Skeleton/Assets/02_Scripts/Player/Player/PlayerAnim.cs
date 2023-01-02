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
        // PlayerMove 에서 velocity 받아와서 Move 애니메이션 속도를 설정
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

    // PlayerAction 에서 각 상태의 끝나는 시간을 체크하기 위한 AnimTime을 받아가는 함수
    public float GetCurAnimTime()
    {
        float curAnimTime = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        return curAnimTime;
    }

    // PlayerAnim 내에서 실제로 애니메이션 배속시간을 설정해주는 함수. 
    // 20221220 양우석 : 수정해야함. 아직 배속조절 안했음
    private void SetActionAnimSpeed(float _baseActionSpeed, float _curActionSpeed)
    {
        float actionAnimSpeed = 1f;
        m_Animator.SetFloat("actionSpeed", actionAnimSpeed);
    }

    // 캐릭터 이동속도가 변하면 애니메이션 재생속도를 보정해준다.
    private float GetMoveAnimSpeed(Vector3 _velocity, float _baseMoveSpeed, float _curMoveSpeed)
    {
        float moveAnimSpeed = _velocity == Vector3.zero ? 0f
            : 1.3f + (_curMoveSpeed - _baseMoveSpeed) * 0.08f;
        return moveAnimSpeed;
    }
}
