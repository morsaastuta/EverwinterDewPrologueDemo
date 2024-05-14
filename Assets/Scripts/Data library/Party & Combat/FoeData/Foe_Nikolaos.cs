using System;

[Serializable]
public class Foe_Nikolaos : FoeData
{
    public Foe_Nikolaos(int initLV)
    {
        name = "Evil Nikolaos";
        description = "";

        iconPath = "Sprites/Characters/Nikolaos/iconsheet";
        profilePath = "Sprites/Characters/Nikolaos/profilesheet";
        facePath = "Sprites/Characters/Nikolaos/facesheet";
        spritesheetOWPath = "Sprites/Characters/Nikolaos/OW_spritesheet";
        spritesheetCSPath = "Sprites/Characters/Nikolaos/CS_spritesheet";
        animatorOWPath = "Animations/Characters/Nikolaos/OW_controller";
        animatorCSPath = "Animations/Characters/Nikolaos/CS_controller";

        level = initLV;

        baseAP = 2;
        baseHP = 80;
        baseMP = 6;
        baseATK = 5;
        baseDFN = 4;
        baseMAG = 2;
        baseDFL = 3;
        baseSPI = 1;
        baseSPD = 4;
        baseMOV = 3;
        basePSA = 115;
        basePRA = 115;

        LoadStats();
        FullRestore();
    }
}
