using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProperties : MonoBehaviour
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
    [OdinSerialize] public Dictionary<Item, int> inventory = new();
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

    // Player info
    [OdinSerialize] public Vector3 playerPos;
    [OdinSerialize] public Quaternion playerAngle;

    // Visuals
    Animator animator;

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
            AddGear(new BodyWinter());
            AddGear(new ArmsWinter());
            AddGear(new LegsWinter());
            AddGear(new AccessoryChrysanthemumCorola());
            AddGear(new AccessoryBellSouvenir());
            AddGear(new SwordAthanas());
            AddGear(new SwordBell());
            AddGear(new ShieldFloe());
            AddGear(new BowAnemone());
            for (int i = 0; i < UnityEngine.Random.Range(15, 31); i++)
            {
                Item item = null;
                int stock = UnityEngine.Random.Range(1, 100);
                switch (UnityEngine.Random.Range(0, 8))
                {
                    case 0: item = new RationI(); break;
                    case 1: item = new RationII(); break;
                    case 2: item = new EtherI(); break;
                    case 3: item = new FlowerSnowdrop(); break;
                    case 4: item = new HerbsThrascias(); break;
                    case 5: item = new ClawRimebear(); break;
                    case 6: item = new PeltRimebear(); break;
                    case 7: item = new Galanthus(); break;
                }
                AddItem(item, stock);
            }

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

    public void Reload(PlayerProperties p)
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

        LoadStatus();
        UpdateVisuals();
    }

    public void SaveStatus()
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

        foreach (Item item in inventory.Keys)
        {
            if (item.GetType().BaseType.Equals(typeof(ConsumableItem))) consumableItems.Add((ConsumableItem)item);
            else if (item.GetType().BaseType.Equals(typeof(MaterialItem))) materialItems.Add((MaterialItem)item);
            else if (item.GetType().BaseType.Equals(typeof(KeyItem))) keyItems.Add((KeyItem)item);
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
        else
        {
            ReloadInventory();
            return false;
        }
    }

    public bool AddGear(GearItem newItem)
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

    public void UpdateVisuals()
    {
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
            animator.SetInteger("direction", 2);
            animator.SetInteger("velocity", 0);
        }
    }
}