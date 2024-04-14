using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    [SerializeField] private MapProperties mapProperties;
    [SerializeField] private PlayerProperties player;

    [SerializeField] private List<string> messages;

    [SerializeField] private GameObject textHUD;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private CameraProperties cam;
    [SerializeField] private Loitering npc;
    [SerializeField] private Image profile;
    [SerializeField] private Sprite face;

    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;

    public bool interacting = false;
    private int currentMessage = 0;

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
