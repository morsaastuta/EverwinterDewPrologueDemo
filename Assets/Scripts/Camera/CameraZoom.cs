using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public CameraProperties cam;

    public float speed;
    public float furthest = 10;
    public float closest = -2;
    private float current = 0;

    void Update()
    {
        if (cam.canZoom)
        {
            Vector3 position = transform.position;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && current > closest)
            {
                position += speed * transform.forward;
                current--;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && current < furthest)
            {
                position += speed * -transform.forward;
                current++;
            }
            transform.position = position;
        }
    }
}
