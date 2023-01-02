using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTornado : Tornado
{
    protected override void Awake()
    {
        base.Awake();
        dmg = 10f;
    }
    public override float Damage(float _magnification = 1f)
    {
        return dmg * _magnification;
    }

    public override void Attack(Collider _other)
    {
        base.Attack(_other);
        _other.gameObject.GetComponent<Enemy>().StartCoroutine(_other.gameObject.GetComponent<Enemy>().EnemyFreezed(1f));
        _other.gameObject.GetComponent<Enemy>().SetEnemyAttackedMaterial(new Color(0, 0f, 255f, 50f));
    }
    public override void FinishAttack(Collider _other)
    {
        base.FinishAttack(_other);
        _other.gameObject.GetComponent<Enemy>().ReturnEnemyOriginMaterial();
    }
    
}
