using UnityEngine;
using System;

[Serializable]
public class ItemProperties : MonoBehaviour
{
    public Item item;
    public int stock = 0;

    public ItemProperties(Item item)
    {
        this.item = item;
    }

    public void Consume()
    {
        stock--;
    }
}
