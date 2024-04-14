using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class InventoryController : MonoBehaviour
{
    // Important stuff
    [SerializeField] SlotGenerator slotGenerator;
    [SerializeField] RectTransform inventoryPane;
    [SerializeField] Sprite emptyIcon;
    int consumablesSize = 15;
    int materialsSize = 30;
    int keysSize = 5;

    // Item dragger
    [SerializeField] GameObject itemDragger;
    Item ogItem;
    int ogStock;
    int ogIndex;
    bool dropped = true;
    bool extractMode = false;

    // Shown info
    [SerializeField] Image shownIcon;
    [SerializeField] TextMeshProUGUI shownName;
    [SerializeField] TextMeshProUGUI shownDescription;
    [SerializeField] GameObject stockBox;
    [SerializeField] TextMeshProUGUI shownStock;

    // Item lists
    List<ConsumableItem> consumableItems = new List<ConsumableItem>();
    List<MaterialItem> materialItems = new List<MaterialItem>();
    List<KeyItem> keyItems = new List<KeyItem>();
    Dictionary<Item, int> itemQty = new Dictionary<Item, int>();

    // Slot lists
    List<GameObject> consumables = new List<GameObject>();
    List<GameObject> materials = new List<GameObject>();
    List<GameObject> keys = new List<GameObject>();
    List<GameObject> activeList = new List<GameObject>();

    public void Awake()
    {
        Generate();
        Initialize();

        activeList = consumables;
        Consumables();
    }

    public void Consumables()
    {
        Deselect();
        activeList = consumables;
        LoadSection();
    }

    public void Materials()
    {
        Deselect();
        activeList = materials;
        LoadSection();
    }

    public void Keys()
    {
        Deselect();
        activeList = keys;
        LoadSection();
    }

    public void Last()
    {
        Deselect();
        LoadSection();
    }

    public void LoadSection()
    {

        foreach (GameObject obj in consumables)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in materials)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in keys)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in activeList)
        {
            obj.SetActive(true);
        }
    }

    public void Generate()
    {
        for (int i = 0; i < Random.Range(15, 31); i++)
        {
            Item item = null;
            int stock = Random.Range(1, 100);

            switch (Random.Range(0, 8))
            {
                case 0:
                    item = new RationI();
                    break;
                case 1:
                    item = new RationII();
                    break;
                case 2:
                    item = new EtherI();
                    break;
                case 3:
                    item = new FlowerSnowdrop();
                    break;
                case 4:
                    item = new HerbsThrascias();
                    break;
                case 5:
                    item = new ClawRimebear();
                    break;
                case 6:
                    item = new PeltRimebear();
                    break;
                case 7:
                    item = new Galanthus();
                    break;
            }

            if (consumableItems.Count < consumablesSize && item.GetType().BaseType.Equals(typeof(ConsumableItem)))
            {
                consumableItems.Add((ConsumableItem)item);
            }

            if (materialItems.Count < materialsSize && item.GetType().BaseType.Equals(typeof(MaterialItem)))
            {
                materialItems.Add((MaterialItem)item);
            }

            if (keyItems.Count < keysSize && item.GetType().BaseType.Equals(typeof(KeyItem)))
            {
                keyItems.Add((KeyItem)item);
            }

            itemQty.Add(item, stock);
        }
    }

    public void Initialize()
    {
        int consumablesIndex = 0;

        foreach (Item item in consumableItems)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().UpdateItem(item, itemQty.GetValueOrDefault(item));
            slot.GetComponent<SlotController>().index = consumablesIndex;
            consumablesIndex++;
            consumables.Add(slot);
        }

        while (consumables.Count < consumablesSize)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().index = consumablesIndex;
            consumablesIndex++;
            consumables.Add(slot);
        }

        int materialsIndex = 0;

        foreach (Item item in materialItems)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().UpdateItem(item, itemQty.GetValueOrDefault(item));
            slot.GetComponent<SlotController>().index = materialsIndex;
            materialsIndex++;
            materials.Add(slot);
        }

        while (materials.Count < materialsSize)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().index = materialsIndex;
            materialsIndex++;
            materials.Add(slot);
        }

        int keysIndex = 0;

        foreach (Item item in keyItems)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().UpdateItem(item, itemQty.GetValueOrDefault(item));
            slot.GetComponent<SlotController>().index = keysIndex;
            keysIndex++;
            keys.Add(slot);
        }

        while (keys.Count < keysSize)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().index = keysIndex;
            keysIndex++;
            keys.Add(slot);
        }
    }

    public void Select(int index)
    {
        Item item = activeList[index].GetComponent<SlotController>().item;
        
        shownIcon.sprite = item.icon;
        shownName.text = item.name;
        shownDescription.text = item.description;

        if (item.stackable)
        {
            stockBox.SetActive(true);
            shownStock.text = activeList[index].GetComponent<SlotController>().stock.ToString();
        }
    }

    public void Deselect()
    {
        shownIcon.sprite = emptyIcon;
        shownName.text = "";
        shownDescription.text = "";
        shownStock.text = "";
        stockBox.SetActive(false);

        foreach (GameObject obj in activeList)
        {
            obj.GetComponent<SlotController>().Deselect();
        }
    }

    public void InitDrag(SlotController slot)
    {
        if (dropped == true)
        {
            Deselect();
            ogItem = slot.item;
            ogStock = slot.stock;
            ogIndex = slot.index;
            dropped = false;
            Select(slot.index);
            itemDragger.GetComponentInChildren<SlotController>().UpdateItem(slot.item, slot.stock);
            itemDragger.SetActive(true);
        }
    }

    public SlotController Drop()
    {
        if (dropped == false && itemDragger.GetComponentInChildren<SlotController>().item != null)
        {
            dropped = true;
            Deselect();
            SlotController toReturn = itemDragger.GetComponentInChildren<SlotController>();
            return toReturn;
        }
        return null;
    }

    public void DragSwitch(Item item, int stock)
    {
        activeList[ogIndex].GetComponent<SlotController>().UpdateItem(item, stock);
    }

    public bool SafeCheck()
    {
        extractMode = false;
        itemDragger.GetComponentInChildren<SlotController>().Clear();
        itemDragger.SetActive(false);
        if (dropped) return true;
        else return false;
    }

    public Item OriginalItem()
    {
        dropped = true;
        return ogItem;
    }

    public int OriginalStock()
    {
        return ogStock;
    }

    public void Extract(SlotController slot)
    {
        if (dropped == true)
        {
            extractMode = true;

            Deselect();
            ogItem = slot.item;
            ogStock = slot.stock;
            ogIndex = slot.index;
            dropped = false;
            Select(slot.index);
            itemDragger.GetComponentInChildren<SlotController>().UpdateItem(slot.item, 1);
            itemDragger.SetActive(true);
        }
    }

    public bool CheckExtraction()
    {
        if (extractMode)
        {
            dropped = false;
            return true;
        } else
        {
            return false;
        }
    }
}
