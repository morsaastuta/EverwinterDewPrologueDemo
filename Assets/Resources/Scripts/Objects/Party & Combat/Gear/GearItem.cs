using System.Collections.Generic;
using System;
using Sirenix.Serialization;

[Serializable]
public abstract class GearItem : Item
{
    [OdinSerialize] public int price = 0;

    // Main stats (integer)
    [OdinSerialize] public int statHP = 0;
    [OdinSerialize] public int statMP = 0;
    [OdinSerialize] public int statATK = 0;
    [OdinSerialize] public int statDFN = 0;
    [OdinSerialize] public int statMAG = 0;
    [OdinSerialize] public int statDFL = 0;
    [OdinSerialize] public int statSPI = 0;

    // Action stats (integer)
    [OdinSerialize] public int statSPD = 0;
    [OdinSerialize] public int statMOV = 0;

    // RNG stats (percentage)
    [OdinSerialize] public float statACC = 0;
    [OdinSerialize] public float statCR = 0;
    [OdinSerialize] public float statCD = 0;

    // Affinity stats (percentage)
    [OdinSerialize] public float statPSA = 0;
    [OdinSerialize] public float statPRA = 0;
    [OdinSerialize] public float statASA = 0;
    [OdinSerialize] public float statARA = 0;
    [OdinSerialize] public float statSSA = 0;
    [OdinSerialize] public float statSRA = 0;
    [OdinSerialize] public float statHSA = 0;
    [OdinSerialize] public float statHRA = 0;

    [OdinSerialize] public List<string> statNames = new();
    [OdinSerialize] public Dictionary<string,float> statByName = new();

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
        statNames.Add("SSA");
        statNames.Add("HSA");
        statNames.Add("PRA");
        statNames.Add("ARA");
        statNames.Add("SRA");
        statNames.Add("HRA");
    }
}
