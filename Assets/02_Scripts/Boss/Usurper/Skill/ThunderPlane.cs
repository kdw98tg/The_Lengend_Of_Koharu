using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPlane : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ShotCoroutine());
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("PLAYER"))
        {
            _other.GetComponent<PlayerCollider>().HitBite(this.transform.position);
        }
    }


    public void SetActive(bool _value)
    {
        this.gameObject.SetActive(_value);
    }
    public void SetPosition(Vector3 _pos)
    {
        this.transform.position = _pos;
    }

    private IEnumerator ShotCoroutine()
    {
        // 3초 후 번개떨어짐
        yield return new WaitForSeconds(3f);
        GetComponent<Collider>().enabled = true;

        // 0.3초 후 판정 사라짐
        yield return new WaitForSeconds(0.3f);
        GetComponent<Collider>().enabled = false;

        this.gameObject.SetActive(false);
    }
}
