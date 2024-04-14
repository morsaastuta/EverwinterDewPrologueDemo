using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EtherI : ConsumableItem
{
    public EtherI()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[1];
        name = "Small Ether";
        description = "Recovers 25 MP to one party member.";
        price = 25;
        stackable = true;
    }
}
