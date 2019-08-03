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

    [SerializeField] float xMovementThreshold;
    [SerializeField] float zMovementThreshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        xDifference = Mathf.Abs(player.transform.position.x - transform.position.x);
        zDifference = Mathf.Abs(player.transform.position.z - transform.position.z);

        if (xDifference > xMovementThreshold || zDifference > zMovementThreshold)
        {
            moveTemp = player.transform.position;
            moveTemp.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, moveTemp, speed * Time.deltaTime);
        }



    }
}
