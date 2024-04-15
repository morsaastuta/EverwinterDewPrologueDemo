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

    // Stat icons
    [SerializeField] Sprite iconHP;
    [SerializeField] Sprite iconMP;
    [SerializeField] Sprite iconATK;
    [SerializeField] Sprite iconDFN;
    [SerializeField] Sprite iconMAG;
    [SerializeField] Sprite iconDFL;
    [SerializeField] Sprite iconSPI;
    [SerializeField] Sprite iconACC;
    [SerializeField] Sprite iconCR;
    [SerializeField] Sprite iconCD;
    [SerializeField] Sprite iconSPD;
    [SerializeField] Sprite iconMOV;
    [SerializeField] Sprite iconP;
    [SerializeField] Sprite iconA;
    [SerializeField] Sprite iconF;
    [SerializeField] Sprite iconH;
    [SerializeField] Sprite typeSTR;
    [SerializeField] Sprite typeRES;

    // Stat slots
    [SerializeField] Image statImg1;
    [SerializeField] Image statImg2;
    [SerializeField] Image statImg3;
    [SerializeField] Image statImg4;
    [SerializeField] Image statImg5;
    [SerializeField] Image statImg6;
    [SerializeField] Image statImg7;
    [SerializeField] Image statImg8;
    [SerializeField] TextMeshProUGUI statTxt1;
    [SerializeField] TextMeshProUGUI statTxt2;
    [SerializeField] TextMeshProUGUI statTxt3;
    [SerializeField] TextMeshProUGUI statTxt4;
    [SerializeField] TextMeshProUGUI statTxt5;
    [SerializeField] TextMeshProUGUI statTxt6;
    [SerializeField] TextMeshProUGUI statTxt7;
    [SerializeField] TextMeshProUGUI statTxt8;
    [SerializeField] Image typeImg1;
    [SerializeField] Image typeImg2;
    [SerializeField] Image typeImg3;
    [SerializeField] Image typeImg4;
    [SerializeField] Image typeImg5;
    [SerializeField] Image typeImg6;
    [SerializeField] Image typeImg7;
    [SerializeField] Image typeImg8;

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

    void SetStatSlot(int position, string name, float value)
    {
        Sprite nameIcon = emptyIcon;
        int intValue = 0;
        bool affSTR = false;
        bool affRES = false;

        switch (name)
        {
            case "HP": nameIcon = iconHP; intValue = (int)value; break;
            case "MP": nameIcon = iconMP; intValue = (int)value; break;
            case "ATK": nameIcon = iconATK; intValue = (int)value; break;
            case "DFN": nameIcon = iconDFN; intValue = (int)value; break;
            case "MAG": nameIcon = iconMAG; intValue = (int)value; break;
            case "DFL": nameIcon = iconDFL; intValue = (int)value; break;
            case "SPI": nameIcon = iconSPI; intValue = (int)value; break;
            case "ACC": nameIcon = iconACC; break;
            case "CR": nameIcon = iconCR; break;
            case "CD": nameIcon = iconCD; break;
            case "SPD": nameIcon = iconSPD; intValue = (int)value; break;
            case "MOV": nameIcon = iconMOV; intValue = (int)value; break;
            case "PSA": nameIcon = iconP; affSTR = true; break;
            case "PRA": nameIcon = iconP; affRES = true; break;
            case "ASA": nameIcon = iconA; affSTR = true; break;
            case "ARA": nameIcon = iconA; affRES = true; break;
            case "FSA": nameIcon = iconF; affSTR = true; break;
            case "FRA": nameIcon = iconF; affRES = true; break;
            case "HSA": nameIcon = iconH; affSTR = true; break;
            case "HRA": nameIcon = iconH; affRES = true; break;
        }

        switch (position)
        {
            case 0:

                statImg1.sprite = nameIcon;

                if (intValue == 0) statTxt1.SetText(value.ToString() + "%");
                else statTxt1.SetText(intValue.ToString());

                if (affSTR) typeImg1.sprite = typeSTR;
                else if (affRES) typeImg1.sprite = typeRES;

                break;

            case 1:

                statImg2.sprite = nameIcon;

                if (intValue == 0) statTxt2.SetText(value.ToString() + "%");
                else statTxt2.SetText(intValue.ToString());

                if (affSTR) typeImg2.sprite = typeSTR;
                else if (affRES) typeImg2.sprite = typeRES;

                break;

            case 2:

                statImg3.sprite = nameIcon;

                if (intValue == 0) statTxt3.SetText(value.ToString() + "%");
                else statTxt3.SetText(intValue.ToString());

                if (affSTR) typeImg3.sprite = typeSTR;
                else if (affRES) typeImg3.sprite = typeRES;

                break;

            case 3:

                statImg4.sprite = nameIcon;

                if (intValue == 0) statTxt4.SetText(value.ToString() + "%");
                else statTxt4.SetText(intValue.ToString());

                if (affSTR) typeImg4.sprite = typeSTR;
                else if (affRES) typeImg4.sprite = typeRES;

                break;

            case 4:

                statImg5.sprite = nameIcon;

                if (intValue == 0) statTxt5.SetText(value.ToString() + "%");
                else statTxt5.SetText(intValue.ToString());

                if (affSTR) typeImg5.sprite = typeSTR;
                else if (affRES) typeImg5.sprite = typeRES;

                break;

            case 5:

                statImg6.sprite = nameIcon;

                if (intValue == 0) statTxt6.SetText(value.ToString() + "%");
                else statTxt6.SetText(intValue.ToString());

                if (affSTR) typeImg6.sprite = typeSTR;
                else if (affRES) typeImg6.sprite = typeRES;

                break;

            case 6:

                statImg7.sprite = nameIcon;

                if (intValue == 0) statTxt7.SetText(value.ToString() + "%");
                else statTxt2.SetText(intValue.ToString());

                if (affSTR) typeImg7.sprite = typeSTR;
                else if (affRES) typeImg7.sprite = typeRES;

                break;

            case 7:

                statImg8.sprite = nameIcon;

                if (intValue == 0) statTxt8.SetText(value.ToString() + "%");
                else statTxt8.SetText(intValue.ToString());

                if (affSTR) typeImg8.sprite = typeSTR;
                else if (affRES) typeImg8.sprite = typeRES;

                break;

        }
    }

    void ClearStatSlots()
    {
        statImg1.sprite = emptyIcon; statImg2.sprite = emptyIcon; statImg3.sprite = emptyIcon; statImg4.sprite = emptyIcon;
        statImg5.sprite = emptyIcon; statImg6.sprite = emptyIcon; statImg7.sprite = emptyIcon; statImg8.sprite = emptyIcon;

        typeImg1.sprite = emptyIcon; typeImg2.sprite = emptyIcon; typeImg3.sprite = emptyIcon; typeImg4.sprite = emptyIcon;
        typeImg5.sprite = emptyIcon; typeImg6.sprite = emptyIcon; typeImg7.sprite = emptyIcon; typeImg8.sprite = emptyIcon;

        statTxt1.SetText(""); statTxt2.SetText(""); statTxt3.SetText(""); statTxt4.SetText(""); statTxt5.SetText(""); statTxt6.SetText(""); statTxt7.SetText(""); statTxt8.SetText("");
    }

    public void Select(GearItem item)
    {
        int shownStats = 0;
        if (item != null)
        {
            shownIcon.sprite = item.icon;
            shownName.text = item.name;
            shownDescription.text = item.description;

            foreach (string statName in item.statByName.Keys)
            {
                if (item.statByName.ContainsKey(statName))
                {
                    float value = item.statByName.GetValueOrDefault(statName);
                    if (item.statByName.GetValueOrDefault(statName) != 0)
                    {
                        SetStatSlot(shownStats, statName, value);
                        if (shownStats++ >= 8) break;
                    }
                }
            }
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
        ClearStatSlots();

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
                gearItems.Remove(gearItems[ogIndex]);
                if (switched) gearItems.Add(switchedItem);
            }
            if (!fromSelector && !toEquipment)
            {
                gearItems.Add((GearItem)itemDragger.GetComponentInChildren<SlotController>().item);
                if (switched) gearItems.Remove(switchedItem);
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
