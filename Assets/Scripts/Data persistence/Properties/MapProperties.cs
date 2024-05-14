using Sirenix.Serialization;
using System;
using UnityEngine;

[Serializable]
public class MapProperties : MonoBehaviour
{
    [OdinSerialize] Encounter activeEncounter;
    [OdinSerialize] public string overworldScene;
    [OdinSerialize] public bool pausedGame;

    public void Reload(MapProperties m)
    {
        activeEncounter = new Encounter(m.activeEncounter.GetFoes());
        overworldScene = m.overworldScene;
        pausedGame = m.pausedGame;
    }

    public void SetPaused(bool state)
    {
        pausedGame = state;
    }

    public void SetCurrentEncounter(Encounter encounter)
    {
        activeEncounter = encounter;
    }

    public Encounter GetCurrentEncounter()
    {
        return activeEncounter;
    }
}
