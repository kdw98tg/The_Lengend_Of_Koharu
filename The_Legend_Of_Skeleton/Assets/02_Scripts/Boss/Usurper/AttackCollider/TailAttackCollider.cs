using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAttackCollider : AttackCollider
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("PLAYER"))
        {
            _other.GetComponent<PlayerCollider>().HitTailAttack(this.transform.position);
        }
    }
}
