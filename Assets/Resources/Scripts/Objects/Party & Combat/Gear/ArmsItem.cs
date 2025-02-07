using System;

[Serializable]
public abstract class ArmsItem : GearItem
{
    protected void GenStatList()
    {
        statByName.Add("ATK", statATK);
        statByName.Add("MAG", statMAG);
        statByName.Add("SPI", statSPI);
        statByName.Add("DFN", statDFN);
        statByName.Add("DFL", statDFL);
        statByName.Add("ACC", statACC);
        statByName.Add("CR", statCR);
        statByName.Add("CD", statCD);
        statByName.Add("PSA", statPRA);
        statByName.Add("ASA", statARA);
        statByName.Add("SSA", statSRA);
        statByName.Add("HSA", statHRA);
        statByName.Add("PRA", statPRA);
        statByName.Add("ARA", statARA);
        statByName.Add("SRA", statSRA);
        statByName.Add("HRA", statHRA);
        statByName.Add("HP", statHP);
        statByName.Add("MP", statMP);
    }
}
