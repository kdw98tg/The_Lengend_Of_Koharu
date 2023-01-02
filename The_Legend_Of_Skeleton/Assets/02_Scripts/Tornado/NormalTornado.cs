using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTornado : Tornado
{
    protected override void Awake()
    {
        base.Awake();
        dmg = 1f;
    }
   
    public override float Damage(float _magnification)
    {
        return base.Damage();
    }
}
