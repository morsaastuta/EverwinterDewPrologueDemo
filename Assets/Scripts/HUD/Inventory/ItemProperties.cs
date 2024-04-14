using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
