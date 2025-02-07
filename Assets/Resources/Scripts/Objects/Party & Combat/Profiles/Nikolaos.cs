using System;

[Serializable]
public class Nikolaos : Profile
{
    public Nikolaos()
    {
        name = "Nikolaos";
        fullname = "Nikolaos Athanas";
        description = "The crown prince of Pantheos. Of 23 years of age already, he just started his Snowforcing pilgrimage. He was blessed by Khione, the Pagos goddess, in his early years, and as such he can change the flow of the Pagos winds at will. His swordsmanship is still on the works, but he's already an expert in sword dancing.";

        iconPath = "Sprites/Characters/Nikolaos/iconsheet";
        profilePath = "Sprites/Characters/Nikolaos/profilesheet";
        facePath = "Sprites/Characters/Nikolaos/facesheet";
        spritesheetOWPath = "Sprites/Characters/Nikolaos/OW_spritesheet";
        spritesheetCSPath = "Sprites/Characters/Nikolaos/CS_spritesheet";
        animatorOWPath = "Animations/Characters/Nikolaos/OW_controller";
        animatorCSPath = "Animations/Characters/Nikolaos/CS_controller";

        level = 0;
        NewJob(new Knight(0));
        SetJob(typeof(Knight), true);

        baseAP = 2;
        baseHP = 160;
        baseMP = 10;
        baseATK = 5;
        baseDFN = 5;
        baseMAG = 4;
        baseDFL = 5;
        baseSPI = 3;
        baseSPD = 5;
        baseMOV = 3;
        basePSA = 150;
        basePRA = 150;

        incrHP = 6.0f;
        incrMP = 0.8f;
        incrATK = 1.6f;
        incrDFN = 1.6f;
        incrMAG = 0.8f;
        incrDFL = 1.2f;
        incrSPI = 0.7f;
        incrSPD = 0.8f;

        LoadStats();
        FullRestore();

        CheckNewSkills();
        mainJob.CheckNewSkills(this);
    }

    public override void CheckNewSkills()
    {
        string source = "self";
        switch (level)
        {
            case 0:
                NewSkill(new Skill_BasicAttack(source));
                break;
            case 1:
                NewSkill(new Skill_TreatWounds(source));
                break;
            case 4:
                NewSkill(new Skill_Snowball(source));
                break;
        }
    }
}
