using System;

[Serializable]
public class Skill_BasicAttack : PhysicalSkill
{
    public Skill_BasicAttack(string s)
    {
        source = s;

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
