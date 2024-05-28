using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    FileDataHandler fileDataHandler;
    string newGameScene = "ThrasciasForest_Overworld";

    // Referenced scripts
    [SerializeField] PlayerProperties player;
    [SerializeField] CameraProperties camera;
    [SerializeField] WorldProperties world;

    void Awake()
    {
        LoadGame(0);
    }

    public void SaveGame(int slot)
    {
        // Save current position, rotation, scene...
        player.SaveState();
        camera.SaveState();
        world.SaveState();

        // Instantiate data scripts
        SlotData slotData = new(player, camera, world);

        // Save scripts as files
        fileDataHandler = new(slot);
        fileDataHandler.SaveSlot(slotData);
    }

    public void LoadGame(int slot)
    {
        // Get scripts from files
        fileDataHandler = new(slot);
        SlotData savfile = fileDataHandler.LoadSlot();

        // Load data from file and get it ready for the reload
        List<MonoBehaviour> loadedProperties = savfile.LoadData();
        player.Reload((PlayerProperties)loadedProperties[0]);
        camera.Reload((CameraProperties)loadedProperties[1]);
        world.Reload((WorldProperties)loadedProperties[2]);

        // Reload scene
        if (slot != 0)
        {
            SaveGame(0);
            SceneManager.LoadSceneAsync(player.sceneName);
        }
    }

    public void DeleteGame(int slot)
    {
        fileDataHandler = new(slot);
        fileDataHandler.DeleteSlot();
    }

    public void NewGame()
    {
        fileDataHandler = new(0);
        fileDataHandler.DeleteSlot();

        SceneManager.LoadSceneAsync(newGameScene);

        player.Initialize();
    }

    public void ChangeScene(string scene)
    {
        SaveGame(0);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public List<int> CheckExistingFiles()
    {
        return new FileDataHandler().Check();
    }
}