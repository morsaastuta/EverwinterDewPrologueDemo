using System;

[Serializable]
public class Skill_IcyEdge : MixedSkill
{
    public Skill_IcyEdge(string s)
    {
        source = s;

        name = "Icy Edge";
        description = "The user channels Pagos into its sword's edge and slashes an enemy.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 6;

        elements.Add("pagos");

        costAP = 2;
        costMP = 5;
        range = 1;

        wields.Add(typeof(SwordItem));
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-
                AttuneDamage(
                RollCrit(
                    // Physical damage
                    Formulate(user.combatant.statATK * 2.0f, target.combatant.statDFN * 0.5f) +
                    // Magical damage
                    Formulate(user.combatant.statMAG * 1.0f, target.combatant.statDFL * 0.5f),
                    // Critical augment
                    user.combatant.statCR, user.combatant.statCD),
                    // Pagos attunement
                    user.combatant.statPSA, target.combatant.statPRA)
                );

            return true;
        }
        else return false;
    }
}
