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
        // �÷��̾��� ��ġ, ���� �Ǵ� �� ���� �ൿ ����

        // 0. �÷��̾ �ٶ󺸱�
        // 1. �����ġ�� 
        // 2. ���� ����
        // 3. �극��
        // 4. ���¢���� ��������

        // �÷��̾ �������� �ٶ󺸰� ���� �ʴ´ٸ� ȸ�� �׼�
        // �÷��̾ �������� �ٶ󺸰� �ִٸ�

        // �Ÿ��� ������
        // 40% Ȯ���� �������
        // 40% Ȯ���� �����ġ��
        // 20% Ȯ���� ��������

        // �Ÿ��� �ָ�
        // 30% Ȯ���� �극��
        // 70% Ȯ���� ������ �������

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


        // �÷��̾ ������ �ƴ϶��
        if (angle >= (m_Boss as Boss_UsurperStateMachine).ForwardAngle)
        {
            // ���� ����ؼ� ���� ����ġ��
            int randomIdx = Random.Range(0, 3);
            if (Mathf.Abs(angle) >= 160f && randomIdx == 0)
            {
                Debug.Log("�����Ŀ�");
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
                // ����������
                (m_Boss as Boss_UsurperStateMachine).Whirlwind();
                return;
            }
        }

        // ���� ���ݹ��� ���̶��
        // 40% Ȯ���� �������
        // 40% Ȯ���� �������
        // 20% Ȯ���� ��������
        if ((m_Boss as Boss_UsurperStateMachine).MeleeAttackDistance >= distance)
        {
            int randomIdx = Random.Range(0, 10);
            Debug.Log(randomIdx);
            if (randomIdx <= 3)
            {
                // �������
                Debug.Log("�����!");
                (m_Boss as Boss_UsurperStateMachine).Bite();
            }
            else if (randomIdx <= 7)
            {
                // ����ġ��
                Debug.Log("�����Ŀ�!");
                (m_Boss as Boss_UsurperStateMachine).Slap();
            }
            else
            {
                // ��������
                Debug.Log("�Ҹ�������");
                (m_Boss as Boss_UsurperStateMachine).Scream();
            }
            mb_IsAttacking = true;
            return;
        }

        // ���� ���ݹ��� ���̰�, ���Ÿ� ���ݹ��� ���̶��
        // 50% Ȯ���� �������
        // 20% Ȯ���� �극��
        // 30% Ȯ���� �����ġ��
        if (distance <= (m_Boss as Boss_UsurperStateMachine).RangeAttackDistance)
        {
            int randomIdx = Random.Range(0, 10);
            Debug.Log($"���Ÿ����� : {randomIdx}");
            if ( randomIdx <= 1)
            {
                // �극�� ����
                Debug.Log("�һվ��!");
                (m_Boss as Boss_UsurperStateMachine).Breath();
                mb_IsAttacking = true;
            }
            else if (randomIdx <= 4)
            {
                // ���� ����
                Debug.Log("�����ؿ�!");
                (m_Boss as Boss_UsurperStateMachine).Headbutt();
                mb_IsAttacking = true;
            }
            else
            {
                // �������
                Debug.Log("���󰡿�");
                mb_IsTracing = true;
            }
            return;
        }
    }

    public override void CheckState()
    {
        // ü���� 0�� �ȴٸ� SetState(Die)
        if ((m_Boss as Boss_UsurperStateMachine).CurHp <= 0f)
        {
            Debug.Log("�׾����");
            m_Boss.SetState((m_Boss as Boss_UsurperStateMachine).Die);
            return;
        }

        // ������ġ�� 100�� �ȴٸ� SetState(Sturn)
        if ((m_Boss as Boss_UsurperStateMachine).CurSturnGauge >= 100f)
        {
            Debug.Log("����������!");
            m_Boss.SetState((m_Boss as Boss_UsurperStateMachine).Sturn);
            return;
        }

        // ü���� 40% ���ϰ� �ȴٸ� SetState(Lazy)
        if ((m_Boss as Boss_UsurperStateMachine).CurHpRatio <= 40f)
        {
            Debug.Log("ȭ�����!");
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
