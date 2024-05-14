using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public abstract class Skill
{
    [OdinSerialize] public bool friendly;

    [OdinSerialize] public string name = "";
    [OdinSerialize] public string description = "";
    [OdinSerialize] protected string sheetPath = "Sprites/Empty";
    [OdinSerialize] protected int sheetIndex = -1;

    [OdinSerialize] public int costAP = 0;
    [OdinSerialize] public int range = 0;
    [OdinSerialize] public bool directional = false;
    [OdinSerialize] public bool squared = false;
    [OdinSerialize] public bool multitarget = false;

    public Sprite GetIcon()
    {
        if (sheetIndex >= 0) return Resources.LoadAll<Sprite>(sheetPath)[sheetIndex];
        else return Resources.Load<Sprite>(sheetPath);
    }

    protected void SpendPoints(Combatant user)
    {
        user.ChangeAP(-costAP);
    }

    public void RecoverPoints(CellController cell)
    {
        cell.combatant.ChangeAP(costAP);
    }

    protected bool Roll(float rate)
    {
        if (rate >= UnityEngine.Random.value * 100) return true;
        else return false;
    }

    protected int FormulateDamage(int userSTR, int foeRES, float multiplier)
    {
        // Simple DMG = STR^ - RES^
        float value = userSTR * multiplier - foeRES;

        if (value >= 0) return (int)value;
        else return 0;
    }

    protected int FormulateDamage(int userSTR, int foeRES, float multiplier, float userCR, float userCD)
    {
        float value = FormulateDamage(userSTR, foeRES, multiplier);

        if (Roll(userCR))
        {
            // Critical DMG = DMG + DMG * CD
            value += value * userCD;
        }

        if (value >= 0) return (int)value;
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
