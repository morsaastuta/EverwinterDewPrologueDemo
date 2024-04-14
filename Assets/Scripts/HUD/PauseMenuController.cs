using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    // External properties
    [SerializeField] private PlayerProperties properties;
    [SerializeField] private MapProperties mapProperties;
    [SerializeField] private CameraProperties camProperties;

    // Internal properties
    private int cooldownMax = 10;
    private int cooldown;
    private string task;

    // Prompt controller
    [SerializeField] private PromptController promptController;

    // Main menu
    [SerializeField] private GameObject menuBox;
    [SerializeField] private GameObject optParty;
    [SerializeField] private GameObject optInventory;
    [SerializeField] private GameObject optSettings;
    [SerializeField] private GameObject optExit;

    // Party submenu
    [SerializeField] private GameObject infoBox;
    [SerializeField] private GameObject optStatus;
    [SerializeField] private GameObject optEquipment;
    [SerializeField] private GameObject optSkills;
    [SerializeField] private GameObject optFormation;

    // Inventory submenu
    [SerializeField] private GameObject partyFrame;
    [SerializeField] private GameObject inventoryFrame;

    private void Start()
    {
        cooldown = cooldownMax;
    }

    private void Update()
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
