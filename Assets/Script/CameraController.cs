using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    private Vector3 moveTemp;

    [SerializeField] float speed = 3;
    [SerializeField] float xDifference;
    [SerializeField] float zDifference;

    [SerializeField] float movementThreshold = 3; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        xDifference = player.transform.position.x - transform.position.x;
        zDifference = player.transform.position.z - transform.position.z;

        if(xDifference > movementThreshold || zDifference > movementThreshold)
        {
            moveTemp = player.transform.position;
            moveTemp.y = transform.position.y;
            //moveTemp.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, moveTemp, speed * Time.deltaTime);
        }



    }
}
