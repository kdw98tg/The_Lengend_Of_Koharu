using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;


// ���Ž�, ElementBurst, ����̵� ���� ��ųȿ���� ����
// ���⼭�� ���ǰ˻縦 �Ұ� �ƴϰ�, ����� ������ �־�� �Ѵ�.
public class PlayerAction : MonoBehaviour
{
    public delegate bool BoolVoidDelegate();


    public void OnSmashCallback()
    {

    }

    public void OnElementBurstCallback(int _elementIdx, int _elementStack, float _elementBurstDamage)
    {
        // �Ӽ����� �߻��ϱ�
    }


    #region Skill
    private void ElementBurst(int _elementIdx, int _elementStack, float _elementBurstDamage)
    {

    }

    // ����� �׳� ��ȯ�� �����ָ� ��. ���ǰ˻�� �տ���
    // ���⼭ ���ϴ� ���ǰ˻� : Fever�������� �־ SpawnTornado�� ����� �� �ִ°� �˻� ����
    private void SpawnTornado()
    {
        Debug.Log("����̵�!");
    }


    #endregion
}
