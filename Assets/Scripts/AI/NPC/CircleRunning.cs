using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CircleRunning : MonoBehaviour
{
    public MapProperties mapProperties;

    // Circling vars
    public float speed;
    public float angle;

    // Animation
    public int velocity;
    public int direction;

    // Interaction
    public bool interacting;

    private void Start()
    {
        velocity = 0;
        interacting = false;
    }

    void Update()
    {
        if (!mapProperties.pausedGame && !interacting)
        {
            Circle();
        }
        else
        {
            velocity = 0;
        }
    }

    void Circle()
    {
        velocity = 1;
        transform.position += speed * transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + angle, 0);
    }

    void Stop()
    {
    }
}
