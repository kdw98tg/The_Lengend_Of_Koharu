using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoCenter : MonoBehaviour
{
    private Transform tr = null;

    private void Awake()
    {
        tr = transform;
    }
    public Vector3 ReturnTornadoCenterPos()
    {
        return tr.position;
    }
    public Transform ReturnTornadoTransform()
    {
        return tr;
    }
}
