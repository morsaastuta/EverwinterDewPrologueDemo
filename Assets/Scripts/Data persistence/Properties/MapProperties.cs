using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapProperties : MonoBehaviour
{
    [OdinSerialize] public string overworldScene;
    [OdinSerialize] public List<EncounterData> persistedEncounters = new();

    public void Reload(MapProperties m)
    {
        persistedEncounters = m.persistedEncounters;
    }

    public void Reload(MapProperties m, EncounterData exitedEncounter)
    {
        persistedEncounters = m.persistedEncounters;
        foreach (EncounterData encounter in persistedEncounters)
        {
            if (encounter.GetHashCode().Equals(exitedEncounter.GetHashCode()))
            {
                encounter.UpdateData(exitedEncounter);
                break;
            }
        }
    }

    void Start()
    {
        LoadEncounters();
    }

    void LoadEncounters()
    {
        // Set all encounters
        List<Encounter> allEncounters = new();
        allEncounters.AddRange(GetComponentsInChildren<Encounter>());

        if (persistedEncounters.Count <= 0)
        {
            foreach (Encounter encounter in allEncounters) persistedEncounters.Add(encounter.data);
        }
        else
        {
            foreach(Encounter encounter in allEncounters)
            {
                encounter.data = persistedEncounters[allEncounters.IndexOf(encounter)];
                UpdateEncounters(allEncounters);
            }
        }
    }

    public void UpdateEncounters(List<Encounter> encounters)
    {
        foreach (Encounter encounter in encounters) encounter.UpdateState();
    }
}
