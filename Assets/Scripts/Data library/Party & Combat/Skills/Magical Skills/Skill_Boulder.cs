using System;
using UnityEngine;

[Serializable]
public class Skill_Boulder : MagicalSkill
{
    public Skill_Boulder(string s)
    {
        source = s;

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
            target.combatant.ChangeHP(-
                AttuneDamage(
                RollCrit(
                    // Magical damage
                    Formulate(user.combatant.statMAG * 2.0f, target.combatant.statDFL * 0.5f),
                    // Critical augment
                    user.combatant.statCR, user.combatant.statCD),
                    // Homa attunement
                    user.combatant.statHSA, target.combatant.statHRA)
                );

            return true;
        }
        else return false;
    }
}
