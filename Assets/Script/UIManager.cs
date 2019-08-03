using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject healthLayer;
    public GameObject ammoLayer;
    public GameObject pauseLayer;

    private int currentLayer = 1;

    const int ENNEMIES = 1;
    const int WALLS = 2;
    const int HEALTH = 3;
    const int AMMO = 4;
    const int PAUSE = 5;


    // Start is called before the first frame update
    void Start()
    {
        resetUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            currentLayer++;
            if(currentLayer == 6) { currentLayer = 1; }

            SwitchLayer();

        }
    }

    private void SwitchLayer()
    {
        switch (currentLayer)
        {
            case ENNEMIES:
                resetUI();
                enemyVisible(true);
                break;
            case WALLS:
                resetUI();
                //TODO walls visible
                break;
            case HEALTH:
                resetUI();
                healthLayer.SetActive(true);
                break;
            case AMMO:
                resetUI();
                ammoLayer.SetActive(true);
                break;
            case PAUSE:
                resetUI();
                pauseLayer.SetActive(true);
                break;
        }
    }

    private void resetUI()
    {
        healthLayer.SetActive(false);
        ammoLayer.SetActive(false);
        pauseLayer.SetActive(false);
        enemyVisible(false);
    }

    private void enemyVisible(bool isVisible)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i=0; i<enemies.Length; i++)
        {
            enemies[i].GetComponent<MeshRenderer>().enabled = isVisible;
        }
    }
}
