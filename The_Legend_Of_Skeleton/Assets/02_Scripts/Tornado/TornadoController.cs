using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoController : MonoBehaviour
{
    //[SerializeField] private Tornado tornado;
    [SerializeField] private Bullet bullet;

    private Tornado curTornado = null;
    //AddComponent를 하는건 이친구고
    //공은 속성값을 저장해놔야 한다.
    


    private void Awake()
    {
        curTornado = GetComponent<LightningTornado>();
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("ENEMY"))
        {
            curTornado.Attack(_other);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("ENEMY"))
        {
            curTornado.FinishAttack(_other);
        }
    }


    public void SetTornado(int _idx)
    {
        if (_idx == 0)
        {
            FireTornado firTor = this.gameObject.AddComponent<FireTornado>();
            SetState(firTor);
        }
        else if (_idx == 1)
        {
            IceTornado iceTor = this.gameObject.AddComponent<IceTornado>();
        }
        else if (_idx == 2)
        {
            LightningTornado lightningTor = this.gameObject.AddComponent<LightningTornado>();
        }

    }

    private void SetState(Tornado _tornado)
    {
        curTornado = _tornado;
    }
}
