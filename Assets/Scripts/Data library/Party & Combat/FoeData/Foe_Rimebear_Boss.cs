using System;

[Serializable]
public class Foe_Rimebear_Boss : FoeData
{
    public Foe_Rimebear_Boss(int initLV)
    {
        name = "Elder Rimebear";
        description = "Thrascias Forest's elder bear, greatly influenced by the flow of Pagos energy over the course of the years.";

        iconPath = "Sprites/NPCs/Foes/Rimebear/iconsheet";
        profilePath = "Sprites/NPCs/Foes/Rimebear/profilesheet";
        facePath = "Sprites/NPCs/Foes/Rimebear/facesheet_boss";
        spritesheetOWPath = "Sprites/NPCs/Foes/Rimebear/OW_boss";
        spritesheetCSPath = "Sprites/NPCs/Foes/Rimebear/CS_boss";
        animatorOWPath = "Animations/NPCs/Foes/Rimebear/OW_boss";
        animatorCSPath = "Animations/NPCs/Foes/Rimebear/CS_boss";

        level = initLV;
        lootXP = 30;

        baseAP = 5;
        baseHP = 300;
        baseMP = 100;
        baseATK = 20;
        baseDFN = 20;
        baseMAG = 20;
        baseDFL = 20;
        baseSPI = 10;
        baseSPD = 8;
        baseMOV = 3;
        basePSA = 200;
        basePRA = 150;

        LoadStats();
        FullRestore();
    }
}
