using System;

[Serializable]
public class Skill_SpinCutter : PhysicalSkill
{
    public Skill_SpinCutter()
    {
        name = "Spin Cutter";
        description = "The user spins around with its sword unleashed, cutting any foe close enough.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 2;

        costAP = 1;
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
            target.combatant.ChangeHP(-(
                // Physical damage
                FormulateDamage(user.combatant.statATK, target.combatant.statDFN, 1.2f, user.combatant.statCR, user.combatant.statCD)
                ));

            return true;
        }
        else return false;
    }
}
