using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavingSystem : MonoBehaviour
{
    [SerializeField] PlayerProperties properties;
    [SerializeField] CameraProperties camProperties;
    [SerializeField] MapProperties mapProperties;
    [SerializeField] DataPersistenceManager dpm;

    [SerializeField] GameObject savePane;
    [SerializeField] GameObject optionPane;
    [SerializeField] GameObject slotPane;
    [SerializeField] List<GameObject> slots;

    int selectedSlot;
    string mode;

    [SerializeField] PromptController promptController;

    public void Initialize()
    {
        savePane.SetActive(true);
        optionPane.SetActive(true);
        properties.SetActive(false);
        camProperties.SetActive(false);
        mapProperties.SetPaused(true);
        SlotsEnabled(true);
    }

    public void TitleLoad()
    {
        savePane.SetActive(true);
        SlotsEnabled(true);
        LoadMode();
    }

    public void SaveMode()
    {
        mode = "save";
        SlotsActive(true);
        SlotsEnabled(true);
    }

    public void LoadMode()
    {
        mode = "load";
        SlotsActive(true);
        SlotsEnabled(true);
        ReadyUsableSlots();
    }

    public void SelectSlot(int slot)
    {
        selectedSlot = slot;
        SlotsEnabled(false);
        Prompt();
    }

    public void Prompt()
    {
        switch (mode)
        {
            case "save":
                promptController.InitPrompt("Do you really want to save the current progress in this slot?", "save");
                break;
            case "load":
                promptController.InitPrompt("Do you really want to load this save file?", "save");
                break;
        }
    }

    public void ReceiveAnswer(bool answer)
    {
        SlotsEnabled(true);
        if (answer) Complete();
    }

    public void Complete()
    {
        Terminate();
        switch(mode)
        {
            case "save":
                dpm.SaveGame(selectedSlot);
                break;
            case "load":
                dpm.LoadGame(selectedSlot);
                break;
        }
    }

    public void Terminate()
    {
        SlotsActive(false);
        savePane.SetActive(false);
        properties.SetActive(true);
        camProperties.SetActive(true);
        mapProperties.SetPaused(false);
    }

    public void SlotsActive(bool state)
    {
        try
        {
            optionPane.SetActive(!state);
        } catch {}

        slotPane.SetActive(state);
    }

    private void SlotsEnabled(bool state)
    {
        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Button>().interactable = state;
        }
        if (mode == "load") ReadyUsableSlots();
    }

    private void ReadyUsableSlots()
    {
        foreach (int i in dpm.CheckExistingFiles())
        {
            slots[i].GetComponent<Button>().interactable = false;
        }
    }
}
