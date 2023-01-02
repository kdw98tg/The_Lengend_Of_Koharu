using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    private void OnTriggerEnter(Collider _coll)
    {
        if (_coll.CompareTag("PLAYER"))
        {
            _coll.GetComponent<PlayerCollider>().HitWhirlwind(this.transform.forward);
        }
    }
}
