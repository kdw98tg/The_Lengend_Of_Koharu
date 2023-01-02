using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornado : Tornado
{
    //private TornadoCenter tornadoCenter;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;


    protected override void Awake()
    {
        tornadoCenter = GetComponentInChildren<TornadoCenter>();
        base.Awake();
        dmg = 10f;
    }
    private void OnDestroy()
    {
        ExplodeTornado();
    }
    public override float Damage(float _magnification = 1f)
    {
        //Debug.Log("tornado fire");
        //dmg += dmg * Time.deltaTime;
        return dmg * _magnification;
    }
      
    private void ExplodeTornado()//토네이도 터는 함수
    {
        enemy.ReturnEnemyRigidbody().AddExplosionForce(explosionForce, tornadoCenter.ReturnTornadoCenterPos(), explosionRadius);
        ///Debug.Log("Explode");
    }
    public override void Attack(Collider _other)
    {
        base.Attack(_other);
        _other.gameObject.GetComponent<Enemy>().SetEnemyAttackedMaterial(new Color(255f, 0f, 0f, 50f));
    }

    public override void FinishAttack(Collider _other)
    {
        base.FinishAttack(_other);
        _other.gameObject.GetComponent<Enemy>().ReturnEnemyOriginMaterial();
        _other.gameObject.GetComponent<Enemy>().StartCoroutine(_other.gameObject.GetComponent<Enemy>().EnemyDotDamage(1f, 10));//(�ʴ� 1f��ŭ�� ���ظ�, 10������)
    }
}
