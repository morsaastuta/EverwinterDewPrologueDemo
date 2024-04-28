using UnityEngine;

public class Skill_Slash : PhysicalSkill
{
    public Skill_Slash()
    {
        name = "Slash";
        description = "The user slashes an enemy with a sharp weapon.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 1;

        costAP = 1;
        range = 1;

        wields.Add(typeof(SwordItem));
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-(
                // Physical damage
                FormulateDamage(user.combatant.statATK, target.combatant.statDFN, 1.2f, user.combatant.statCR, user.combatant.statCD)
                ));

            return true;
        }
        else return false;
    }
}
