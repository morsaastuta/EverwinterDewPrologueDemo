using System;

[Serializable]
public abstract class SwordItem : WieldItem
{
    public SwordItem()
    {
        dual = false;
    }

    protected void GenStatList()
    {
        statByName.Add("ATK", statATK);
        statByName.Add("MAG", statMAG);
        statByName.Add("ACC", statACC);
        statByName.Add("CR", statCR);
        statByName.Add("CD", statCD);
        statByName.Add("PSA", statPSA);
        statByName.Add("ASA", statASA);
        statByName.Add("SSA", statSSA);
        statByName.Add("HSA", statHSA);
        statByName.Add("SPI", statSPI);
        statByName.Add("MP", statMP);
        statByName.Add("SPD", statSPD);
        statByName.Add("MOV", statMOV);
        statByName.Add("DFN", statDFN);
        statByName.Add("DFL", statDFL);
        statByName.Add("PRA", statPRA);
        statByName.Add("ARA", statARA);
        statByName.Add("SRA", statSRA);
        statByName.Add("HRA", statHRA);
        statByName.Add("HP", statHP);
    }
}
