using System;
using UnityEngine;

[Serializable]
public abstract class Skill
{
    public bool friendly;

    public string name = "";
    public string description = "";
    protected string sheetPath = "Sprites/Empty";
    protected int sheetIndex = -1;

    public int costAP = 0;
    public int range = 0;
    public bool directional = false;
    public bool squared = false;
    public bool multitarget = false;

    public Sprite GetIcon()
    {
        if (sheetIndex >= 0) return Resources.LoadAll<Sprite>(sheetPath)[sheetIndex];
        else return Resources.Load<Sprite>(sheetPath);
    }

    protected void SpendPoints(Combatant user)
    {
        user.ChangeAP(-costAP);
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
