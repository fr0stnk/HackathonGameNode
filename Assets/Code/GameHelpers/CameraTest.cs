using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour {

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    public bool cameraDragging = true;

    public bool doMovement = true;

    
    void Update()
    {
        if (!doMovement)
        {
            return;
        }

        float speed = dragSpeed * Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            Camera.main.transform.position += new Vector3(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, 0);
            //Camera.main.transform.position += new Vector3(Input.GetAxis("Mouse Y") * speed, 0, Input.GetAxis("Mouse X") * speed);
        }
        
    }


}

