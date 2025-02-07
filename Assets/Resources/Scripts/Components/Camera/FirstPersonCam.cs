using System.Collections;
using UnityEngine;


public class FirstPersonCam : MonoBehaviour
{

    float speedH = 2.0f;
    float speedV = 2.0f;

    float yaw = 0.0f;
    float pitch = 0.0f;

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}