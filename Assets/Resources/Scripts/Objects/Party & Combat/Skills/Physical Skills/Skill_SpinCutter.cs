using System;

[Serializable]
public class Skill_SpinCutter : PhysicalSkill
{
    public Skill_SpinCutter(string s)
    {
        source = s;

        name = "Spin Cutter";
        description = "The user spins around with its sword unleashed, cutting any foe close enough.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 2;
        sfxPath = "Audio/SFX/sword4";

        costAP = 2;
        range = 1;
        squared = true;
        multitarget = true;

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
                    Formulate(user.combatant.statATK * 1.5f, target.combatant.statDFN * 0.5f),
                    // Critical augment
                    user.combatant.statCR, user.combatant.statCD)
                );

            return true;
        }
        else return false;
    }
}
