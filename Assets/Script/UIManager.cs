using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject healthLayer;
    public GameObject ammoLayer;
    public GameObject pauseLayer;

    private int currentLayer = 1;

    public const int ENNEMIES = 1;
    public const int WALLS = 2;
    public const int HEALTH = 3;
    public const int AMMO = 4;
    public const int PAUSE = 5;

    public TextMeshProUGUI ammoText;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        resetUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !player.isPaused){
            currentLayer++;
            if(currentLayer == 6) { currentLayer = 1; }

            SwitchLayer();

        }

        //updateUI
        ammoText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().nbOfAmmo.ToString();

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
                wallsVisible(true);
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
        wallsVisible(false);
    }

    private void enemyVisible(bool isVisible)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();

            for(int j =0; j<enemy.renderers.Length; j++)
            {
                enemy.renderers[j].enabled = isVisible;
            }

        }

    }
    
    private void wallsVisible(bool isVisible)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Walls");

        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i].GetComponent<MeshRenderer>() != null)
            {
                walls[i].GetComponent<MeshRenderer>().enabled = isVisible;
            }
        }
    }



    public int GetCurrentLayer()
    {
        return currentLayer;
    }
}
