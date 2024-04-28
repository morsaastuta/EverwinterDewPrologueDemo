using System;
using UnityEngine;

public class Skill_ShieldOnrush : PhysicalSkill
{
    public Skill_ShieldOnrush()
    {
        name = "Shield Onrush";
        description = "The user runs shield in hand along a straight line and hits a foe, sending it in the opposite direction with as much force as the user gathered during the rally.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 3;

        costAP = 1;
        range = 4;
        directional = true;

        wields.Add(typeof(ShieldItem));
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        int rallyMultiplier = 0;
        if (user.posX != target.posX)
        {
            rallyMultiplier = (int)Math.Abs(target.posX - user.posX);
        }
        else if (user.posY != target.posY)
        {
            rallyMultiplier = (int)Math.Abs(target.posY - user.posY);
        }

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-(
                // Physical damage
                FormulateDamage(user.combatant.statATK, target.combatant.statDFN, 1.5f, user.combatant.statCR, user.combatant.statCD) +
                // Bonus rally damage
                FormulateDamage(user.combatant.statDFN, target.combatant.statDFN, .5f * rallyMultiplier)
                ));

            return true;
        }
        else return false;
    }
}
