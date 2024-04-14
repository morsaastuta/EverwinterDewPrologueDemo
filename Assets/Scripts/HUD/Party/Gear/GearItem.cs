using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GearItem()
    {
        stackable = false;
    }
}
