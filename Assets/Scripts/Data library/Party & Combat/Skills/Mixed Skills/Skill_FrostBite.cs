using System;

[Serializable]
public class Skill_FrostBite : MixedSkill
{
    public Skill_FrostBite(string s)
    {
        source = s;

        name = "Frost Bite";
        description = "";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 9;
        sfxPath = "Audio/SFX/bite1";

        elements.Add("pagos");

        costAP = 3;
        costMP = 8;
        range = 1;

        wields.AddRange(AnyWield());
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
                    Formulate(user.combatant.statATK * 2.5f, target.combatant.statDFN * 0.5f) +
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
