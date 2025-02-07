using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[Serializable]
public class WorldProperties : MonoBehaviour
{
    [OdinSerialize] public EncounterData activeEncounter = null;
    [OdinSerialize] public bool pausedGame = false;
    [OdinSerialize] public List<MapProperties> savedMaps = new();
    [OdinSerialize] public string currentScene;

    public void Reload(WorldProperties w)
    {
        if (w.activeEncounter is not null) activeEncounter = w.activeEncounter;

        currentScene = w.currentScene;
        pausedGame = w.pausedGame;
        savedMaps = w.savedMaps;

        if (GetComponent<DataHUB>())
        {
            foreach (MapProperties map in savedMaps)
            {
                if (map.overworldScene.Equals(GetComponent<DataHUB>().map.overworldScene))
                {
                    if (w.activeEncounter is not null) GetComponent<DataHUB>().map.Reload(map, activeEncounter); 
                    else GetComponent<DataHUB>().map.Reload(map, true);
                    break;
                }
            }
        }
    }

    public void SaveState()
    {
        if (GetComponent<DataHUB>())
        {
            foreach (MapProperties map in savedMaps)
            {
                if (map.overworldScene.Equals(GetComponent<DataHUB>().map.overworldScene))
                {
                    map.Reload(GetComponent<DataHUB>().map.SaveState(), false);
                    break;
                }
            }
        }
    }

    void Start()
    {
        if (GetComponent<DataHUB>() is not null)
        {
            activeEncounter = null;
            currentScene = GetComponent<DataHUB>().map.overworldScene;

            bool newMap = true;
            foreach (MapProperties map in savedMaps)
            {
                if (map.overworldScene.Equals(GetComponent<DataHUB>().map.overworldScene))
                {
                    newMap = false;
                    break;
                }
            }
            if (newMap) savedMaps.Add(GetComponent<DataHUB>().map);
        }

        foreach (Encounter encounter in GetComponentsInChildren<Encounter>())
        {
            encounter.ready = true;
        }
    }

    public void SetPaused(bool state)
    {
        pausedGame = state;
    }

    public void SetCurrentEncounter(EncounterData encounter)
    {
        activeEncounter = encounter;
    }

    public void UpdateEncounters(List<Encounter> encounters)
    {
        foreach (Encounter encounter in encounters)
        {
            encounter.UpdateState();
        }
    }

    public void ExitToTitle()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
