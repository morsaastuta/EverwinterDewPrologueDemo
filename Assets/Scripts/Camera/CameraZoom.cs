using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] CameraProperties cam;

    [SerializeField] float speed;
    [SerializeField] float furthest = 10;
    [SerializeField] float closest = -2;
    float current = 0;

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
