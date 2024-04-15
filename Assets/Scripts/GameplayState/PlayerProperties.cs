using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] private DataPersistenceManager dpm;
    [SerializeField] public string sceneName;

    // Abilities
    public bool canMove;
    public bool canJump;
    public bool canRun;
    public bool canInteract;
    public bool canPause;

    // Control keys
    public List<KeyCode> northKey;
    public List<KeyCode> westKey;
    public List<KeyCode> eastKey;
    public List<KeyCode> southKey;
    public List<KeyCode> jumpKey;
    public List<KeyCode> runKey;
    public List<KeyCode> menuKey;
    public List<KeyCode> interactKey;
    public List<KeyCode> skipKey;

    // Player info
    public Vector3 playerPos;
    public Quaternion playerAngle;

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
        transform.position = playerPos;
        transform.rotation = playerAngle;
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
        foreach (KeyCode key in keyList)
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }
        return false;
    }

    public bool CompareKeyOnce(List<KeyCode> keyList, bool init)
    {
        foreach (KeyCode key in keyList)
        {
            if (init)
            {
                if (Input.GetKeyDown(key))
                {
                    return true;
                }
            } else
            {
                if (Input.GetKeyUp(key))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
