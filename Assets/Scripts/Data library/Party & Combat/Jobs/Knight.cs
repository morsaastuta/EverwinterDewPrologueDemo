using System.Collections.Generic;
using System;

[Serializable]
public class Knight : Job
{
    public Knight()
    {
        name = "Knight";
        description = "Knights have sworn to protect their allies from any danger. They master the sword and shield, being able to block their foes while attacking at a close range.";
        wield1 = typeof(SwordItem);
        wield2 = typeof(ShieldItem);
    }

    public override List<Skill> SkillsByLevel(int level)
    {
        List<Skill> output = new();
        level++;

        switch (level)
        {
            case 1:
                output.Add(new Skill_Slash());
                break;
            case 5:
                output.Add(new Skill_ShieldOnrush());
                break;
            case 6:
                output.Add(new Skill_SpinCutter());
                break;
            case 7:
                output.Add(new Skill_IcyEdge());
                break;
        }

        return output;
    }
}
