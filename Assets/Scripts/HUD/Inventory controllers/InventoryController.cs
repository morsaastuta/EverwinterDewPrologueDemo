using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] PartyController partyController;

    // Important stuff
    [SerializeField] SlotGenerator slotGenerator;
    [SerializeField] RectTransform inventoryPane;
    [SerializeField] Sprite emptyIcon;

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

    // Slot lists
    List<GameObject> consumables = new();
    List<GameObject> materials = new();
    List<GameObject> keys = new();
    List<GameObject> activeList = new();

    public void Consumables()
    {
        Deselect();
        DeleteSlots();
        activeList = consumables;
        LoadSection();
    }

    public void Materials()
    {
        Deselect();
        DeleteSlots();
        activeList = materials;
        LoadSection();
    }

    public void Keys()
    {
        Deselect();
        DeleteSlots();
        activeList = keys;
        LoadSection();
    }

    public void Last()
    {
        Deselect();
        DeleteSlots();
        LoadSection();
    }

    public void DeleteSlots()
    {
        foreach (GameObject obj in consumables) Destroy(obj);
        foreach (GameObject obj in materials) Destroy(obj);
        foreach (GameObject obj in keys) Destroy(obj);

        consumables.Clear();
        materials.Clear();
        keys.Clear();

        activeList = null;
    }

    public void LoadSection()
    {
        Load();

        foreach (GameObject obj in consumables) obj.SetActive(false);
        foreach (GameObject obj in materials) obj.SetActive(false);
        foreach (GameObject obj in keys) obj.SetActive(false);
        foreach (GameObject obj in activeList) obj.SetActive(true);
    }

    public void Load()
    {
        int consumablesIndex = 0;
        int materialsIndex = 0;
        int keysIndex = 0;

        foreach (Item item in partyController.playerProperties.inventory.Keys)
        {
            if (item.GetType().BaseType.Equals(typeof(ConsumableItem)))
            {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(inventoryPane);
                slot.GetComponent<SlotController>().UpdateItem(item, partyController.playerProperties.inventory.GetValueOrDefault(item));
                slot.GetComponent<SlotController>().index = consumablesIndex;
                consumablesIndex++;
                consumables.Add(slot);
            }
            if (item.GetType().BaseType.Equals(typeof(MaterialItem)))
            {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(inventoryPane);
                slot.GetComponent<SlotController>().UpdateItem(item, partyController.playerProperties.inventory.GetValueOrDefault(item));
                slot.GetComponent<SlotController>().index = materialsIndex;
                materialsIndex++;
                materials.Add(slot);
            }
            if (item.GetType().BaseType.Equals(typeof(KeyItem)))
            {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(inventoryPane);
                slot.GetComponent<SlotController>().UpdateItem(item, partyController.playerProperties.inventory.GetValueOrDefault(item));
                slot.GetComponent<SlotController>().index = keysIndex;
                keysIndex++;
                keys.Add(slot);
            }
        }

        while (consumables.Count < partyController.playerProperties.consumablesSize)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().index = consumablesIndex;
            consumablesIndex++;
            consumables.Add(slot);
        }

        while (materials.Count < partyController.playerProperties.materialsSize)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().index = materialsIndex;
            materialsIndex++;
            materials.Add(slot);
        }

        while (keys.Count < partyController.playerProperties.keysSize)
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
        
        shownIcon.sprite = item.GetIcon();
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

        if (activeList != null) foreach (GameObject obj in activeList) obj.GetComponent<SlotController>().Deselect();
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
        }
        else return false;
    }
}
