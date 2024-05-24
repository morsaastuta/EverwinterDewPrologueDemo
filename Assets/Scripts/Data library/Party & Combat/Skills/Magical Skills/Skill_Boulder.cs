using System;
using UnityEngine;

[Serializable]
public class Skill_Boulder : MagicalSkill
{
    public Skill_Boulder()
    {
        name = "Boulder";
        description = "The user channels Homa from the ground to create a stone boulder and launch it towards a foe.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 5;

        elements.Add("homa");

        costMP = 5;
        range = 3;
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-(
                AttuneDamage(
                    // Magical damage
                    FormulateCrit(user.combatant.statMAG * 2.0f, target.combatant.statDFL * 0.8f, user.combatant.statCR, user.combatant.statCD),
                    // Homa attunement
                    user.combatant.statHSA, target.combatant.statHRA)
                ));

            return true;
        }
        else return false;
    }
}
