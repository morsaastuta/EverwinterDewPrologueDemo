using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    FileDataHandler fileDataHandler;

    // Referenced scripts
    [SerializeField] PlayerProperties playerProperties;
    [SerializeField] CameraProperties cameraProperties;
    [SerializeField] MapProperties mapProperties;

    public static DataPersistenceManager instance
    {
        get; private set;
    }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one DPM in the scene");
        }
        instance = this;

        LoadGame(0);

        playerProperties.Initialize();
    }

    public void SaveGame(int slot)
    {

        // Save current position, rotation, scene...
        playerProperties.SaveStatus();
        cameraProperties.SaveStatus();

        // Instantiate data scripts
        SlotData slotData = new(playerProperties, cameraProperties, mapProperties);

        // Save scripts as files
        fileDataHandler = new(slot);
        fileDataHandler.SaveSlot(slotData);
    }

    public void LoadGame(int slot)
    {
        // Get scripts from files
        fileDataHandler = new(slot);
        SlotData savfile = fileDataHandler.LoadSlot();

        // Load data from file
        List<MonoBehaviour> loadedProperties = savfile.LoadData();
        playerProperties.Reload((PlayerProperties)loadedProperties[0]);
        cameraProperties.Reload((CameraProperties)loadedProperties[1]);
        mapProperties.Reload((MapProperties)loadedProperties[2]);

        if (!playerProperties.sceneName.Equals(savfile.sceneName) && slot != 0) SceneManager.LoadSceneAsync(savfile.sceneName);
    }

    public void DeleteGame(int slot)
    {
        fileDataHandler = new(slot);
        fileDataHandler.DeleteSlot();
    }

    public List<int> CheckExistingFiles()
    {
        return new FileDataHandler().Check();
    }
}