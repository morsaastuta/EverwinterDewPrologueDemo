using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObtainedItemSlot : MonoBehaviour
{
    // Important info
    [SerializeField] Sprite emptyIcon;

    // Checked info
    public bool occupied;
    public int index;

    // Called info
    public Item item;
    public int stock = 0;

    // Place where info is shown
    [SerializeField] Image icon;
    [SerializeField] GameObject stockBox;
    [SerializeField] TextMeshProUGUI stockCount;

    public void Awake()
    {
        icon.sprite = emptyIcon;
        Load();
    }

    public void UpdateItem(Item newItem, int newStock)
    {
        occupied = true;
        item = newItem;
        UpdateStock(newStock);
    }

    public void UpdateStock(int newStock)
    {
        stock = newStock;
    }

    public void CheckStock()
    {
        if (stock > 0) Load();
        else Clear();
    }

    public void Load()
    {
        if (occupied)
        {
            stockBox.SetActive(true);
            icon.sprite = item.GetIcon();
            stockCount.text = stock.ToString();
            if (!item.stackable) stockBox.SetActive(false);
        }
        else
        {
            stockBox.SetActive(false);
            icon.sprite = emptyIcon;
            stockCount.text = 0.ToString();
        }
    }

    public void Clear()
    {
        item = null;
        stock = 0;
        occupied = false;
        Load();
    }

    public void AskForInfo()
    {
        GetComponentInParent<ItemGatherer>().AskForInfo(item);
    }

    public void Dismiss()
    {
        GetComponentInParent<ItemGatherer>().Dismiss();
    }
}
