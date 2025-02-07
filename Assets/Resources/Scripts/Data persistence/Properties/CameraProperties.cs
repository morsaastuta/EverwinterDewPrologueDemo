using Sirenix.Serialization;
using System;
using UnityEngine;

[Serializable]
public class CameraProperties : MonoBehaviour
{
    [OdinSerialize] public bool canZoom;
    [OdinSerialize] public bool canPivot;
    [OdinSerialize] public bool canHold;

    // Camera info
    [OdinSerialize] public Vector3 cameraPos;
    [OdinSerialize] public Quaternion cameraAngle;

    public void Reload(CameraProperties c)
    {
        canZoom = c.canZoom;
        canPivot = c.canPivot;
        canHold = c.canHold;

        cameraPos = c.cameraPos;
        cameraAngle = c.cameraAngle;

        LoadStatus();
    }

    public void SaveState()
    {
        cameraPos = transform.position;
        cameraAngle = transform.rotation;
    }

    public void LoadStatus()
    {
        transform.SetPositionAndRotation(cameraPos, cameraAngle);
    }

    public void SetActive(bool state)
    {
        canZoom = state;
        canPivot = state;
    }

    public void SetZoom(bool state)
    {
        canZoom = state;
    }

    public void SetPivot(bool state)
    {
        canPivot = state;
    }
}
