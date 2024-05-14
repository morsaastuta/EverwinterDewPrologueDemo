using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    [SerializeField] MapProperties mapProperties;
    [SerializeField] PlayerProperties player;

    [SerializeField] List<string> messages;

    [SerializeField] GameObject textHUD;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] CameraProperties cam;
    [SerializeField] Loitering npc;
    [SerializeField] Image profile;
    [SerializeField] Sprite face;

    [SerializeField] float range;
    [SerializeField] LayerMask playerLayer;

    public bool interacting = false;
    int currentMessage = 0;

    void Update()
    {
        if (!mapProperties.pausedGame)
        {
            if (!interacting)
            {
                if (Physics.CheckSphere(transform.position, range, playerLayer))
                {
                    if (player.CompareKeyOnce(player.interactKey, true))
                    {
                        StartInteraction();
                        Interact();
                    }
                }
            }
            // In case it IS interacting...
            else if (player.CompareKeyOnce(player.interactKey, true)) Interact();
            else if (player.CompareKeyOnce(player.skipKey, true)) EndInteraction();
        }
    }

    void StartInteraction()
    {
        if(player.canInteract)
        {
            interacting = true;
            cam.SetActive(false);
            player.SetActive(false);
            textHUD.SetActive(true);
            currentMessage = 0;
            profile.sprite = face;
        }
    }

    void Interact()
    {
        foreach (string message in messages)
        {
            if (currentMessage > messages.Capacity - 1) EndInteraction();
            else if (currentMessage == messages.IndexOf(message)) textBox.text = message;
        }
        currentMessage++;
    }

    void EndInteraction()
    {
        textHUD.SetActive(false);
        cam.SetActive(true);
        player.SetActive(true);
        interacting = false;
    }
}
