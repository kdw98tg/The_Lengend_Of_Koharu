using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Jaw : MonoBehaviour
{
    private BoxCollider boxCollider;
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    public void BoxColliderEnabled(bool _on)
    {
        if(_on)
        {
            boxCollider.enabled = true;
        }
        else if(!_on)
        {
            boxCollider.enabled = false;
        }
    }
}
