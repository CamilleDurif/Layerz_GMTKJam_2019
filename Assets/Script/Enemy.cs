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
            StartCoroutine(Blink(0.1f, 0.05f));
        }
    }


    IEnumerator Blink(float duration, float blinkTime)
    {
        while(duration >= 0f)
        {
            duration -= Time.deltaTime;

            Debug.Log(duration);
            
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

            for (int j = 0; j < renderers.Length; j++)
            {
                renderers[j].enabled = !renderers[j].enabled;
            }

            yield return new WaitForSeconds(blinkTime);
        }

        Destroy(gameObject);

    }
}
