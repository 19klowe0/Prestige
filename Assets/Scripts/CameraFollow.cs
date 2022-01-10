using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //offset for camera 
    public float offset;
    

    private Transform playerTransform;
    public Transform butterflyTransform;
    public SpriteSwitch i;
    void Start()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //butterflyTransform = GameObject.FindGameObjectWithTag("ButterFly").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //butterflyTransform = GameObject.FindGameObjectWithTag("ButterFly").transform;
    }

    void LateUpdate()
    {
        Vector3 temp;
        if (i.whichIsOn == 1)
        {
            //we stroew currents camera's position in variable temp
            temp = transform.position;

            //set camera's position x to the player's position x
            temp.x = playerTransform.position.x;
            temp.y = playerTransform.position.y;

            //only offsetting Y position
            temp.y += offset;


            //we set back the camera's temp position to the camera's current position
            transform.position = temp;
        }
        else if (i.whichIsOn == 2)
        {
            //we stroew currents camera's position in variable temp
            temp = transform.position;

            //set camera's position x to the player's position x
            temp.x = butterflyTransform.position.x;
            temp.y = butterflyTransform.position.y;

            //only offsetting Y position
            temp.y += offset;

            //we set back the camera's temp position to the camera's current position
            transform.position = temp;
        }
        

        

    }
}
