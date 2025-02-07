using System;

[Serializable]
public abstract class HeadItem : GearItem
{
    protected void GenStatList()
    {
        statByName.Add("DFN", statDFN);
        statByName.Add("DFL", statDFL);
        statByName.Add("SPI", statSPI);
        statByName.Add("MAG", statMAG);
        statByName.Add("ACC", statACC);
        statByName.Add("CR", statCR);
        statByName.Add("CD", statCD);
        statByName.Add("PRA", statPRA);
        statByName.Add("ARA", statARA);
        statByName.Add("SRA", statSRA);
        statByName.Add("HRA", statHRA);
        statByName.Add("HP", statHP);
        statByName.Add("MP", statMP);
    }
}