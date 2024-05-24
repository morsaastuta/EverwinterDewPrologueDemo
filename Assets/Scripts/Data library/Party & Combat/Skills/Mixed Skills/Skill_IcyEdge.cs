using System;

[Serializable]
public class Skill_IcyEdge : MixedSkill
{
    public Skill_IcyEdge()
    {
        name = "Icy Edge";
        description = "The user channels Pagos into its sword's edge and slashes an enemy.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 6;

        elements.Add("pagos");

        costAP = 1;
        costMP = 5;
        range = 1;

        wields.Add(typeof(SwordItem));
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-(
                AttuneDamage(
                    // Physical damage
                    FormulateCrit(user.combatant.statATK * 1.6f, target.combatant.statDFN * 0.8f, user.combatant.statCR, user.combatant.statCD) +
                    // Magical damage
                    Formulate(user.combatant.statMAG * 1.2f, target.combatant.statDFL * 0.8f),
                    // Pagos attunement
                    user.combatant.statPSA, target.combatant.statPRA)
                ));

            return true;
        }
        else return false;
    }
}
