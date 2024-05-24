using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class WorldProperties : MonoBehaviour
{
    [OdinSerialize] public EncounterData activeEncounter;
    [OdinSerialize] public bool pausedGame = false;
    [OdinSerialize] public List<MapProperties> savedMaps = new();
    [OdinSerialize] public string currentScene;

    public void Reload(WorldProperties w)
    {
        if (w.activeEncounter is not null) activeEncounter = w.activeEncounter;

        currentScene = w.currentScene;
        pausedGame = w.pausedGame;
        savedMaps = w.savedMaps;

        foreach (MapProperties map in savedMaps)
        {
            if (GetComponent<DataHUB>())
            {
                if (map.overworldScene.Equals(GetComponent<DataHUB>().map.overworldScene))
                {
                    if (activeEncounter is not null) GetComponent<DataHUB>().map.Reload(map, activeEncounter);
                    else GetComponent<DataHUB>().map.Reload(map);
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
            if (!savedMaps.Contains(GetComponent<DataHUB>().map)) savedMaps.Add(GetComponent<DataHUB>().map);
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
