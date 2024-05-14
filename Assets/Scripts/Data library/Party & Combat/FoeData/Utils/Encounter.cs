using Sirenix.Serialization;
using System.Collections.Generic;
using System;

[Serializable]
public class Encounter
{
    [OdinSerialize] List<FoeData> foes = new();

    public Encounter(List<FoeData> newFoes)
    {
        foes.Clear();
        foes.AddRange(newFoes);
    }

    public List<FoeData> GetFoes()
    {
        return foes;
    }
}
