using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    // Subsequent controllers
    [SerializeField] PartyController partyController;
    [SerializeField] InventoryController inventoryController;

    // External properties
    [SerializeField] PlayerProperties properties;
    [SerializeField] MapProperties mapProperties;
    [SerializeField] CameraProperties camProperties;

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
    [SerializeField] GameObject infoBox;
    [SerializeField] GameObject optStatus;
    [SerializeField] GameObject optEquipment;
    [SerializeField] GameObject optSkills;
    [SerializeField] GameObject optFormation;

    // Inventory submenu
    [SerializeField] GameObject partyFrame;
    [SerializeField] GameObject inventoryFrame;

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
        if (properties.canPause && cooldown == cooldownMax && !menuBox.activeSelf && properties.CompareKeyOnce(properties.menuKey, true))
        {
            partyController.CloseAll();
            inventoryController.CloseAll();
            menuBox.SetActive(true);
            properties.SetActive(false);
            mapProperties.SetPaused(true);
            camProperties.SetActive(false);
            cooldown = 0;
        }

        // Close menu
        if (cooldown == cooldownMax && menuBox.activeSelf && properties.CompareKeyOnce(properties.menuKey, true))
        {
            CloseAll();
            menuBox.SetActive(false);
            properties.SetActive(true);
            mapProperties.SetPaused(false);
            camProperties.SetActive(true);
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

    private void OptExit()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }

    // Executed each time the menu changes its view or is closed
    private void CloseAll()
    {
        partyFrame.SetActive(false);
        inventoryFrame.SetActive(false);
    }

}
