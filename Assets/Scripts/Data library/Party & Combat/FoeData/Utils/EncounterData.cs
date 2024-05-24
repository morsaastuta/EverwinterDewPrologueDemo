using Sirenix.Serialization;
using System.Collections.Generic;
using System;

[Serializable]
public class EncounterData
{
    [OdinSerialize] List<FoeData> foes = new();
    [OdinSerialize] public bool defeated = false;
    [OdinSerialize] public bool fled = false;

    public EncounterData(List<FoeData> newFoes)
    {
        foes.Clear();
        foes.AddRange(newFoes);
    }

    public List<FoeData> GetFoes()
    {
        return foes;
    }

    public void UpdateData(EncounterData e)
    {
        foes = e.foes;
        defeated = e.defeated;
        fled = e.fled;
    }
}
