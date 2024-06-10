using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    // Subsequent controllers
    [SerializeField] PartyController partyController;
    [SerializeField] InventoryController inventoryController;
    [SerializeField] GuidebookController guidebookController;
    [SerializeField] HelpCall helper;

    // External properties
    [SerializeField] DataHUB dataHUB;

    // Internal properties
    int cooldownMax = 10;
    int cooldown;
    string task;

    // Prompt controller
    [SerializeField] PromptController promptController;

    // Main menu
    [SerializeField] GameObject menuBox;
    [SerializeField] GameObject optParty;
    [SerializeField] GameObject optInventory;
    [SerializeField] GameObject optSettings;
    [SerializeField] GameObject optExit;

    // Party submenu
    [SerializeField] GameObject partyFrame;
    [SerializeField] GameObject infoBox;
    [SerializeField] GameObject optStatus;
    [SerializeField] GameObject optEquipment;
    [SerializeField] GameObject optSkills;
    [SerializeField] GameObject optFormation;

    // Inventory submenu
    [SerializeField] GameObject inventoryFrame;

    // Guidebook submenu
    [SerializeField] GameObject guidebookFrame;

    // Audio
    [SerializeField] AudioMachine audioMachine;
    [SerializeField] AudioClip openClip;
    [SerializeField] AudioClip closeClip;

    void Start()
    {
        cooldown = cooldownMax;
    }

    void Update()
    {
        CheckMenu();
    }

    public void SetTask(string task)
    {
        this.task = task;
    }

    public void CheckMenu()
    {
        // Open menu
        if (dataHUB.player.canPause && cooldown == cooldownMax && !menuBox.activeSelf && dataHUB.player.CompareKeyOnce(dataHUB.player.menuKey, true))
        {
            audioMachine.PlaySFX(openClip);
            partyController.CloseAll();
            inventoryController.CloseAll();
            menuBox.SetActive(true);
            dataHUB.player.SetActive(false);
            dataHUB.world.SetPaused(true);
            dataHUB.camera.SetActive(false);
            cooldown = 0;
        }

        // Close menu
        if (cooldown == cooldownMax && menuBox.activeSelf && dataHUB.player.CompareKeyOnce(dataHUB.player.menuKey, true))
        {
            audioMachine.PlaySFX(closeClip);
            CloseAll();
            helper.Dismiss();
            menuBox.SetActive(false);
            dataHUB.player.SetActive(true);
            dataHUB.world.SetPaused(false);
            dataHUB.camera.SetActive(true);
            cooldown = 0;
        }

        if (cooldown < cooldownMax)
        {
            cooldown++;
        }
    }

    public void PromptMode(bool state)
    {
        optParty.GetComponent<Button>().interactable = !state;
        optInventory.GetComponent<Button>().interactable = !state;
        optSettings.GetComponent<Button>().interactable = !state;
        optExit.GetComponent<Button>().interactable = !state;
        optStatus.GetComponent<Button>().interactable = !state;
        optEquipment.GetComponent<Button>().interactable = !state;
        optSkills.GetComponent<Button>().interactable = !state;
        optFormation.GetComponent<Button>().interactable = !state;
    }

    public void ReceiveAnswer(bool answer)
    {
        PromptMode(false);
        if (answer)
        {
            switch (task)
            {
                case "exit":
                    OptExit();
                    break;
            }
        }
    }

    public void OptParty()
    {
        CloseAll();
        partyFrame.SetActive(true);
    }

    public void OptInventory()
    {
        CloseAll();
        inventoryFrame.SetActive(true);
    }

    public void OptGuidebook()
    {
        CloseAll();
        guidebookFrame.SetActive(true);
    }

    private void OptExit()
    {
        dataHUB.world.ExitToTitle();
    }

    // Executed each time the menu changes its view or is closed
    private void CloseAll()
    {
        partyController.CloseAll();
        partyFrame.SetActive(false);
        inventoryController.CloseAll();
        inventoryFrame.SetActive(false);
        guidebookController.CloseAll();
        guidebookFrame.SetActive(false);
    }

}
