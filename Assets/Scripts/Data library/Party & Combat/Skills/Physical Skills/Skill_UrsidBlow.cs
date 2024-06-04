using System;

[Serializable]
public class Skill_UrsidBlow : PhysicalSkill
{
    public Skill_UrsidBlow(string s)
    {
        source = s;

        name = "Ursid Blow";
        description = "";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 8;

        costAP = 1;
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
                    Formulate(user.combatant.statATK * 2f, target.combatant.statDFN * 0.5f),
                    // Critical augment
                    user.combatant.statCR, user.combatant.statCD)
                );
            return true;
        }
        else return false;
    }
}
