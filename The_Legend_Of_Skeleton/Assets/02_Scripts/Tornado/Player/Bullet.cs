using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    
    private enum Attribute
    {
        Fire,
        Ice,
        Lightning
    };

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.AddForce(transform.forward * bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TORNADO"))
        {
            other.GetComponent<TornadoController>().SetTornado(((int)Attribute.Fire));
        }
    }

}
