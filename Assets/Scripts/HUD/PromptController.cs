using System;
using TMPro;
using UnityEngine;

public class PromptController : MonoBehaviour
{
    // Internal properties
    string menu;

    // UI elements
    [SerializeField] private GameObject pane;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject optYes;
    [SerializeField] private GameObject optNo;

    // Inherited controllers
    [SerializeField] private TitleMenuController titleMenu;
    [SerializeField] private SavingSystem saveMenu;
    [SerializeField] private PauseMenuController pauseMenu;

    public void InitPrompt(string question, string menu)
    {
        SetMenu(menu);
        Prompt(question);
    }

    public void Answer(bool answer)
    {
        // Hide prompt box 
        pane.SetActive(false);

        // Send info
        switch (menu)
        {
            case "title":
                titleMenu.ReceiveAnswer(answer);
                break;
            case "save":
                saveMenu.ReceiveAnswer(answer);
                break;
            case "pause":
                pauseMenu.ReceiveAnswer(answer);
                break;
        }
    }

    public void SetMenu(string menu)
    {
        this.menu = menu;
    }

    public void Prompt(string question)
    {
        pane.SetActive(true);
        text.SetText(question);
    }
}
