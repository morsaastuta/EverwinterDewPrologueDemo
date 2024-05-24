using System;

[Serializable]
public class Skill_TreatWounds : PhysicalSkill
{
    public Skill_TreatWounds()
    {
        name = "Treat wounds";
        description = "Heal an adjacent companion a little.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 7;

        friendly = true;
        costAP = 1;
        range = 1;

        wields.AddRange(AnyWield());
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);
        
        target.combatant.ChangeHP((
            // Healing
            Formulate(user.combatant.statSPI * 1.5f, 0)
            ));

        return true;
    }
}
