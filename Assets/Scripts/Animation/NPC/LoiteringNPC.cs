using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoiteringNPC : MonoBehaviour
{
    public Animator animator;
    public Loitering movement;
    new public Transform camera;
    private int direction; // Back 0 - Right 1 - Front 2 - Left 3

    private void Update()
    {
        float npcDir = transform.rotation.eulerAngles.y + 45;
        float camDir = camera.rotation.eulerAngles.y + 45;

        if (npcDir > 360) npcDir -= 360; 
        if (camDir > 360) camDir -= 360;

        if (Mathf.Abs(camDir - npcDir) <= 45) direction = 0;
        else if ((camDir - npcDir <= 135 && camDir - npcDir > 45) || (npcDir - camDir <= 315 && npcDir - camDir > 225)) direction = 1;
        else if ((npcDir - camDir <= 135 && npcDir - camDir > 45) || (camDir - npcDir <= 315 && camDir - npcDir > 225)) direction = 3;
        else direction = 2;

        if (animator.GetInteger("velocity") != movement.velocity) animator.SetInteger("velocity", movement.velocity);
        if (animator.GetInteger("direction") != direction) animator.SetInteger("direction", direction);
    }
}
