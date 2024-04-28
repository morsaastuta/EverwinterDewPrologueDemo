using System;

[Serializable]
public abstract class AccessoryItem : GearItem
{
    protected void GenStatList()
    {
        statByName.Add("HP", statHP);
        statByName.Add("MP", statMP);

        statByName.Add("ATK", statHP);
        statByName.Add("MAG", statMP);
        statByName.Add("SPI", statMP);

        statByName.Add("DFN", statDFN);
        statByName.Add("DFL", statDFL);

        statByName.Add("SPD", statMOV);
        statByName.Add("MOV", statMOV);

        statByName.Add("CR", statMOV);
        statByName.Add("CD", statMOV);

        statByName.Add("PSA", statPRA);
        statByName.Add("ASA", statARA);
        statByName.Add("SSA", statSRA);
        statByName.Add("HSA", statHRA);
        statByName.Add("PRA", statPRA);
        statByName.Add("ARA", statARA);
        statByName.Add("SRA", statSRA);
        statByName.Add("HRA", statHRA);
    }
}
