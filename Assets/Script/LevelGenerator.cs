using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int lines = 3;
    private int columns = 3;

    public GameObject[] rooms;
    public GameObject bossRoom;
    public GameObject startRoom;
    public GameObject keyRoom;

    public GameObject[] levelStructures;
      
    private Vector3 roomPosition;
    private Vector3 levelPosition; 

    private float xModifier = 0;
    private float zModifier = 0;

    private int index;
    private GameObject room;
    private GameObject levelStructure; 

    private bool keyRoomGenerated = false;

    [SerializeField]
    private int iKeyRoom;
    [SerializeField]
    private int jKeyRoom; 
    
    void Start()
    {
        GenerateLevelStrucutre();
        ReturnIndexOfKeyRoom();
        SetupLevel();
    }

    private void GenerateLevelStrucutre()
    {
        index = Random.Range(0, levelStructures.Length);
        levelStructure = levelStructures[index];

        levelPosition = new Vector3(5.94f, 3.348f, 4.02f);

        GameObject newLevel = Instantiate(levelStructure, levelPosition, levelStructure.transform.rotation);
    }

    private void SetupLevel()
    {
        int i;
        int j; 

        for(i = 0; i < lines; i++)
        {
            for(j = 0; j < columns; j++)
            {
                xModifier = (i * 4.8f);
                zModifier = (j * 4.8f); 
                roomPosition = new Vector3(xModifier, 0, zModifier);

                if(i == 0 && j == 0)
                {
                    room = bossRoom;
                }

                else if(i == lines-1 && j == columns-1)
                {
                    room = startRoom;
                }

                else if(i == iKeyRoom && j == jKeyRoom)
                {
                    room = keyRoom;
                }

                else
                {
                    index = Random.Range(0, rooms.Length);
                    room = rooms[index];
                }

                GameObject newRoom = Instantiate(room, roomPosition, room.transform.rotation);
            }
        }
    }

    private void ReturnIndexOfKeyRoom()
    {
        iKeyRoom = Random.Range(0, 3);
        if(iKeyRoom == 0)
        {
            jKeyRoom = 2;
        }
        else if(iKeyRoom == 1)
        {
            jKeyRoom = 1;
        }
        else if(iKeyRoom == 2)
        {
            jKeyRoom = 0;
        }
    }
}
