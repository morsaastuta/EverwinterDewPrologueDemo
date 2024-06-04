using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    ItemGatherer controller;

    // Data
    public ChestData data;
    [SerializeField] List<string> itemNames = new();
    [SerializeField] List<int> itemQtys;

    // Interaction
    [SerializeField] float range;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<DataHUB>().itemGatherer;

        Dictionary<Item, int> content = new();

        foreach (string item in itemNames) content.Add(ItemIndex.Generate(item), itemQtys[itemNames.IndexOf(item)]);

        data.UpdateContent(content);
    }

    void Update()
    {
        if (Physics.CheckSphere(transform.position, range, playerLayer))
        {
            if (controller.dataHUB.player.CompareKeyOnce(controller.dataHUB.player.interactKey, true)) Open(false);
        }
    }

    public void Open(bool forcefully)
    {
        if (!data.open && !forcefully)
        {
            bool success = true;
            Dictionary<Item, int> obtainedItems = new();

            foreach (Item item in data.content.Keys)
            {
                success = controller.dataHUB.player.AddItem(item, data.content.GetValueOrDefault(item));
                if (!success) break;
                obtainedItems.Add(item, data.content.GetValueOrDefault(item));
            }

            if (success)
            {
                data.open = true;
                animator.SetTrigger("open");
                Notify(obtainedItems);
            }
            else if (!success && obtainedItems.Count > 0)
            {
                animator.SetTrigger("openclose");
                Notify(obtainedItems);
            }
        }
        else if (forcefully) animator.SetTrigger("force");
    }

    public void Notify(Dictionary<Item, int> obtainedItems)
    {
        foreach (Item item in obtainedItems.Keys) data.content.Remove(item);
        controller.Notify(obtainedItems, false);
    }

    public void UpdateState()
    {
        if (data.open) Open(true);
    }
}
