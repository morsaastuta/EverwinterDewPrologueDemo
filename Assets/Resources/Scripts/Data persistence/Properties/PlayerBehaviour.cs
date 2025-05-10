using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerBehaviour : MonoBehaviour
{
    [OdinSerialize] public string sceneName;
    [OdinSerialize] public bool hasBeenInit;

    // Abilities
    [OdinSerialize] public bool canMove;
    [OdinSerialize] public bool canJump;
    [OdinSerialize] public bool canRun;
    [OdinSerialize] public bool canInteract;
    [OdinSerialize] public bool canPause;

    // Party data
    [OdinSerialize] public List<Profile> party = new();
    [OdinSerialize] public Profile currentProfile;

    // Inventory & Armory
    [OdinSerialize] public Inventory inventory = new();
    [OdinSerialize] public List<ConsumableItem> consumableItems = new();
    [OdinSerialize] public int consumablesSize = 15;
    [OdinSerialize] public List<MaterialItem> materialItems = new();
    [OdinSerialize] public int materialsSize = 30;
    [OdinSerialize] public List<KeyItem> keyItems = new();
    [OdinSerialize] public int keysSize = 5;
    [OdinSerialize] public List<GearItem> armory = new();
    [OdinSerialize] public int armorySize = 15;

    // Control keys
    [OdinSerialize] public List<KeyCode> northKey = new();
    [OdinSerialize] public List<KeyCode> westKey = new();
    [OdinSerialize] public List<KeyCode> southKey = new();
    [OdinSerialize] public List<KeyCode> eastKey = new();
    [OdinSerialize] public List<KeyCode> jumpKey = new();
    [OdinSerialize] public List<KeyCode> runKey = new();
    [OdinSerialize] public List<KeyCode> menuKey = new();
    [OdinSerialize] public List<KeyCode> interactKey = new();
    [OdinSerialize] public List<KeyCode> skipKey = new();
    [OdinSerialize] public List<KeyCode> holdKey = new();

    // Player status
    [OdinSerialize] public Vector3 playerPos;
    [OdinSerialize] public Quaternion playerAngle;
    public bool isInteracting;

    // Visuals
    Animator animator;

    // Exit combat
    [OdinSerialize] float storedXP = 0;
    [OdinSerialize] List<Item> storedItems = new();
    [SerializeField] ItemGatherer itemGatherer;
    [SerializeField] GrowthGatherer growthGatherer;

    void Awake()
    {
        animator = GetComponent<Animator>();

        // Initialization (once per savefile)
        if (!hasBeenInit)
        {
            // Test profiles
            party.Add(new Nikolaos());
            SelectCharacter(typeof(Nikolaos));

            // Test items
            AddItem(new SwordAthanas(), 1);
            AddItem(new ShieldFloe(), 1);

            // End
            hasBeenInit = true;
        }
    }

    public void Initialize()
    {
        RestoreParty();

        if (currentProfile is not null) SelectCharacter(currentProfile.GetType());
        else SelectCharacter(party[0].GetType());
    }

    public void Reload(PlayerBehaviour p)
    {
        sceneName = p.sceneName;
        hasBeenInit = p.hasBeenInit;
        canMove = p.canMove;
        canJump = p.canJump;
        canRun = p.canRun;
        canInteract = p.canInteract;
        canPause = p.canPause;

        party.Clear(); party.AddRange(p.party);
        SelectCharacter(p.currentProfile.GetType());

        inventory = p.inventory;
        consumableItems.Clear(); consumableItems.AddRange(p.consumableItems);
        consumablesSize = p.consumablesSize;
        materialItems.Clear(); materialItems.AddRange(p.materialItems);
        materialsSize = p.materialsSize;
        keyItems.Clear(); keyItems.AddRange(p.keyItems);
        keysSize = p.keysSize;
        armory.Clear(); armory.AddRange(p.armory);
        armorySize = p.armorySize;

        northKey.Clear(); northKey.AddRange(p.northKey);
        westKey.Clear(); westKey.AddRange(p.westKey);
        southKey.Clear(); southKey.AddRange(p.southKey);
        eastKey.Clear(); eastKey.AddRange(p.eastKey);
        jumpKey.Clear(); jumpKey.AddRange(p.jumpKey);
        runKey.Clear(); runKey.AddRange(p.runKey);
        menuKey.Clear(); menuKey.AddRange(p.menuKey);
        interactKey.Clear(); interactKey.AddRange(p.interactKey);
        skipKey.Clear(); skipKey.AddRange(p.skipKey);
        holdKey.Clear(); holdKey.AddRange(p.holdKey);

        playerPos = p.playerPos;
        playerAngle = p.playerAngle;

        storedXP = p.storedXP;
        storedItems = p.storedItems;

        LoadStatus();
        UpdateVisuals();
    }

    void Start()
    {
        foreach (Profile character in party) character.ChangeWield(0);

        SetLoot();
    }

    public void SaveState()
    {
        playerPos = transform.position;
        playerAngle = transform.rotation;
    }

    public void LoadStatus()
    {
        transform.SetPositionAndRotation(playerPos, playerAngle);
    }

    public void SetActive(bool state)
    {
        canMove = state;
        canJump = state;
        canPause = state;
    }

    public void SetRun(bool state)
    {
        // Temporarily cancelled
        canRun = false;
    }

    public void SetInteract(bool state)
    {
        canInteract = state;
    }

    public bool CompareKey(List<KeyCode> keyList)
    {
        foreach (KeyCode key in keyList) if (Input.GetKey(key)) return true;

        return false;
    }

    public bool CompareKeyOnce(List<KeyCode> keyList, bool init)
    {
        foreach (KeyCode key in keyList)
        {
            if (init)
            {
                if (Input.GetKeyDown(key)) return true;
            }
            else if (Input.GetKeyUp(key)) return true;
        }

        return false;
    }

    void ReloadInventory()
    {
        consumableItems.Clear();
        materialItems.Clear();
        keyItems.Clear();

        foreach (Item item in inventory.items)
        {
            if (item.GetType().BaseType.Equals(typeof(ConsumableItem))) consumableItems.Add((ConsumableItem)item);
            else if (item.GetType().BaseType.Equals(typeof(MaterialItem))) materialItems.Add((MaterialItem)item);
            else if (item.GetType().BaseType.Equals(typeof(KeyItem))) keyItems.Add((KeyItem)item);
        }
    }

    public void ReorganizeInventory(List<SlotController> itemsToReorganize)
    {
        if (itemsToReorganize.Count > 0)
        {
            Type typeToReorganize = itemsToReorganize[0].item.GetType().BaseType;

            // Remove all relevant items
            for (int i = inventory.GetSize(); i > 0; i--)
            {
                Item checkedItem = inventory.items.ElementAt(i - 1);
                if (checkedItem.GetType().BaseType.Equals(typeToReorganize)) inventory.Delete(checkedItem);
            }

            // Fill with new set of items
            foreach (SlotController slot in itemsToReorganize) AddItem(slot.item.Regenerate(), slot.stock);
        }
    }

    public List<ConsumableItem> GetConsumableItems()
    {
        ReloadInventory();

        return consumableItems;
    }

    public List<MaterialItem> GetMaterialItems()
    {
        ReloadInventory();

        return materialItems;
    }

    public List<KeyItem> GetKeyItems()
    {
        ReloadInventory();

        return keyItems;
    }

    public bool AddItem(Item newItem, int qty)
    {
        Type type = newItem.GetType().BaseType;
        ReloadInventory();

        if ((type.Equals(typeof(ConsumableItem)) && consumableItems.Count < consumablesSize) || (type.Equals(typeof(MaterialItem)) && materialItems.Count < materialsSize) || (type.Equals(typeof(KeyItem)) && keyItems.Count < keysSize))
        {
            inventory.Add(newItem, qty);
            ReloadInventory();
            return true;
        }
        else if (type.BaseType.Equals(typeof(GearItem)) || type.BaseType.Equals(typeof(WieldItem)))
        {
            bool returnedVal = true;

            for (int i = 0; i < qty; i++)
            {
                returnedVal = AddGear((GearItem)newItem);
                if (!returnedVal) break;
            }

            return returnedVal;
        }
        else
        {
            ReloadInventory();
            return false;
        }
    }

    bool AddGear(GearItem newItem)
    {
        if (armory.Count < armorySize)
        {
            armory.Add(newItem);
            return true;
        }
        else return false;
    }

    public void SelectCharacter(Type character)
    {
        foreach (Profile profile in party)
        {
            if (profile.GetType().Equals(character))
            {
                currentProfile = profile;
                break;
            }
        }

        UpdateVisuals();
    }

    public void RestoreParty()
    {
        foreach (Profile profile in party) profile.FullRestore();
    }

    public void InitializeAP()
    {
        foreach (Profile profile in party) profile.ReloadAP();
    }

    public void MaximizeAP()
    {
        foreach (Profile profile in party) profile.ChangeAP(profile.statAP);
    }

    public void UpdateVisuals()
    {
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            animator.SetInteger("direction", 2);
            animator.SetInteger("velocity", 0);
        }
    }

    public void GetLoot(FoeData foe)
    {
        storedXP += foe.lootXP;

        foreach (Item item in foe.lootItems.Keys) if (UnityEngine.Random.Range(0f, 1f) >= foe.lootItems.GetValueOrDefault(item)) storedItems.Add(item);
    }

    public void SetLoot()
    {
        if (storedXP > 0 || storedItems.Count > 0)
        {
            Dictionary<Item, int> obtainedItems = new();
            int skillsetThreshold = currentProfile.skillset.Count;
            int prev = currentProfile.level;
            int prevMJ = 0;
            if (currentProfile.mainJob is not null) prevMJ = currentProfile.mainJob.level;
            int prevSJ = 0;
            if (currentProfile.subJob is not null) prevSJ = currentProfile.subJob.level;

            foreach (Profile character in party) character.ObtainXP((int)storedXP);

            foreach (Item item in storedItems)
            {
                if (!obtainedItems.ContainsKey(item)) obtainedItems.Add(item, 1);
                else obtainedItems[item]++;
                
                AddItem(item, 1);
            }

            List<Skill> newSkillset = new();
            newSkillset.AddRange(currentProfile.skillset);
            newSkillset.RemoveRange(0, skillsetThreshold);

            itemGatherer.Notify(obtainedItems, true);
            growthGatherer.Notify(currentProfile, prev, (int)storedXP, prevMJ, (int)(storedXP/2), prevSJ, (int)(storedXP/3), newSkillset);

            storedXP = 0;
            storedItems.Clear();
        }

    }
}
