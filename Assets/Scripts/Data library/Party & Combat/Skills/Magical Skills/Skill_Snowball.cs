using System;

[Serializable]
public class Skill_Snowball : MagicalSkill
{
    public Skill_Snowball(string s)
    {
        source = s;

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
            target.combatant.ChangeHP(-
                AttuneDamage(
                RollCrit(
                    // Magical damage
                    Formulate(user.combatant.statMAG * 1.5f, target.combatant.statDFL * 0.5f),
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
