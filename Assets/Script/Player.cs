using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Stat health;

    public float speed;

    private void Awake()
    {
        health.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void TakeDamage(int damage)
    {
        health.CurrentVal -= damage;
        if (health.CurrentVal <= 0)
        {
            Destroy(gameObject);
        }
    }
}
