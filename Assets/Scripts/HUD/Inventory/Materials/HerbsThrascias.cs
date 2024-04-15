using UnityEngine;

public class HerbsThrascias : MaterialItem
{
    public HerbsThrascias()
    {
        icon = Resources.LoadAll<Sprite>("Sprites/HUD/Items/iconsheet")[4];
        name = "Thrascias Herbs";
        description = "Herbs gathered from the cold forests of Thrascias.";
        price = 3;
        stackable = true;
    }
}
