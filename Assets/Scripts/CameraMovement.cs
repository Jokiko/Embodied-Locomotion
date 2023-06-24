using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private int cameraMode = 0; // 0 = 1st person, 1 = 3rd person

    /*private Vector3 originalPosition;
    private Quaternion originalRotation;*/

    private Transform target;
    private float smoothSpeed = 10f;
    private Vector3 firstPersonOffset;

    private float distance = 4f;  // 
    private float height = 2f;  // 
    private Vector3 thirdPersonOffset;


    // Start is called before the first frame update
    void Start()
    {
        //Saving original camera position to reset camera
        /*originalPosition = transform.position;
        originalRotation = transform.rotation;*/

        target = GameObject.Find("Sparrow").transform;
        firstPersonOffset = new Vector3(0f, 0f, 0f);
        thirdPersonOffset = new Vector3(0f, height, distance);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameraMode = (cameraMode + 1) % 2; 
        }

        switch(cameraMode){
            /*//Standard
            case 0:
                transform.position = originalPosition;
                transform.rotation = originalRotation;
                break;*/
            //First Person    
            case 0: 
                Vector3 desiredPosition = target.position + firstPersonOffset;

                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;

                //turn camera in rotation of bird
                transform.rotation = target.rotation;
                break;
            //Third Person    
            case 1:
                Vector3 newPosition = target.position - (target.forward * thirdPersonOffset.z) + (Vector3.up * thirdPersonOffset.y);

                transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
                Vector3 lookDirection = target.forward;

                // calculate rotation based on bird's forward rotation
                Vector3 lookAtPosition = target.position + (target.forward * thirdPersonOffset.z);

                // turn camera where the bird is looking
                transform.LookAt(lookAtPosition);
                break;         
        }
    }
}
