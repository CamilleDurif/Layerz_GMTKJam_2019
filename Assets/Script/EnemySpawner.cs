using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public int nbOfEnememies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawEnemies());
    }

    private IEnumerator SpawEnemies()
    {
        for (int i = 0; i < nbOfEnememies; i++)
        {
            Instantiate(enemy, this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
}