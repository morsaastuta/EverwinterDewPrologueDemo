

public abstract class ShieldItem : WieldItem
{
    public ShieldItem()
    {
        dual = false;
    }

    protected void GenStatList()
    {
        statByName.Add("ATK", statATK);
        statByName.Add("MAG", statMAG);
        statByName.Add("DFN", statDFN);
        statByName.Add("DFL", statDFL);
        statByName.Add("ACC", statACC);
        statByName.Add("CR", statCR);
        statByName.Add("CD", statCD);
        statByName.Add("PSA", statPSA);
        statByName.Add("ASA", statASA);
        statByName.Add("FSA", statFSA);
        statByName.Add("HSA", statHSA);
        statByName.Add("SPI", statSPI);
        statByName.Add("MP", statMP);
        statByName.Add("SPD", statSPD);
        statByName.Add("MOV", statMOV);
        statByName.Add("PRA", statPRA);
        statByName.Add("ARA", statARA);
        statByName.Add("FRA", statFRA);
        statByName.Add("HRA", statHRA);
        statByName.Add("HP", statHP);
    }
}