using System.Collections.Generic;

public abstract class GearItem : Item
{
    public int price;

    // Main stats (integer)
    public int statHP = 0;
    public int statMP = 0;
    public int statATK = 0;
    public int statDFN = 0;
    public int statMAG = 0;
    public int statDFL = 0;
    public int statSPI = 0;

    // Action stats (integer)
    public int statSPD = 0;
    public int statMOV = 0;

    // RNG stats (percentage)
    public float statACC = 0;
    public float statCR = 0;
    public float statCD = 0;

    // Affinity stats (percentage)
    public float statPSA = 0;
    public float statPRA = 0;
    public float statASA = 0;
    public float statARA = 0;
    public float statFSA = 0;
    public float statFRA = 0;
    public float statHSA = 0;
    public float statHRA = 0;

    public List<string> statNames = new List<string>();
    public Dictionary<string,float> statByName = new Dictionary<string, float>();

    public GearItem()
    {
        stackable = false;

        statNames.Add("HP");
        statNames.Add("MP");

        statNames.Add("ATK");
        statNames.Add("DFN");
        statNames.Add("MAG");
        statNames.Add("DFL");
        statNames.Add("SPI");

        statNames.Add("ACC");
        statNames.Add("CR");
        statNames.Add("CD");

        statNames.Add("SPD");
        statNames.Add("MOV");

        statNames.Add("PSA");
        statNames.Add("ASA");
        statNames.Add("FSA");
        statNames.Add("HSA");
        statNames.Add("PRA");
        statNames.Add("ARA");
        statNames.Add("FRA");
        statNames.Add("HRA");
    }
}
