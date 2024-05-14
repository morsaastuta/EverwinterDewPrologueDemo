using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData
{
    [OdinSerialize] public string sceneName;

    [OdinSerialize] PlayerProperties playerProperties;
    [OdinSerialize] CameraProperties cameraProperties;
    [OdinSerialize] MapProperties mapProperties;

    public SlotData(PlayerProperties p, CameraProperties c, MapProperties m)
    {
        sceneName = p.sceneName;
        playerProperties = p;
        cameraProperties = c;
        mapProperties = m;
    }

    public List<MonoBehaviour> LoadData()
    {
        List<MonoBehaviour> properties = new()
        {
            playerProperties,
            cameraProperties,
            mapProperties
        };
        return properties;
    }
}
