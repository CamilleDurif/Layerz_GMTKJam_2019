using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    public float timer;

    private void Start()
    {
        StartCoroutine(bulletLifetime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

    private IEnumerator bulletLifetime()
    {
        timer = 10f; 

        while(timer > 1f)
        {
            timer -= timer * Time.deltaTime / 5f;

            yield return null;
        }

        Destroy(gameObject);
    }

}
