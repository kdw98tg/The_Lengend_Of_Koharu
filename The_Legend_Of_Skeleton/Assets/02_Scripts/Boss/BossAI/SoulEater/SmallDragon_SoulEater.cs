using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallDragon_SoulEater : MonoBehaviour
{
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private Player player = null;
    private NavMeshAgent Agent;
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("PLAYER"))
        {
            _other.gameObject.GetComponent<PlayerCollider>().HitSmallDragon(this.transform.position);
            Destroy(gameObject);
            Debug.Log("explsion");
        }
    }
    private void Update()
    {
        //Debug.Log("retrunpos"+player.transform.position);
        Agent.destination = player.GetPos();//이걸왜 못찾냐
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }
}
