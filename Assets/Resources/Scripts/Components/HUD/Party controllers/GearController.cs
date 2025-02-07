using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GearController : MonoBehaviour, IPointerClickHandler
{
    // Important info
    [SerializeField] Sprite gearSymbol;
    [SerializeField] Sprite wieldSymbol;
    [SerializeField] Sprite emptyHead;
    [SerializeField] Sprite emptyBody;
    [SerializeField] Sprite emptyArms;
    [SerializeField] Sprite emptyLegs;
    [SerializeField] Sprite emptyAccessory;
    [SerializeField] Sprite emptySword;
    [SerializeField] Sprite emptyShield;
    [SerializeField] Sprite emptyBow;

    // Checked info
    public bool occupied;
    public Type gearType;

    // Called info
    public GearItem item;
    Sprite defaultSymbol;
    Sprite defaultIcon;
    [SerializeField] Sprite emptyIcon;

    // Place where info is shown
    [SerializeField] Image icon;
    [SerializeField] Image typeIcon;
    [SerializeField] Image symbol;
    [SerializeField] Image animIcon;
    [SerializeField] Animator animator;

    // Controllers
    EquipmentController equipmentController;

    public void Awake()
    {
        Deselect();
        equipmentController = GetComponentInParent<EquipmentController>();
    }

    public void SetType(Type type)
    {
        gearType = type;
    }

    public void Load()
    {
        if (!gearType.BaseType.Equals(typeof(WieldItem)))
        {
            defaultSymbol = gearSymbol;
            if (gearType.Equals(typeof(HeadItem))) defaultIcon = emptyHead;
            else if (gearType.Equals(typeof(BodyItem))) defaultIcon = emptyBody;
            else if (gearType.Equals(typeof(ArmsItem))) defaultIcon = emptyArms;
            else if (gearType.Equals(typeof(LegsItem))) defaultIcon = emptyLegs;
            else if (gearType.Equals(typeof(AccessoryItem))) defaultIcon = emptyAccessory;
        }
        else
        {
            defaultSymbol = wieldSymbol;
            if (gearType.Equals(typeof(SwordItem))) defaultIcon = emptySword;
            else if (gearType.Equals(typeof(ShieldItem))) defaultIcon = emptyShield;
            else if (gearType.Equals(typeof(BowItem))) defaultIcon = emptyBow;
        }

        symbol.sprite = defaultSymbol;
        if (occupied)
        {
            icon.sprite = item.GetIcon();
            typeIcon.sprite = emptyIcon;
        }
        else
        {
            icon.sprite = emptyIcon;
            typeIcon.sprite = defaultIcon;
        }
    }

    public void Clear()
    {
        item = null;
        occupied = false;
        Load();
    }

    public void UpdateGear(Item newItem)
    {
        if (newItem != null)
        {
            occupied = true;
            item = (GearItem)newItem;
            Load();
        }
        else Clear();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) Select();
    }

    public void Select()
    {
        equipmentController.Deselect();
        equipmentController.SelectGearSlot(gearType, item);
        animator.SetTrigger("select");
    }

    public void Deselect()
    {
        animator.SetTrigger("deselect");
        animIcon.sprite = emptyIcon;
    }

    public void Drop()
    {
        SlotController draggedSlot = equipmentController.GetDropInfo(this);

        if (draggedSlot is not null && draggedSlot.occupied && draggedSlot.item.GetType().BaseType.Equals(gearType))
        {
            if (occupied) equipmentController.DragSwitch(this);
            else equipmentController.SetDropResults(this);
        }
    }

    public void InitDrag()
    {
        if (occupied) equipmentController.InitDrag(this);
    }

    public void EndDrag()
    {
        equipmentController.SafeEnd();
    }
}
