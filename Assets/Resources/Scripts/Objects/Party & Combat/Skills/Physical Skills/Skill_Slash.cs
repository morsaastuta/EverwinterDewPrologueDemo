using System;


[Serializable]
public class Skill_Slash : PhysicalSkill
{
    public Skill_Slash(string s)
    {
        source = s;

        name = "Slash";
        description = "The user slashes an enemy with a sharp weapon.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 1;
        sfxPath = "Audio/SFX/sword3";

        costAP = 1;
        range = 1;

        wields.Add(typeof(SwordItem));
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-
                RollCrit(
                    // Physical damage
                    Formulate(user.combatant.statATK * 2.5f, target.combatant.statDFN * 0.5f),
                    // Critical augment
                    user.combatant.statCR, user.combatant.statCD)
                );

            return true;
        }
        else return false;
    }
}
