using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Skill_Snowball : MagicalSkill
{
    public Skill_Snowball()
    {
        name = "Snowball";
        description = "The user channels Pagos from around to create a snowball and throw it towards an enemy.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 4;

        elements.Add("pagos");

        costMP = 2;
        range = 6;
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-(
                AttuneDamage(
                    // Magical damage
                    FormulateDamage(user.combatant.statMAG, target.combatant.statDFL, 1.2f, user.combatant.statCR, user.combatant.statCD),
                    // Pagos attunement
                    user.combatant.statPSA, target.combatant.statPRA)
                ));

            return true;
        }
        else return false;
    }
}
