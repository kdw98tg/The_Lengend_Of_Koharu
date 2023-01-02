using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Usurper : BossState
{
    public Fight_Usurper(Boss _boss) : base("Fight", _boss) { }

    private bool mb_IsAttacking = false;
    private bool mb_IsTracing = false;
    private bool mb_IsTurning = false;
    

    public override void Action()
    {
        // 플레이어의 위치, 방향 판단 후 다음 행동 결정

        // 0. 플레이어를 바라보기
        // 1. 몸통박치기 
        // 2. 물기 공격
        // 3. 브레스
        // 4. 울부짖으며 범위공격

        // 플레이어를 전방으로 바라보고 있지 않는다면 회전 액션
        // 플레이어를 전방으로 바라보고 있다면

        // 거리가 가까우면
        // 40% 확률로 물기공격
        // 40% 확률로 몸통박치기
        // 20% 확률로 범위공격

        // 거리가 멀면
        // 30% 확률로 브레스
        // 70% 확률로 가까이 따라오기

        if (mb_IsAttacking) return;
        if (mb_IsTracing)
        {
            (m_Boss as Boss_UsurperStateMachine).TraceTarget();
            return;
        }
        if (mb_IsTurning)
        {
            (m_Boss as Boss_UsurperStateMachine).LookAtTarget();
            return;
        }


        


        float distance = Vector3.Distance(m_Boss.PlayerTr.position, m_Boss.transform.position);
        Vector3 dir = (m_Boss.PlayerTr.position - m_Boss.transform.position).normalized;
        float angle = Vector3.Angle(m_Boss.transform.forward, dir);


        // 플레이어가 전방이 아니라면
        if (angle >= (m_Boss as Boss_UsurperStateMachine).ForwardAngle)
        {
            // 각도 계산해서 랜덤 꼬리치기
            int randomIdx = Random.Range(0, 3);
            if (Mathf.Abs(angle) >= 160f && randomIdx == 0)
            {
                Debug.Log("꼬리쳐용");
                (m_Boss as Boss_UsurperStateMachine).TailAttack();
                mb_IsAttacking = true;
            }
            else
            {
                mb_IsTurning = true;
            }
            return;
        }

        if((m_Boss as Boss_UsurperStateMachine).DownAttackDistance >= distance)
        {
            int randomIdx = Random.Range(0, 10);
            if(randomIdx <= 7)
            {
                // 날려보내기
                (m_Boss as Boss_UsurperStateMachine).Whirlwind();
                return;
            }
        }

        // 근접 공격범위 안이라면
        // 40% 확률로 물기공격
        // 40% 확률로 내려찍기
        // 20% 확률로 범위공격
        if ((m_Boss as Boss_UsurperStateMachine).MeleeAttackDistance >= distance)
        {
            int randomIdx = Random.Range(0, 10);
            Debug.Log(randomIdx);
            if (randomIdx <= 3)
            {
                // 물기공격
                Debug.Log("물어용!");
                (m_Boss as Boss_UsurperStateMachine).Bite();
            }
            else if (randomIdx <= 7)
            {
                // 내려치기
                Debug.Log("내려쳐용!");
                (m_Boss as Boss_UsurperStateMachine).Slap();
            }
            else
            {
                // 범위공격
                Debug.Log("소리질러용");
                (m_Boss as Boss_UsurperStateMachine).Scream();
            }
            mb_IsAttacking = true;
            return;
        }

        // 근접 공격범위 밖이고, 원거리 공격범위 안이라면
        // 50% 확률로 따라오기
        // 20% 확률로 브레스
        // 30% 확률로 몸통박치기
        if (distance <= (m_Boss as Boss_UsurperStateMachine).RangeAttackDistance)
        {
            int randomIdx = Random.Range(0, 10);
            Debug.Log($"원거리공격 : {randomIdx}");
            if ( randomIdx <= 1)
            {
                // 브레스 공격
                Debug.Log("불뿜어용!");
                (m_Boss as Boss_UsurperStateMachine).Breath();
                mb_IsAttacking = true;
            }
            else if (randomIdx <= 4)
            {
                // 돌진 공격
                Debug.Log("돌진해용!");
                (m_Boss as Boss_UsurperStateMachine).Headbutt();
                mb_IsAttacking = true;
            }
            else
            {
                // 따라오기
                Debug.Log("따라가용");
                mb_IsTracing = true;
            }
            return;
        }
    }

    public override void CheckState()
    {
        // 체력이 0이 된다면 SetState(Die)
        if ((m_Boss as Boss_UsurperStateMachine).CurHp <= 0f)
        {
            Debug.Log("죽었어용");
            m_Boss.SetState((m_Boss as Boss_UsurperStateMachine).Die);
            return;
        }

        // 경직수치가 100이 된다면 SetState(Sturn)
        if ((m_Boss as Boss_UsurperStateMachine).CurSturnGauge >= 100f)
        {
            Debug.Log("어지러워용!");
            m_Boss.SetState((m_Boss as Boss_UsurperStateMachine).Sturn);
            return;
        }

        // 체력이 40% 이하가 된다면 SetState(Lazy)
        if ((m_Boss as Boss_UsurperStateMachine).CurHpRatio <= 40f)
        {
            Debug.Log("화났어용!");
            m_Boss.SetState((m_Boss as Boss_UsurperStateMachine).Lazy);
            return;
        }
    }

    public override void EnterState()
    {
        Debug.Log("Usurper : Enter Fight!");
    }

    public override void ExitState()
    {
        Debug.Log("Usurper : Exit Fight!");
        mb_IsAttacking = false;
    }

    public void SetIsAttacking(bool _isAttacking)
    {
        mb_IsAttacking = _isAttacking;
    }
    public void SetIsTracing(bool _isTracing)
    {
        mb_IsTracing = _isTracing;
    }
    public void SetIsTurning(bool _isTurning)
    {
        mb_IsTurning = _isTurning;
    }
}
