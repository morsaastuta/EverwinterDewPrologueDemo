using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] CameraProperties cam;

    [SerializeField] float speed;
    [SerializeField] float furthest;
    [SerializeField] float closest;
    float current = 0;

    RaycastHit hit;
    [SerializeField] Transform player;
    [SerializeField] Transform camera;
    bool corrected = false;
    List<Vector3> steppedPositions = new();

    void Update()
    {
        if (cam.canZoom)
        {
            Vector3 position = transform.position;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && current > closest)
            {
                position += speed * transform.forward;
                current--;
                corrected = false;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && current < furthest)
            {
                position += speed * -transform.forward;
                current++;
                corrected = false;
            }
            transform.position = position;
        }

        DetectCollision();
    }

    void DetectCollision()
    {
        Vector3 position = transform.position;
        if (Physics.Linecast(camera.position, player.position, out hit, LayerMask.GetMask("Terrain")) && current > closest)
        {
            steppedPositions.Add(position);

            position += speed * transform.forward;
            current--;

            corrected = true;
        }
        else if (corrected && !Physics.Linecast(steppedPositions[steppedPositions.Count - 1], camera.position, out hit, LayerMask.GetMask("Terrain")))
        {
            bool doit = false;

            if (current == closest && steppedPositions.Count >= 2)
            {
                if(Physics.Linecast(steppedPositions[steppedPositions.Count - 2], camera.position, out hit, LayerMask.GetMask("Terrain"))) doit = false;
            }

            if (doit)
            {
                steppedPositions.RemoveAt(steppedPositions.Count - 1);

                position += speed * -transform.forward;
                current++;

                if (steppedPositions.Count == 0) corrected = false;
            }
        }

        transform.position = position;
    }
}
