using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    private Weapon m_Weapon = null;

    public void OnCollider()
    {
        m_Weapon.OnCollider();
    }
    public void OffCollider()
    {
        m_Weapon.OffCollider();
    }
    public void Init(Weapon _weapon)
    {
        m_Weapon = _weapon;
    }
}
