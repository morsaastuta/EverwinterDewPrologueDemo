using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemFrameController : MonoBehaviour
{
    [SerializeField] public CombatController scene;
    [SerializeField] SlotGenerator slotGenerator;
    [SerializeField] RectTransform itemSelectionPane;
    List<GameObject> itemSlots = new();

    // Audio
    [SerializeField] AudioMachine audioMachine;
    [SerializeField] AudioClip selectClip;

    public void Consumables()
    {
        ClearItems();

        foreach (ConsumableItem item in scene.playerProperties.GetConsumableItems())
        {
            GameObject slot = slotGenerator.Generate();
            slot.transform.SetParent(itemSelectionPane);
            slot.GetComponent<ItemSlotController>().SetItem(item);
            itemSlots.Add(slot);
        }
    }

    public void ClearItems()
    {
        Deselect();

        foreach (GameObject obj in itemSlots)
        {
            Destroy(obj);
        }

        itemSlots.Clear();
    }

    public void Deselect()
    {
        foreach (GameObject obj in itemSlots)
        {
            obj.GetComponent<ItemSlotController>().Deselect();
        }
    }

    public void Select(ConsumableItem item)
    {
        audioMachine.PlaySFX(selectClip);
        if (scene.mode.Equals("use"))
        {
            scene.ReturnMode();
            if (!item.Equals(scene.selectedItem)) scene.SelectItem(item);
        }
        else scene.SelectItem(item);
    }

    public void Exit()
    {
        scene.ReturnMode();
    }
}
