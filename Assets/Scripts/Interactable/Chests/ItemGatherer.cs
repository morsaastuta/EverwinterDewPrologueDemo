using System.Collections.Generic;
using UnityEngine;

public class ItemGatherer : MonoBehaviour
{
    [SerializeField] public DataHUB dataHUB;

    [SerializeField] GameObject notification;
    [SerializeField] SlotGenerator slotGenerator;
    [SerializeField] ObtainedItemHelper helper;
    bool gg = false;

    public void Notify(Dictionary<Item, int> obtainedItems, bool xpRightAfter)
    {
        gg = xpRightAfter;

        dataHUB.player.SetActive(false);
        dataHUB.camera.SetActive(false);
        notification.SetActive(true);

        foreach (Item obtainedItem in obtainedItems.Keys)
        {
            ObtainedItemSlot slot = slotGenerator.Generate().GetComponent<ObtainedItemSlot>();
            slot.UpdateItem(obtainedItem, obtainedItems.GetValueOrDefault(obtainedItem));
            slot.Load();
        }
    }

    public void AskForInfo(Item item)
    {
        helper.CallForHelp(item);
    }

    public void Dismiss()
    {
        helper.Dismiss();
    }

    public void Close()
    {
        foreach (Transform slot in slotGenerator.transform) Destroy(slot.gameObject);

        Dismiss();
        notification.SetActive(false);
        dataHUB.camera.SetActive(true);
        dataHUB.player.SetActive(true);

        if (gg) dataHUB.growthGatherer.Launch();
    }
}