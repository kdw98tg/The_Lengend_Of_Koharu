using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tornado : MonoBehaviour
{
    protected TornadoCenter tornadoCenter;
    [SerializeField] protected Enemy enemy;
    [SerializeField] private float pullForce;
    [SerializeField] private float refreshRate;
    protected float dmg;


    protected virtual void Awake()
    {
        tornadoCenter = GetComponentInChildren<TornadoCenter>();
    }

    public virtual void Attack(Collider _other)
    {
        StartCoroutine(PullObject(_other, true));
    }
    public virtual void FinishAttack(Collider _other)
    {
        StopCoroutine(PullObject(_other, false)); //소용돌이 끝
    }


    public IEnumerator PullObject(Collider _x, bool _shouldPull)
    {
        while (_shouldPull)
        {
            Vector3 ForceDir = tornadoCenter.ReturnTornadoCenterPos() - _x.GetComponent<Enemy>().ReturnEnemyPos(); //토네이도 센터 쪽으로 
            _x.GetComponent<Enemy>().ReturnEnemyRigidbody().AddForce(ForceDir.normalized * pullForce * Time.deltaTime);
            yield return refreshRate; //refreshRate 만큼 프레임 지연
        }
    }

    public virtual float Damage(float _magnification = 1f)//데미지 주는 함수
    {
        //Debug.Log("tornado normal");
        return dmg * _magnification;
    }
}
