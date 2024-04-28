using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] DataPersistenceManager dpm;
    public string sceneName;
    public bool hasBeenInit;

    // Abilities
    public bool canMove;
    public bool canJump;
    public bool canRun;
    public bool canInteract;
    public bool canPause;

    // Party data
    public List<Profile> party = new();
    public Profile currentProfile;

    // Item data
    public Dictionary<Item, int> inventory = new();
    public List<ConsumableItem> consumableItems = new();
    public int consumablesSize = 15;
    public List<MaterialItem> materialItems = new();
    public int materialsSize = 30;
    public List<KeyItem> keyItems = new();
    public int keysSize = 5;

    // Gear data
    public List<GearItem> armory = new();
    public int armorySize = 15;

    // Control keys
    public List<KeyCode> northKey = new();
    public List<KeyCode> westKey = new();
    public List<KeyCode> eastKey = new();
    public List<KeyCode> southKey = new();
    public List<KeyCode> jumpKey = new();
    public List<KeyCode> runKey = new();
    public List<KeyCode> menuKey = new();
    public List<KeyCode> interactKey = new();
    public List<KeyCode> skipKey = new();

    // Player info
    public Vector3 playerPos;
    public Quaternion playerAngle;

    private void Awake()
    {
        if (!hasBeenInit)
        {
            Debug.Log("its happening!!!");
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
                    case 0:
                        item = new RationI();
                        break;
                    case 1:
                        item = new RationII();
                        break;
                    case 2:
                        item = new EtherI();
                        break;
                    case 3:
                        item = new FlowerSnowdrop();
                        break;
                    case 4:
                        item = new HerbsThrascias();
                        break;
                    case 5:
                        item = new ClawRimebear();
                        break;
                    case 6:
                        item = new PeltRimebear();
                        break;
                    case 7:
                        item = new Galanthus();
                        break;
                }
                AddItem(item, stock);
            }

            // End
            hasBeenInit = true;
        }
    }

    void Start()
    {
        dpm.LoadGame(0);
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
        canRun = state;
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
    }
}
