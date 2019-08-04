using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private Stat health;

    public NavMeshAgent nav;

    private Transform player;

    public MeshRenderer[] renderers;

    private AudioSource enemyDeathSound;
    private AudioSource damageSound;
    private AudioSource successSound;
    private AudioSource theme;

    private TextMeshProUGUI winText; 

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyDeathSound = GameObject.Find("enemy death").GetComponent<AudioSource>();
        damageSound = GameObject.Find("damage").GetComponent<AudioSource>();
        successSound = GameObject.Find("success").GetComponent<AudioSource>();
        theme = GameObject.Find("theme").GetComponent<AudioSource>();

        winText = GameObject.Find("you win").GetComponent<TextMeshProUGUI>();
        winText.enabled = false;
    }

    void Update()
    {
        nav.SetDestination(player.position);
    }

    private void TakeDamage(int damage)
    {
        health.CurrentVal -= damage;
        damageSound.Play();

        StartCoroutine(Blink(0.1f, 0.05f));

        if (health.CurrentVal <= 0)
        {
            StartCoroutine(deathBlink(0.1f, 0.05f));

        }
    }

    IEnumerator Blink(float duration, float blinkTime)
    {
        while(duration >= 0f)
        {
            duration -= Time.deltaTime;

            for (int j = 0; j < renderers.Length; j++)
            {
                renderers[j].enabled = !renderers[j].enabled;
            }

            yield return new WaitForSeconds(blinkTime);
        }

    }

    IEnumerator deathBlink(float duration, float blinkTime)
    {
        while (duration >= 0f)
        {
            duration -= Time.deltaTime;
            enemyDeathSound.Play();

            for (int j = 0; j < renderers.Length; j++)
            {
                renderers[j].enabled = !renderers[j].enabled;
            }

            yield return new WaitForSeconds(blinkTime);

            Destroy(gameObject);

            theme.Stop();
            successSound.Play();
            winText.enabled = true;


        }
    }
}
