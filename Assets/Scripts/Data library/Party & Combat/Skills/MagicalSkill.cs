using System.Collections.Generic;
using UnityEngine;

public abstract class MagicalSkill : Skill
{
    public int costMP;

    protected List<string> elements = new List<string>();

    protected void SpendPoints(Combatant combatant)
    {
        combatant.ChangeAP(-costAP);
        combatant.ChangeMP(-costMP);
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
