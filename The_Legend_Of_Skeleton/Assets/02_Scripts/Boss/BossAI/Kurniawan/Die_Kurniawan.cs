using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Die_Kurniawan : BossState
{
    public Die_Kurniawan(Boss _boss) : base("Die", _boss){}

    public override void EnterState()
    {
        Debug.Log("Die_Kurniawan 입장");
        m_Boss.Anim.SetBool("IsDie",true);
    }

    public override void ExitState()
    {
        Debug.Log("Die_Kurniawan 퇴장");
        m_Boss.Anim.SetBool("IsDie",false);
    }

    public override void Action()
    {

    }
    
    public override void CheckState()
    {
         //1. 모든 상태에서 어떠한 이유에서든
         //2. 보스의 체력이 0이하가 되면 
         //3. Die 상태로 가야해요.
         //4. 이건 다른 상태에 넣어야해요.
    }
    
  
    
    
}
