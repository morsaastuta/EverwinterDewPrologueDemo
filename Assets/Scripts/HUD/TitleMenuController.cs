using UnityEngine;
using UnityEngine.UI;

public class TitleMenuController : MonoBehaviour
{
    [SerializeField] string startScene = "RimeForest";

    string task;

    [SerializeField] GameObject optNew;
    [SerializeField] GameObject optSlots;
    [SerializeField] GameObject optExit;

    [SerializeField] SavingSystem savingSystem;
    [SerializeField] DataPersistenceManager dpm;

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
        optExit.GetComponent<Button>().interactable = !state;
    }

    public void ReceiveAnswer(bool answer)
    {
        Working(false);
        if (answer)
        {
            switch (task)
            {
                case "newgame":
                    OptNew();
                    break;
                case "exit":
                    OptExit();
                    break;
            }
        }
    }

    public void OptNew()
    {
        dpm.NewGame();
    }

    public void OptSlots()
    {
        Working(true);
        savingSystem.TitleLoad();
    }

    public void OptExit()
    {
        Application.Quit();
    }
}
