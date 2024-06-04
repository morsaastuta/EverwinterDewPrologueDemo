using System;

[Serializable]
public class Knight : Job
{
    public Knight(int initLevel)
    {
        name = "Knight";
        description = "Knights have sworn to protect their allies from any danger. They master the sword and shield, being able to block their foes while attacking at a close range.";
        wield1 = typeof(SwordItem);
        wield2 = typeof(ShieldItem);

        level = initLevel;
    }

    public override void CheckNewSkills(Profile character)
    {
        string source = "Knight";
        switch (level)
        {
            case 1:
                character.NewSkill(new Skill_Slash(source));
                break;
            case 5:
                character.NewSkill(new Skill_ShieldOnrush(source));
                break;
            case 6:
                character.NewSkill(new Skill_SpinCutter(source));
                break;
            case 7:
                character.NewSkill(new Skill_IcyEdge(source));
                break;
        }
    }
}
