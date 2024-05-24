using System;

[Serializable]
public class Skill_BasicAttack : PhysicalSkill
{
    public Skill_BasicAttack()
    {
        name = "Attack";
        description = "Basic physical attack.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 0;

        range = 1;

        wields.AddRange(AnyWield());
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-(
                // Physical damage
                FormulateCrit(user.combatant.statATK * 1.2f, target.combatant.statDFN * 0.8f, user.combatant.statCR, user.combatant.statCD)
                ));

            return true;
        }
        else return false;
    }
}
