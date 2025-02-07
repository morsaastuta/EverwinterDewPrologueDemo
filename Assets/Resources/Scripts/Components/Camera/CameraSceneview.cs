using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSceneview : MonoBehaviour
{
    public float safeAngleX = 0;
    public float safeAngleY = 0;

    void Update()
    {
        Vector3 angle = transform.eulerAngles;

        // POV update
        if ((angle.x < 30 || angle.x > 330) && (angle.y < 30 || angle.y > 330))
        {
            safeAngleX = angle.x;
            safeAngleY = angle.y;
            angle.y += Input.GetAxisRaw("Mouse X") * 2;
            angle.x -= Input.GetAxisRaw("Mouse Y") * 2;
        }
        else
        {
            angle.x = safeAngleX;
            angle.y = safeAngleY;
        }

        transform.eulerAngles = angle;
    }
}
