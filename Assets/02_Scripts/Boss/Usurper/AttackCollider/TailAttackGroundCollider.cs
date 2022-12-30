using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAttackGroundCollider : AttackCollider
{
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("PLAYER"))
        {
            _other.GetComponent<PlayerCollider>().HitTailAttackGround(this.transform.position);
        }
    }
}
