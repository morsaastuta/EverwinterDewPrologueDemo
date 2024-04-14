using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{
    // Important stuff
    [SerializeField] PartyController partyController;
    [SerializeField] SlotGenerator slotGenerator;
    [SerializeField] RectTransform gearSelectionPane;
    [SerializeField] Sprite emptyIcon;
    int gearSize = 15;

    // Item dragger
    [SerializeField] GameObject itemDragger;
    Item ogItem;
    bool dropped = true;
    bool fromSelector;
    bool toEquipment;
    Type operationType;
    bool switched;
    GearItem switchedItem;
    bool mismatchError;

    // Items from selector
    int ogIndex;

    // Items from gear slots
    GearController ogSlot;

    // Shown info
    [SerializeField] Image shownIcon;
    [SerializeField] TextMeshProUGUI shownName;
    [SerializeField] TextMeshProUGUI shownDescription;

    // Gear slots
    [SerializeField] GameObject headSlot;
    [SerializeField] GameObject bodySlot;
    [SerializeField] GameObject armsSlot;
    [SerializeField] GameObject legsSlot;
    [SerializeField] GameObject wield1Slot;
    [SerializeField] GameObject wield2Slot;
    [SerializeField] GameObject accessory1Slot;
    [SerializeField] GameObject accessory2Slot;
    List<GameObject> gearSlots = new List<GameObject>();

    // Gear selector list
    List<GearItem> gearItems = new List<GearItem>();
    List<GearItem> selectorItemList = new List<GearItem>();
    List<GameObject> selectorSlotList = new List<GameObject>();

    public void Awake()
    {
        // Slot observation
        gearSlots.Add(headSlot);
        gearSlots.Add(bodySlot);
        gearSlots.Add(armsSlot);
        gearSlots.Add(legsSlot);
        gearSlots.Add(wield1Slot);
        gearSlots.Add(wield2Slot);
        gearSlots.Add(accessory1Slot);
        gearSlots.Add(accessory2Slot);

        // Test items
        gearItems.Add(new BodyWinter());
        gearItems.Add(new ArmsWinter());
        gearItems.Add(new LegsWinter());
        gearItems.Add(new AccessoryChrysanthemumCorola());
        gearItems.Add(new AccessoryBellSouvenir());
        gearItems.Add(new SwordAthanas());
        gearItems.Add(new SwordBell());
        gearItems.Add(new ShieldFloe());
        gearItems.Add(new BowAnemone());

        LoadGearSlots(new Nikolaos());
    }

    public void SelectGearSlot(Type type, GearItem gearItem)
    {
        Deselect();
        selectorItemList.Clear();
        Select(gearItem);

        foreach (GearItem item in gearItems)
        {
            if (item.GetType().BaseType.Equals(type))
            {
                selectorItemList.Add(item);
            }
        }

        LoadSelection(type);
    }

    public void Select(GearItem item)
    {
        if (item != null)
        {
            shownIcon.sprite = item.icon;
            shownName.text = item.name;
            shownDescription.text = item.description;
        }
    }

    public void LoadSelection(Type type)
    {
        foreach (GameObject slot in selectorSlotList)
        {
            Destroy(slot);
        }

        selectorSlotList.Clear();

        foreach (GearItem item in gearItems)
        {
            if (item.GetType().BaseType.Equals(type))
            {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(gearSelectionPane);
                slot.GetComponent<SlotController>().UpdateItem(item, 1);
                slot.GetComponent<SlotController>().index = gearItems.IndexOf(item);
                selectorSlotList.Add(slot);
            }
        }

        int index = gearSize;
        int hiddenItems = gearItems.Count - selectorSlotList.Count;

        while (selectorSlotList.Count < gearSize - hiddenItems)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(gearSelectionPane);
            slot.GetComponent<SlotController>().index = index;
            index++;
            selectorSlotList.Add(slot);
        }
    }

    public void LoadGearSlots(Profile profile)
    {
        headSlot.GetComponent<GearController>().SetType(typeof(HeadItem));
        bodySlot.GetComponent<GearController>().SetType(typeof(BodyItem));
        armsSlot.GetComponent<GearController>().SetType(typeof(ArmsItem));
        legsSlot.GetComponent<GearController>().SetType(typeof(LegsItem));
        wield1Slot.GetComponent<GearController>().SetType(profile.GetWield(1));
        wield2Slot.GetComponent<GearController>().SetType(profile.GetWield(2));
        accessory1Slot.GetComponent<GearController>().SetType(typeof(AccessoryItem));
        accessory2Slot.GetComponent<GearController>().SetType(typeof(AccessoryItem));

        foreach(GameObject slot in gearSlots)
        {
            slot.GetComponent<GearController>().Load();
        }
    }

    public void CloseSelector()
    {
        selectorSlotList.Clear();
    }

    public void Deselect()
    {
        shownIcon.sprite = emptyIcon;
        shownName.text = "";
        shownDescription.text = "";

        foreach (GameObject obj in gearSlots)
        {
            obj.GetComponent<GearController>().Deselect();
        }

        foreach (GameObject obj in selectorSlotList)
        {
            obj.GetComponent<SlotController>().Deselect();
        }
    }

    public void InitDrag(GearController slot)
    {
        if (dropped == true)
        {
            CommonInit();
            operationType = slot.gearType;
            fromSelector = false;
            ogSlot = slot;
            ogItem = slot.item;
            Select(slot.item);
            itemDragger.GetComponentInChildren<SlotController>().UpdateItem(slot.item, 1);
        }
    }

    public void InitDrag(SlotController slot)
    {
        if (dropped == true)
        {
            CommonInit();
            operationType = slot.item.GetType().BaseType;
            fromSelector = true;
            ogItem = slot.item;
            ogIndex = slot.index;
            Select((GearItem)slot.item);
            itemDragger.GetComponentInChildren<SlotController>().UpdateItem(slot.item, 1);
        }
    }

    void CommonInit()
    {
        switched = false;
        Deselect();
        dropped = false;
        itemDragger.SetActive(true);
    }

    public SlotController Drop(bool equipping)
    {
        toEquipment = equipping;

        if (dropped == false && itemDragger.GetComponentInChildren<SlotController>().item != null)
        {
            Deselect();
            dropped = true;
            SlotController toReturn = itemDragger.GetComponentInChildren<SlotController>();
            return toReturn;
        }
        return null;
    }

    public void CancelDrop()
    {
        dropped = false;
    }

    public void MismatchCorrection()
    {
        // When a GearItem is dragged from an ItemSlot to a GearSlot that does not match its type, the GearItem disappears.
        mismatchError = true;
        CancelDrop();
    }

    public void DragSwitch(GearController slot)
    {
        Item switchedItem = CommonSwitch(slot.item);
        if (fromSelector) selectorSlotList[ogIndex].GetComponent<SlotController>().UpdateItem(switchedItem, 1);
        else
        {
            if (ogItem.GetType().BaseType.Equals(switchedItem.GetType().BaseType)) ogSlot.UpdateGear(switchedItem);
            else
            {
                switched = false;
                CancelDrop();
            }
        }
    }

    public void DragSwitch(SlotController slot)
    {
        Item switchedItem = CommonSwitch(slot.item);
        if (fromSelector) selectorSlotList[ogIndex].GetComponent<SlotController>().UpdateItem(switchedItem, 1);
        else ogSlot.UpdateGear(switchedItem);
    }

    Item CommonSwitch(Item item)
    {
        switched = true;
        switchedItem = (GearItem)item;
        return item;
    }

    public bool SafeCheck()
    {
        if (dropped) return true;
        else return false;
    }

    public Item OriginalItem()
    {
        dropped = true;
        return ogItem;
    }

    public void EndOperation()
    {
        if (!mismatchError)
        {
            // Depending on the direction of the drag, remove or add to the list
            if (fromSelector && toEquipment)
            {
                Debug.Log("from SELECTOR to SLOTS");
                Debug.Log(gearItems[ogIndex].name);
                gearItems.Remove(gearItems[ogIndex]);
                if (switched)
                {
                    Debug.Log("SWITCH with");
                    Debug.Log(switchedItem.name);
                    gearItems.Add(switchedItem);
                }
            }
            if (!fromSelector && !toEquipment)
            {
                Debug.Log("from SLOTS to SELECTOR");
                Debug.Log(itemDragger.GetComponentInChildren<SlotController>().item.name);
                gearItems.Add((GearItem)itemDragger.GetComponentInChildren<SlotController>().item);
                if (switched)
                {
                    Debug.Log("SWITCH with");
                    Debug.Log(switchedItem.name);
                    gearItems.Remove(switchedItem);
                }
            }
        }

        // Save the current gear set
        partyController.currentProfile.SaveGear(
            headSlot.GetComponent<GearController>().item, bodySlot.GetComponent<GearController>().item,
            armsSlot.GetComponent<GearController>().item, legsSlot.GetComponent<GearController>().item,
            wield1Slot.GetComponent<GearController>().item, wield2Slot.GetComponent<GearController>().item,
            accessory1Slot.GetComponent<GearController>().item, accessory2Slot.GetComponent<GearController>().item);

        // Finalize by removing content from itemDragger and reloading the selector
        ogSlot = null;
        ogItem = null;
        mismatchError = false;
        itemDragger.GetComponentInChildren<SlotController>().Clear();
        itemDragger.SetActive(false);
        LoadSelection(operationType);
    }
}
