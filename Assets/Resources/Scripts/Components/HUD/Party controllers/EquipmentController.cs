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

    // Item dragger
    [SerializeField] GameObject itemDragger;
    bool initialized = false;
    bool operating = false;
    bool fromSelector;
    bool toShowcase;
    Type operationType;
    bool switched = false;

    // Original dragged slots & new dropped slots
    GearController ogShowcaseSlot;
    SlotController ogSelectorSlot;
    GearItem ogItem;
    GearController newShowcaseSlot;
    SlotController newSelectorSlot;
    GearItem newItem;

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
    List<GameObject> gearSlots = new();

    // Gear selector list
    List<GearItem> selectorItemList = new();
    List<GameObject> selectorSlotList = new();

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

    // Audio
    [SerializeField] AudioMachine audioMachine;
    [SerializeField] AudioClip selectClip;
    [SerializeField] AudioClip dropInSelectorClip;
    [SerializeField] AudioClip dropInShowcaseClip;

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

        LoadGearSlots(partyController.playerProperties.currentProfile);
    }

    public void SelectGearSlot(Type type, GearItem gearItem)
    {
        audioMachine.PlaySFX(selectClip);

        Deselect();
        selectorItemList.Clear();
        Select(gearItem);

        foreach (GearItem item in partyController.playerProperties.armory)
        {
            if (item.GetType().BaseType.Equals(type)) selectorItemList.Add(item);
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
            case "SSA": nameIcon = iconF; affSTR = true; break;
            case "SRA": nameIcon = iconF; affRES = true; break;
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
        audioMachine.PlaySFX(selectClip);

        int shownStats = 0;
        if (item != null)
        {
            shownIcon.sprite = item.GetIcon();
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
        ClearSelection();

        foreach (GearItem item in partyController.playerProperties.armory)
        {
            if (item.GetType().BaseType.Equals(type))
            {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(gearSelectionPane);
                slot.GetComponent<SlotController>().UpdateItem(item, 1);
                slot.GetComponent<SlotController>().index = partyController.playerProperties.armory.IndexOf(item);
                selectorSlotList.Add(slot);
            }
        }

        int index = partyController.playerProperties.armorySize;
        int hiddenItems = partyController.playerProperties.armory.Count - selectorSlotList.Count;

        while (selectorSlotList.Count < partyController.playerProperties.armorySize - hiddenItems)
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(gearSelectionPane);
            slot.GetComponent<SlotController>().index = index;
            index++;
            selectorSlotList.Add(slot);
        }
    }

    public void ClearSelection()
    {
        foreach (GameObject slot in selectorSlotList) Destroy(slot);
        selectorSlotList.Clear();
    }

    public void LoadGearSlots(Profile profile)
    {
        headSlot.GetComponent<GearController>().SetType(typeof(HeadItem));
        headSlot.GetComponent<GearController>().UpdateGear(profile.head);

        bodySlot.GetComponent<GearController>().SetType(typeof(BodyItem));
        bodySlot.GetComponent<GearController>().UpdateGear(profile.body);

        armsSlot.GetComponent<GearController>().SetType(typeof(ArmsItem));
        armsSlot.GetComponent<GearController>().UpdateGear(profile.arms);

        legsSlot.GetComponent<GearController>().SetType(typeof(LegsItem));
        legsSlot.GetComponent<GearController>().UpdateGear(profile.legs);

        wield1Slot.GetComponent<GearController>().SetType(profile.GetWield(1));
        wield1Slot.GetComponent<GearController>().UpdateGear(profile.wield1);

        wield2Slot.GetComponent<GearController>().SetType(profile.GetWield(2));
        wield2Slot.GetComponent<GearController>().UpdateGear(profile.wield2);

        accessory1Slot.GetComponent<GearController>().SetType(typeof(AccessoryItem));
        accessory1Slot.GetComponent<GearController>().UpdateGear(profile.accessory1);

        accessory2Slot.GetComponent<GearController>().SetType(typeof(AccessoryItem));
        accessory2Slot.GetComponent<GearController>().UpdateGear(profile.accessory2);
    }

    public void Deselect()
    {
        ClearStatSlots();

        shownIcon.sprite = emptyIcon;
        shownName.text = "";
        shownDescription.text = "";

        foreach (GameObject obj in gearSlots) obj.GetComponent<GearController>().Deselect();
        foreach (GameObject obj in selectorSlotList) obj.GetComponent<SlotController>().Deselect();
    }

    public void InitDrag(GearController slot)
    {
        if (!operating)
        {
            CommonInit();
            fromSelector = false;
            ogItem = slot.item;
            ogShowcaseSlot = slot;
            operationType = ogShowcaseSlot.gearType;
            Select(ogItem);
            itemDragger.GetComponentInChildren<SlotController>().UpdateItem(ogItem);
            slot.Clear();
        }
    }

    public void InitDrag(SlotController slot)
    {
        if (!operating)
        {
            CommonInit();
            fromSelector = true;
            ogItem = (GearItem)slot.item;
            ogSelectorSlot = slot;
            operationType = ogSelectorSlot.item.GetType().BaseType;
            Select(ogItem);
            itemDragger.GetComponentInChildren<SlotController>().UpdateItem(ogItem);
            slot.Clear();
        }
    }

    void CommonInit()
    {
        Deselect();
        initialized = true;
        operating = true;
        itemDragger.SetActive(true);
    }

    public SlotController GetDropInfo(GearController slot)
    {
        if (operating)
        {
            if (itemDragger.GetComponentInChildren<SlotController>().item is not null)
            {
                toShowcase = true;
                Deselect();
                return itemDragger.GetComponentInChildren<SlotController>();
            }
        }
        else CancelOperation();

        return null;
    }

    public SlotController GetDropInfo(SlotController slot)
    {
        if (operating)
        {
            if (ogItem is not null)
            {
                toShowcase = false;
                Deselect();
                return itemDragger.GetComponentInChildren<SlotController>();
            }
        }
        else CancelOperation();

        return null;
    }

    public void SetDropResults(GearController slot)
    {
        if (operating & ogItem is not null)
        {
            newShowcaseSlot = slot;
            newItem = newShowcaseSlot.item;
            newShowcaseSlot.UpdateGear(ogItem);
            operating = false;
        }
        else CancelOperation();
    }

    public void SetDropResults(SlotController slot)
    {
        if (operating & ogItem is not null)
        {
            newSelectorSlot = slot;
            newItem = (GearItem)newSelectorSlot.item;
            newSelectorSlot.UpdateItem(ogItem);
            operating = false;
        }
        else CancelOperation();
    }

    public void DragSwitch(GearController slot)
    {
        // This goes to the showcase
        newItem = slot.item;
        if (fromSelector)
        {
            slot.UpdateGear(ogItem);
            ogSelectorSlot.UpdateItem(newItem);
            switched = true;
            operating = false;
        }
        else // From showcase
        {
            slot.UpdateGear(ogItem);
            ogShowcaseSlot.UpdateGear(newItem);
            switched = true;
            operating = false;
        }
    }

    public void DragSwitch(SlotController slot)
    {
        // This goes to the selector
        newItem = (GearItem)slot.item;
        if (fromSelector)
        {
            slot.UpdateItem(ogItem);
            ogSelectorSlot.UpdateItem(newItem);
            switched = true;
            operating = false;
        }
        else // From showcase
        {
            if (ogShowcaseSlot.gearType.Equals(newItem.GetType().BaseType))
            {
                slot.UpdateItem(ogItem);
                ogShowcaseSlot.UpdateGear(newItem);
                switched = true;
                operating = false;
            }
            else CancelOperation();
        }
    }

    void CancelOperation()
    {
        if (operating)
        {
            if (ogShowcaseSlot is not null) ogShowcaseSlot.UpdateGear(ogItem);
            else if (ogSelectorSlot is not null) ogSelectorSlot.UpdateItem(ogItem);

            if (newShowcaseSlot is not null) newShowcaseSlot.UpdateGear(newItem);
            else if (newSelectorSlot is not null) newSelectorSlot.UpdateItem(newItem);

            EndOperation();
        }

    }

    public void SafeEnd()
    {
        if (!initialized || operating) CancelOperation();
        else EndOperation();
    }

    public void EndOperation()
    {
        // Change player properties ONLY if the operation was not cancelled
        if (initialized && !operating)
        {
            // Depending on the direction of the drag, remove or add to the list
            if (toShowcase)
            {
                audioMachine.PlaySFX(dropInShowcaseClip);
                if (fromSelector)
                {
                    partyController.playerProperties.armory.Remove(ogItem);
                    if (switched) partyController.playerProperties.armory.Add(newItem);
                }
            }
            else
            {
                audioMachine.PlaySFX(dropInSelectorClip);
                if (!fromSelector)
                {
                    partyController.playerProperties.armory.Add(ogItem);
                    if (switched) partyController.playerProperties.armory.Remove(newItem);
                }
            }

            // Save the current gear set
            partyController.playerProperties.currentProfile.SaveGear(
                headSlot.GetComponent<GearController>().item, bodySlot.GetComponent<GearController>().item,
                armsSlot.GetComponent<GearController>().item, legsSlot.GetComponent<GearController>().item,
                wield1Slot.GetComponent<GearController>().item, wield2Slot.GetComponent<GearController>().item,
                accessory1Slot.GetComponent<GearController>().item, accessory2Slot.GetComponent<GearController>().item);
        }


        // Finalize by clearing persistent data, removing content from itemDragger and reloading the selector

        ogShowcaseSlot = null;
        ogSelectorSlot = null;
        ogItem = null;
        newShowcaseSlot = null;
        newSelectorSlot = null;
        newItem = null;
        initialized = false;
        operating = false;
        switched = false;

        itemDragger.GetComponentInChildren<SlotController>().Clear();
        itemDragger.SetActive(false);

        LoadSelection(operationType);
    }
}