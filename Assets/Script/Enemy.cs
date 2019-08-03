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
            StartCoroutine(Blink(0.1f, 0.1f));
        }
    }


    IEnumerator Blink(float duration, float blinkTime)
    {
        while(duration >= 0f)
        {
            duration -= Time.deltaTime;

            Debug.Log(duration);

            this.GetComponent<Renderer>().enabled = !this.GetComponent<Renderer>().enabled;

            yield return new WaitForSeconds(blinkTime);
        }

        Destroy(gameObject);

    }
}
