using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapProperties : MonoBehaviour
{
    [OdinSerialize] public string overworldScene;
    [OdinSerialize] public List<EncounterData> persistedEncounters = new();
    [OdinSerialize] public List<ChestData> persistedChests = new();

    public void Reload(MapProperties m, bool real)
    {
        persistedEncounters = m.persistedEncounters;
        persistedChests = m.persistedChests;

        if (real) LoadInfo();
    }

    public void Reload(MapProperties m, EncounterData exitedEncounter)
    {
        Reload(m, true);

        foreach (EncounterData encounter in persistedEncounters)
        {
            if (encounter.GetHashCode().Equals(exitedEncounter.GetHashCode()))
            {
                encounter.UpdateData(exitedEncounter);
                break;
            }
        }
    }

    public MapProperties SaveState()
    {
        persistedEncounters.Clear();
        persistedChests.Clear();

        foreach (Encounter encounter in GetComponentsInChildren<Encounter>(true)) persistedEncounters.Add(encounter.data);
        foreach (Chest chest in GetComponentsInChildren<Chest>(true)) persistedChests.Add(chest.data);

        return this;
    }

    void LoadInfo()
    {
        // Set all encounters
        List<Encounter> allEncounters = new();
        allEncounters.AddRange(GetComponentsInChildren<Encounter>());
        if (persistedEncounters.Count <= 0) foreach (Encounter encounter in allEncounters) persistedEncounters.Add(encounter.data);
        else foreach (Encounter encounter in allEncounters) encounter.data.UpdateData(persistedEncounters[allEncounters.IndexOf(encounter)]);

        // Set all chests
        List<Chest> allChests = new();
        allChests.AddRange(GetComponentsInChildren<Chest>());
        if (persistedChests.Count <= 0) foreach (Chest chest in allChests) persistedChests.Add(chest.data);
        else foreach (Chest chest in allChests) chest.data.UpdateData(persistedChests[allChests.IndexOf(chest)]);

        UpdateInfo(allEncounters, allChests);
    }

    public void UpdateInfo(List<Encounter> encounters, List<Chest> chests)
    {
        foreach (Encounter encounter in encounters) encounter.UpdateState();
        foreach (Chest chest in chests) chest.UpdateState();
    }
}
