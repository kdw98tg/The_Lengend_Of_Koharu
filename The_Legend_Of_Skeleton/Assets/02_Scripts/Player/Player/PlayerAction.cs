using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStateMachine;


// 스매시, ElementBurst, 토네이도 관련 스킬효과들 구현
// 여기서는 조건검사를 할게 아니고, 기술을 가지고만 있어야 한다.
public class PlayerAction : MonoBehaviour
{
    public delegate bool BoolVoidDelegate();


    public void OnSmashCallback()
    {

    }

    public void OnElementBurstCallback(int _elementIdx, int _elementStack, float _elementBurstDamage)
    {
        // 속성방출 발사하기
    }


    #region Skill
    private void ElementBurst(int _elementIdx, int _elementStack, float _elementBurstDamage)
    {

    }

    // 여기는 그냥 소환만 시켜주면 됨. 조건검사는 앞에서
    // 여기서 말하는 조건검사 : Fever게이지가 있어서 SpawnTornado를 사용할 수 있는가 검사 ㅇㅇ
    private void SpawnTornado()
    {
        Debug.Log("토네이도!");
    }


    #endregion
}
