using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 5f;
    public float sensY = 5f;
    public Transform BodyOriantetion;
    public Transform HeadOriantetion;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        HeadOriantetion.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        BodyOriantetion.Rotate(Vector3.up * mouseX); 
    }
}
