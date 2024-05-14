using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MagicalSkill : Skill
{
    [OdinSerialize] public int costMP;

    [OdinSerialize] protected List<string> elements = new();

    protected void SpendPoints(Combatant user)
    {
        user.ChangeAP(-costAP);
        user.ChangeMP(-costMP);
    }

    public void RecoverPoints(CellController cell)
    {
        cell.combatant.ChangeAP(costAP);
        cell.combatant.ChangeMP(costMP);
    }

    protected int AttuneDamage(int damage, float userSTR, float foeRES)
    {
        // Elemental DMG = DMG * STR / RES
        float value = damage * userSTR / foeRES;

        if (value >= 0) return (int)value;
        else return 0;
    }

    public Sprite GetElement(int index)
    {
        if (index < elements.Count)
        {
            switch (elements[index])
            {
                case "pagos":
                    return Resources.LoadAll<Sprite>("Sprites/HUD/Combat/Skills/skillframe")[2];
                case "aeras":
                    return Resources.Load<Sprite>("Sprites/Empty");
                case "spitha":
                    return Resources.Load<Sprite>("Sprites/Empty");
                case "homa":
                    return Resources.LoadAll<Sprite>("Sprites/HUD/Combat/Skills/skillframe")[3];
                default:
                    return Resources.Load<Sprite>("Sprites/Empty");
            }
        }
        else return Resources.Load<Sprite>("Sprites/Empty");
    }
}
