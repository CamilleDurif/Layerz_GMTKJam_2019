using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keyRoom : MonoBehaviour
{
    private Animator anim;
    private TextMeshProUGUI bossText;

    public float timer = 5f; 
 
    void Start()
    {
        anim = GetComponent<Animator>();
        bossText = GameObject.Find("boss room is opened").GetComponent<TextMeshProUGUI>();
        bossText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            anim.SetTrigger("isActivated");
            StartCoroutine(bossRoomTextActived());

            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            if(doors != null)
            {
                for(int i=0; i<doors.Length; i++)
                {
                    doors[i].SetActive(false);
                }
            }
        }
    }

    IEnumerator bossRoomTextActived()
    {
        while (timer > 1)
        {
            bossText.enabled = true;
            timer -= timer * Time.deltaTime;

            yield return null;
        }

        bossText.enabled = false;
    }
}
