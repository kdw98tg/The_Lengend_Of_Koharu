using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Player : MonoBehaviour
{
    public GameObject shootPos;
    public GameObject bullet;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, shootPos.transform.position, shootPos.transform.rotation);
        }
    }
}
