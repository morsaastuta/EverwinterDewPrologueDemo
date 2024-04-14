using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuController : MonoBehaviour
{
    [SerializeField] private string startScene = "RimeForest";

    private string task;

    [SerializeField] private GameObject optNew;
    [SerializeField] private GameObject optSlots;
    [SerializeField] private GameObject optSettings;
    [SerializeField] private GameObject optExit;

    [SerializeField] private SavingSystem savingSystem;
    [SerializeField] private DataPersistenceManager dpm;

    private void Awake()
    {
        dpm.DeleteGame(0);
    }

    public void SetTask(string task)
    {
        this.task = task;
    }

    public void Working(bool state)
    {
        optNew.GetComponent<Button>().interactable = !state;
        optSlots.GetComponent<Button>().interactable = !state;
        optSettings.GetComponent<Button>().interactable = !state;
        optExit.GetComponent<Button>().interactable = !state;
    }

    public void ReceiveAnswer(bool answer)
    {
        Working(false);
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

    public void OptNew()
    {
    }

    public void OptSlots()
    {
        Working(true);
        savingSystem.TitleLoad();
    }

    public void OptSettings()
    {
    }

    public void OptExit()
    {
        Application.Quit();
    }
}
