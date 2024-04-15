using UnityEngine;

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
