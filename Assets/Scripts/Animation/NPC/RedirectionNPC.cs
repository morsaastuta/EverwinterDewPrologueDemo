using UnityEngine;

public class RedirectionNPC : MonoBehaviour
{
    [SerializeField] Transform npc;
    public int direction; // Back 0 - Right 1 - Front 2 - Left 3

    void Update()
    {
        float npcDir = npc.rotation.eulerAngles.y + 45;
        float camDir = Camera.main.transform.rotation.eulerAngles.y + 45;

        if (npcDir > 360) npcDir -= 360;
        if (camDir > 360) camDir -= 360;

        if (Mathf.Abs(camDir - npcDir) <= 45) direction = 0;
        else if ((camDir - npcDir <= 135 && camDir - npcDir > 45) || (npcDir - camDir <= 315 && npcDir - camDir > 225)) direction = 1;
        else if ((npcDir - camDir <= 135 && npcDir - camDir > 45) || (camDir - npcDir <= 315 && camDir - npcDir > 225)) direction = 3;
        else direction = 2;

        if (GetComponent<Animator>().runtimeAnimatorController != null)
        {
            if (GetComponent<Animator>().GetInteger("direction") != direction)
            {
                GetComponent<Animator>().SetInteger("direction", direction);
            }
        }
    }
}
