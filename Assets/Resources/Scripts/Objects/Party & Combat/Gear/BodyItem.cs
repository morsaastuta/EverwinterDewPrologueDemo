using System;

[Serializable]
public abstract class BodyItem : GearItem
{
    protected void GenStatList()
    {
        statByName.Add("DFN", statDFN);
        statByName.Add("DFL", statDFL);
        statByName.Add("MOV", statMOV);
        statByName.Add("PRA", statPRA);
        statByName.Add("ARA", statARA);
        statByName.Add("SRA", statSRA);
        statByName.Add("HRA", statHRA);
        statByName.Add("HP", statHP);
        statByName.Add("MP", statMP);
    }
}