using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] DataHUB dataHUB;
    [SerializeField] TextHUB textHUB;

    DialogueData dialogueData; // Current dialogue data
    int messageIndex = 0; // Current message's index

    List<string> nameLog = new();
    List<string> messageLog = new();

    public bool isEvent = false;
    public DialogueEventController dec;

    [SerializeField] AudioMachine audioMachine;
    [SerializeField] AudioClip messageClip;

    void Update()
    {
        if (dataHUB.player.isInteracting)
        {
            if (!isEvent)
            {
                if (dataHUB.player.CompareKeyOnce(dataHUB.player.interactKey, true)) Interact();
                else if (dataHUB.player.CompareKeyOnce(dataHUB.player.skipKey, true)) EndInteraction();
            }
            else if (isEvent)
            {
                if (dec.movingActor.Count == 0 && dataHUB.player.CompareKeyOnce(dataHUB.player.interactKey, true)) Interact();
            }
        }
    }

    public void StartInteraction(DialogueData newDialogueData)
    {
        if (dataHUB.player.canInteract)
        {
            nameLog.Clear();
            messageLog.Clear();
            dialogueData = newDialogueData;
            dataHUB.player.isInteracting = true;
            dataHUB.camera.SetActive(false);
            dataHUB.player.SetActive(false);
            textHUB.HUD.SetActive(true);
            messageIndex = 0;
            Interact();
        }
    }

    void Interact()
    {
        audioMachine.PlaySFX(messageClip);
        if (messageIndex > dialogueData.order.Capacity - 1) EndInteraction();
        else
        {
            int characterIndex = dialogueData.order[messageIndex];
            if (isEvent) dec.CheckIndex(messageIndex);

            textHUB.face.sprite = dialogueData.characterFaces[characterIndex];

            if (dialogueData.characterNames[characterIndex] != "")
            {
                textHUB.namebox.SetActive(true);
                textHUB.name.SetText(dialogueData.characterNames[characterIndex]);
                nameLog.Add(dialogueData.characterNames[characterIndex]);
                messageLog.Add(dialogueData.messages[messageIndex]);
            }
            else
            {
                textHUB.namebox.SetActive(false);
                nameLog.Add("???");
            }

            textHUB.dialogue.SetText(dialogueData.messages[messageIndex]);
            messageLog.Add(dialogueData.messages[messageIndex]);
        }
        messageIndex++;
    }

    public void EndInteraction()
    {
        textHUB.namebox.SetActive(false);
        textHUB.HUD.SetActive(false);
        dataHUB.camera.SetActive(true);
        dataHUB.player.SetActive(true);
        dataHUB.player.isInteracting = false;
    }
}
