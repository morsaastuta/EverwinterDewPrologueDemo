using Sirenix.Serialization;
using System;
using UnityEngine;

[Serializable]
public abstract class Skill
{
    [OdinSerialize] public bool friendly = false;

    [OdinSerialize] public string name = "";
    [OdinSerialize] public string description = "";
    [OdinSerialize] protected string sheetPath = "Sprites/Empty";
    [OdinSerialize] protected int sheetIndex = -1;

    [OdinSerialize] public int costAP = 0;
    [OdinSerialize] public int range = 0;
    [OdinSerialize] public bool directional = false;
    [OdinSerialize] public bool squared = false;
    [OdinSerialize] public bool multitarget = false;

    [OdinSerialize] public string source = "";

    [OdinSerialize] protected string sfxPath = "";

    public Sprite GetIcon()
    {
        if (sheetIndex >= 0) return Resources.LoadAll<Sprite>(sheetPath)[sheetIndex];
        else return Resources.Load<Sprite>(sheetPath);
    }

    public AudioClip GetSFX()
    {
        return Resources.Load<AudioClip>(sfxPath);
    }

    protected virtual void SpendPoints(Combatant user)
    {
        user.ChangeAP(-costAP);
    }

    public virtual void RecoverPoints(CellController cell)
    {
        cell.combatant.ChangeAP(costAP);
    }

    protected bool Roll(float rate)
    {
        if (rate >= UnityEngine.Random.value * 100) return true;
        else return false;
    }

    protected int Formulate(float power, float resistance)
    {
        // Simple DMG = POWER - RESISTANCE
        float value = power - resistance;

        if (value >= 0) return (int)value;
        else return 0;
    }

    protected int RollCrit(float damage, float rate, float multiplier)
    {
        if (Roll(rate))
        {
            // Critical DMG = DMG + DMG * CD
            damage += damage * (multiplier / 100);
        }

        if (damage >= 0) return (int)damage;
        else return 0;
    }

    public virtual bool Cast(CellController user, CellController target)
    {
        return false;
    }

    public virtual void Cast(Combatant user)
    {
    }
}
