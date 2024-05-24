using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour
{
    [SerializeField] Sprite emptyIcon;
    ConsumableItem item;

    // Showcase
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Animator animator;
    [SerializeField] Image animIcon;

    // Cost meters
    [SerializeField] GameObject stock;
    [SerializeField] TextMeshProUGUI qty;

    bool selected = false;

    public void SetItem(ConsumableItem newItem)
    {
        item = newItem;
        Load();
    }

    public void Load()
    {
        Clear();

        icon.sprite = item.GetIcon();
        name.SetText(item.name);

        int itemStock = GetComponentInParent<ItemFrameController>().scene.playerProperties.inventory.GetQty(item);
        if (itemStock > 0)
        {
            qty.SetText(itemStock.ToString());
            stock.SetActive(true);
        }
    }

    public void Clear()
    {
        icon.sprite = emptyIcon;
        name.SetText("");
        stock.SetActive(false);
    }

    public void Select()
    {
        GetComponentInParent<ItemFrameController>().Select(item);

        if (!selected)
        {
            selected = true;
            animator.SetTrigger("select");
        }
        else Deselect();
    }

    public void Deselect()
    {
        selected = false;
        animator.SetTrigger("deselect");
        animIcon.sprite = emptyIcon;
    }
}
