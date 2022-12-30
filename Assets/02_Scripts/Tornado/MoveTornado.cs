using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTornado : MonoBehaviour
{
    [SerializeField] private Tornado tornado;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float destroyTime;
    [SerializeField] private float endTime;
    
    private void Update()
    {
        transform.position = transform.position + new Vector3(0f, 0f, moveSpeed * Time.deltaTime)  ;
        destroyTime += Time.deltaTime;
        if(destroyTime > endTime)
        {
            Destroy(gameObject,1f);
        }
    }
}
