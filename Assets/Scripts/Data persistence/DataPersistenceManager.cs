using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    FileDataHandler fileDataHandler;

    // Referenced scripts
    [SerializeField] PlayerProperties playerProperties;
    [SerializeField] CameraProperties camProperties;
    [SerializeField] MapProperties mapProperties;

    public static DataPersistenceManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one DPM in the scene");
        }
        instance = this;
    }

    public void SaveGame(int slot)
    {

        // Save current position, rotation, scene...
        playerProperties.SaveStatus();
        camProperties.SaveStatus();

        // Instantiate data scripts
        SlotData slotData = new SlotData(playerProperties, camProperties);

        // Save scripts as files
        fileDataHandler = new FileDataHandler(slot);
        fileDataHandler.SaveSlot(slotData);
    }

    public void LoadGame(int slot)
    {
        // Get scripts from files
        fileDataHandler = new FileDataHandler(slot);
        SlotData savfile = fileDataHandler.LoadSlot();

        if (!playerProperties.sceneName.Equals(savfile.sceneName))
        {
            // Load data from file
            savfile.LoadData(playerProperties, camProperties);

            // Set data
            camProperties.LoadStatus();
            playerProperties.LoadStatus();

            SaveGame(0);

            SceneManager.LoadSceneAsync(savfile.sceneName);

        } else
        {

            // Load data from file
            savfile.LoadData(playerProperties, camProperties);

            // Set data
            camProperties.LoadStatus();
            playerProperties.LoadStatus();

            SaveGame(0);

        }
    }

    public void DeleteGame(int slot)
    {
        fileDataHandler = new FileDataHandler(slot);
        fileDataHandler.DeleteSlot();
    }

    public List<int> CheckExistingFiles()
    {
        return new FileDataHandler().Check();
    }
}