using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData : Data
{
    // Player properties
    public bool canMove;
    public bool canJump;
    public bool canRun;
    public bool canInteract;
    public bool canPause;
    public string sceneName;
    public Vector3 playerPos;
    public Quaternion playerAngle;
    public List<KeyCode> northKey;
    public List<KeyCode> westKey;
    public List<KeyCode> eastKey;
    public List<KeyCode> southKey;
    public List<KeyCode> jumpKey;
    public List<KeyCode> runKey;
    public List<KeyCode> menuKey;
    public List<KeyCode> interactKey;
    public List<KeyCode> skipKey;

    // Camera properties
    public bool canZoom;
    public bool canPivot;
    public bool canRotate;
    public Vector3 cameraPos;
    public Quaternion cameraAngle;

    public SlotData(PlayerProperties p, CameraProperties c)
    {
        canMove = p.canMove;
        canJump = p.canJump;
        canRun = p.canRun;
        canInteract = p.canInteract;
        canPause = p.canPause;
        sceneName = p.sceneName;
        playerPos = p.playerPos;
        playerAngle = p.playerAngle;
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
        p.canMove = canMove;
        p.canJump = canJump;
        p.canRun = canRun;
        p.canInteract = canInteract;
        p.canPause = canPause;
        p.sceneName = sceneName;
        p.playerPos = playerPos;
        p.playerAngle = playerAngle;
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
