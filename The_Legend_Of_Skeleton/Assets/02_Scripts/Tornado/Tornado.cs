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
        StopCoroutine(PullObject(_other, false)); //�ҿ뵹�� ��
    }


    public IEnumerator PullObject(Collider _x, bool _shouldPull)
    {
        while (_shouldPull)
        {
            Vector3 ForceDir = tornadoCenter.ReturnTornadoCenterPos() - _x.GetComponent<Enemy>().ReturnEnemyPos(); //����̵� ���� ������ 
            _x.GetComponent<Enemy>().ReturnEnemyRigidbody().AddForce(ForceDir.normalized * pullForce * Time.deltaTime);
            yield return refreshRate; //refreshRate ��ŭ ������ ����
        }
    }

    public virtual float Damage(float _magnification = 1f)//������ �ִ� �Լ�
    {
        //Debug.Log("tornado normal");
        return dmg * _magnification;
    }
}
