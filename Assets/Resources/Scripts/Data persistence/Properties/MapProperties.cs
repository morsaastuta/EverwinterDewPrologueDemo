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
    [OdinSerialize] public List<DialogueEventController> persistedEvents = new();

    public void Reload(MapProperties m, bool real)
    {
        persistedEncounters = m.persistedEncounters;
        persistedChests = m.persistedChests;
        persistedEvents = m.persistedEvents;

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
        persistedEvents.Clear();

        foreach (Encounter encounter in GetComponentsInChildren<Encounter>(true)) persistedEncounters.Add(encounter.data);
        foreach (ChestBehaviour chest in GetComponentsInChildren<ChestBehaviour>(true)) persistedChests.Add(chest.data);
        foreach (DialogueEventController dialogueEvent in GetComponentsInChildren<DialogueEventController>(true)) persistedEvents.Add(dialogueEvent);

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
        List<ChestBehaviour> allChests = new();
        allChests.AddRange(GetComponentsInChildren<ChestBehaviour>());
        if (persistedChests.Count <= 0) foreach (ChestBehaviour chest in allChests) persistedChests.Add(chest.data);
        else foreach (ChestBehaviour chest in allChests) chest.data.UpdateData(persistedChests[allChests.IndexOf(chest)]);

        // Set all events
        List<DialogueEventController> allEvents = new();
        allEvents.AddRange(GetComponentsInChildren<DialogueEventController>());
        if (persistedEvents.Count <= 0) foreach (DialogueEventController dialogueEvent in allEvents) persistedEvents.Add(dialogueEvent);
        else foreach (DialogueEventController dialogueEvent in allEvents) dialogueEvent.Seen(persistedEvents[allEvents.IndexOf(dialogueEvent)].seen);

        UpdateInfo(allEncounters, allChests, allEvents);
    }

    public void UpdateInfo(List<Encounter> encounters, List<ChestBehaviour> chests, List<DialogueEventController> events)
    {
        foreach (Encounter encounter in encounters)
        {
            encounter.UpdateState();
        }

        foreach (ChestBehaviour chest in chests)
        {
            chest.UpdateState();
        }

        foreach (DialogueEventController dialogueEvent in events)
        {
            dialogueEvent.UpdateState();
        }
    }
}
