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
    SlotController selectedSlot;

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

    public void CloseAll()
    {
        Deselect();
        DeleteSlots();
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

        foreach (ConsumableItem item in partyController.playerProperties.GetConsumableItems())
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().UpdateItem(item, partyController.playerProperties.inventory.GetQty(item));
            slot.GetComponent<SlotController>().index = consumablesIndex;
            consumablesIndex++;
            consumables.Add(slot);
        }

        foreach (MaterialItem item in partyController.playerProperties.GetMaterialItems())
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().UpdateItem(item, partyController.playerProperties.inventory.GetQty(item));
            slot.GetComponent<SlotController>().index = materialsIndex;
            materialsIndex++;
            materials.Add(slot);
        }

        foreach (KeyItem item in partyController.playerProperties.GetKeyItems())
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(inventoryPane);
            slot.GetComponent<SlotController>().UpdateItem(item, partyController.playerProperties.inventory.GetQty(item));
            slot.GetComponent<SlotController>().index = keysIndex;
            keysIndex++;
            keys.Add(slot);
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

    public void UseItem()
    {
        selectedSlot.UseItem();
        ReSelect();
    }

    public void DropItem()
    {
        selectedSlot.DropItem();
        ReSelect();
    }

    public void Select(int index)
    {
        selectedSlot = activeList[index].GetComponent<SlotController>();

        shownIcon.sprite = selectedSlot.item.GetIcon();
        shownName.text = selectedSlot.item.name;
        shownDescription.text = selectedSlot.item.description;

        if (selectedSlot.item.stackable)
        {
            stockBox.SetActive(true);
            shownStock.text = selectedSlot.stock.ToString();
        }
    }

    public void ReSelect()
    {
        if (selectedSlot.occupied)
        {
            shownIcon.sprite = selectedSlot.item.GetIcon();
            shownName.text = selectedSlot.item.name;
            shownDescription.text = selectedSlot.item.description;

            if (selectedSlot.item.stackable)
            {
                stockBox.SetActive(true);
                shownStock.text = selectedSlot.stock.ToString();
            }
        }
        else
        {
            Deselect();
        }
    }

    public void Deselect()
    {
        shownIcon.sprite = emptyIcon;
        shownName.text = "";
        shownDescription.text = "";
        shownStock.text = "";
        stockBox.SetActive(false);

        if (activeList != null)
        {
            List<SlotController> itemsToReload = new();

            foreach (GameObject obj in activeList)
            {
                obj.GetComponent<SlotController>().Deselect();
                if (obj.GetComponent<SlotController>().occupied) itemsToReload.Add(obj.GetComponent<SlotController>());
            }

            partyController.playerProperties.ReorganizeInventory(itemsToReload);
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
        }
        else return false;
    }
}
