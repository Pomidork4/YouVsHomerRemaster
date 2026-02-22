using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    //public float ShiftLock = 0;


    private float rotationX = 31.2f;

    void Start()
    {
        //do stuff at the begining

    }

    void Update()
    {
        //do stuf      
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
	
   }
}
