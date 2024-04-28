using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler
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
    [SerializeField] Image animIcon;
    [SerializeField] Animator animator;

    public void Awake()
    {
        Deselect();
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
        CheckStock();
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
            if (!item.stackable)
            {
                stockBox.SetActive(false);
            }
        } else
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) Select();
    }

    public void Select()
    {
        if (occupied)
        {
            if (GetComponentInParent<InventoryController>() != null)
            {
                GetComponentInParent<InventoryController>().Deselect();
                GetComponentInParent<InventoryController>().Select(index);
            }
            else if (GetComponentInParent<EquipmentController>() != null)
            {
                GetComponentInParent<EquipmentController>().Deselect();
                GetComponentInParent<EquipmentController>().Select((GearItem)item);
            }
            animator.SetTrigger("select");
        }
    }

    public void Deselect()
    {
        animator.SetTrigger("deselect");
        animIcon.sprite = emptyIcon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Only InventoryController takes note of Extractions
        if (GetComponentInParent<InventoryController>() != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left) InitDrag(false);
            else if (eventData.button == PointerEventData.InputButton.Right) InitDrag(true);
        }
        else if (GetComponentInParent<EquipmentController>() != null)
        {
            InitDrag(false);
        }
    }

    public void InitDrag(bool extractMode)
    {
        if(occupied)
        {
            // If the drag is initialized by the LEFT click, extract ALL
            if (!extractMode)
            {
                if (GetComponentInParent<InventoryController>() != null) GetComponentInParent<InventoryController>().InitDrag(this);
                else if (GetComponentInParent<EquipmentController>() != null) GetComponentInParent<EquipmentController>().InitDrag(this);
                Clear();
            }
            // If the drag is initialized by the RIGHT click, extract ONE
            else Extract();
        }
    }

    public void Extract()
    {
        GetComponentInParent<InventoryController>().Extract(this);
        UpdateStock(stock-1);
    }

    public void Drop()
    {
        SlotController draggedSlot = null;

        if (GetComponentInParent<InventoryController>() != null)
        {
            draggedSlot = GetComponentInParent<InventoryController>().Drop();
        }
        else if (GetComponentInParent<EquipmentController>() != null)
        {
            draggedSlot = GetComponentInParent<EquipmentController>().Drop(false);
        }

        // Check if the receiving slot is occupied
        if (occupied)
        {
            // Check if the item is the same AND stackable
            if (item.GetType().Equals(draggedSlot.item.GetType()) && item.stackable)
            {
                // If it is, FUSE
                UpdateStock(stock + draggedSlot.stock);
            }
            else
            {
                // In case the items are not stackable, check if the drag is an extraction (only on InventoryController)
                if (GetComponentInParent<EquipmentController>() == null)
                {
                    if (GetComponentInParent<InventoryController>().OriginalStock() == 1 || !GetComponentInParent<InventoryController>().CheckExtraction())
                    {
                        // If the items are not the same, and the original slot had stock = 1 OR the drag is not an extraction (that is, if the dragged slot has stock = 1), SWITCH
                        GetComponentInParent<InventoryController>().DragSwitch(item, stock);
                        UpdateItem(draggedSlot.item, draggedSlot.stock);
                    }
                } else
                {
                    // On EquipmentController, switch directly
                    GetComponentInParent<EquipmentController>().DragSwitch(this);
                    UpdateItem(draggedSlot.item, draggedSlot.stock);
                }
            }
        }
        else
        {
            // If the receiving slot is unoccupied, simply update
            UpdateItem(draggedSlot.item, draggedSlot.stock);
        }
    }

    public void EndDrag()
    {
        if (GetComponentInParent<InventoryController>() != null)
        {
            if (!GetComponentInParent<InventoryController>().SafeCheck())
            {
                UpdateItem(GetComponentInParent<InventoryController>().OriginalItem(), GetComponentInParent<InventoryController>().OriginalStock());
            }
        }
        else if (GetComponentInParent<EquipmentController>() != null)
        {
            if (!GetComponentInParent<EquipmentController>().SafeCheck())
            {
                UpdateItem(GetComponentInParent<EquipmentController>().OriginalItem(), 1);
            }

            GetComponentInParent<EquipmentController>().EndOperation();
        }
    }
}
