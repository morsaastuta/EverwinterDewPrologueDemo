using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData
{
    [OdinSerialize] PlayerBehaviour player;
    [OdinSerialize] CameraProperties camera;
    [OdinSerialize] WorldProperties world;

    public SlotData(PlayerBehaviour p, CameraProperties c, WorldProperties w)
    {
        player = p;
        camera = c;
        world = w;
    }

    public List<MonoBehaviour> LoadData()
    {
        List<MonoBehaviour> properties = new()
        {
            player,
            camera,
            world
        };
        return properties;
    }
}
