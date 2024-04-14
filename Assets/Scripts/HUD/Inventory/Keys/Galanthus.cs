using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Galanthus : KeyItem
{
    public Galanthus()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[2];
        name = "Galanthus";
        description = "Still waiting for the first dew drop after Everwinter.";
        stackable = false;
    }
}
