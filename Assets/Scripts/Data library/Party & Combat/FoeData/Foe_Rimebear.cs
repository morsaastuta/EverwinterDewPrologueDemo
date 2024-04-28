

public class Foe_Rimebear : FoeData
{
    public Foe_Rimebear(int initLV)
    {
        name = "Rimebear";
        description = "A bear, slightly influenced by the flow of Pagos, that commonly inhabitates the southern forests of continental Heimonas.";

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
    }
}
