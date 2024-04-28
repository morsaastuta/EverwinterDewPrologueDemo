using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData : Data
{
    // Player properties
    public bool hasBeenInit;
    public bool canMove;
    public bool canJump;
    public bool canRun;
    public bool canInteract;
    public bool canPause;
    public string sceneName;
    public Vector3 playerPos;
    public Quaternion playerAngle;
    public List<Profile> party = new();
    public Profile currentProfile;
    public Dictionary<Item,int> inventory = new();
    public List<ConsumableItem> consumableItems = new();
    public int consumablesSize;
    public List<MaterialItem> materialItems = new();
    public int materialsSize;
    public List<KeyItem> keyItems = new();
    public int keysSize;
    public List<GearItem> armory;
    public int armorySize;
    public List<KeyCode> northKey = new();
    public List<KeyCode> westKey = new();
    public List<KeyCode> eastKey = new();
    public List<KeyCode> southKey = new();
    public List<KeyCode> jumpKey = new();
    public List<KeyCode> runKey = new();
    public List<KeyCode> menuKey = new();
    public List<KeyCode> interactKey = new();
    public List<KeyCode> skipKey = new();

    // Camera properties
    public bool canZoom;
    public bool canPivot;
    public bool canRotate;
    public Vector3 cameraPos;
    public Quaternion cameraAngle;

    public SlotData(PlayerProperties p, CameraProperties c)
    {
        hasBeenInit = p.hasBeenInit;
        canMove = p.canMove;
        canJump = p.canJump;
        canRun = p.canRun;
        canInteract = p.canInteract;
        canPause = p.canPause;
        sceneName = p.sceneName;
        playerPos = p.playerPos;
        playerAngle = p.playerAngle;
        party = p.party;
        currentProfile = p.currentProfile;
        inventory = p.inventory;
        consumableItems = p.consumableItems;
        consumablesSize = p.consumablesSize;
        materialItems = p.materialItems;
        materialsSize = p.materialsSize;
        keyItems = p.keyItems;
        keysSize = p.keysSize;
        armory = p.armory;
        armorySize = p.armorySize;
        northKey = p.northKey;
        westKey = p.westKey;
        eastKey = p.eastKey;
        southKey = p.southKey;
        jumpKey = p.jumpKey;
        runKey = p.runKey;
        menuKey = p.menuKey;
        interactKey = p.interactKey;
        skipKey = p.skipKey;

        canZoom = c.canZoom;
        canPivot = c.canPivot;
        canRotate = c.canRotate;
        cameraPos = c.cameraPos;
        cameraAngle = c.cameraAngle;
    }

    public void LoadData(PlayerProperties p, CameraProperties c)
    {
        p.hasBeenInit = hasBeenInit;
        p.canMove = canMove;
        p.canJump = canJump;
        p.canRun = canRun;
        p.canInteract = canInteract;
        p.canPause = canPause;
        p.sceneName = sceneName;
        p.playerPos = playerPos;
        p.playerAngle = playerAngle;
        p.party = party;
        p.currentProfile = currentProfile;
        p.inventory = inventory;
        p.consumableItems = consumableItems;
        p.consumablesSize = consumablesSize;
        p.materialItems = materialItems;
        p.materialsSize = materialsSize;
        p.keyItems = keyItems;
        p.keysSize = keysSize;
        p.armory = armory;
        p.armorySize = armorySize;
        p.northKey = northKey;
        p.westKey = westKey;
        p.eastKey = eastKey;
        p.southKey = southKey;
        p.jumpKey = jumpKey;
        p.runKey = runKey;
        p.menuKey = menuKey;
        p.interactKey = interactKey;
        p.skipKey = skipKey;

        c.canZoom = canZoom;
        c.canPivot = canPivot;
        c.canRotate = canRotate;
        c.cameraPos = cameraPos;
        c.cameraAngle = cameraAngle;
    }
}
