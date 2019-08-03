using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Stat health;

    public NavMeshAgent nav;

    private Transform player;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
    }

    private void TakeDamage(int damage)
    {
        health.CurrentVal -= damage;
        if (health.CurrentVal <= 0)
        {
            Destroy(gameObject);
        }
    }
}
