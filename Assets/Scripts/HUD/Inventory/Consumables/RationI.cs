using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RationI : ConsumableItem
{
    public RationI()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[0];
        name = "Small Ration";
        description = "Heals 100 HP to one party member.";
        price = 15;
        stackable = true;
    }
}
