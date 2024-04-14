using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PeltRimebear : MaterialItem
{
    public PeltRimebear()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[3];
        name = "Rimebear Pelt";
        description = "Pelt obtained from a Rimebear.";
        price = 10;
        stackable = true;
    }
}
