using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Combatant
{
    // General info
    public string name;
    public string description;
    public int level;
    public bool KO;

    // Point meters
    public int currentHP;
    public int currentMP;
    public int currentAP;
    public int currentFAT;

    // Visuals
    public string iconPath;
    public string profilePath;
    public string facePath;
    public string spriteSheetPath;
    public string animatorOWPath;
    public string animatorCSPath;

    // Skills
    public List<Skill> skillset = new();

    // Base stats
    protected int baseAP = 0;
    protected int baseFAT = 1000;
    protected int baseHP = 0;
    protected int baseMP = 0;
    protected int baseATK = 0;
    protected int baseDFN = 0;
    protected int baseMAG = 0;
    protected int baseDFL = 0;
    protected int baseSPI = 0;
    protected int baseSPD = 0;
    protected int baseMOV = 0;
    protected float baseACC = 100;
    protected float baseCR = 5;
    protected float baseCD = 50;
    protected float basePSA = 100;
    protected float basePRA = 100;
    protected float baseASA = 100;
    protected float baseARA = 100;
    protected float baseSSA = 100;
    protected float baseSRA = 100;
    protected float baseHSA = 100;
    protected float baseHRA = 100;

    // Actual stats
    public int statAP;
    public int statFAT;
    public int statHP;
    public int statMP;
    public int statATK;
    public int statDFN;
    public int statMAG;
    public int statDFL;
    public int statSPI;
    public int statSPD;
    public int statMOV;
    public float statACC;
    public float statCR;
    public float statCD;
    public float statPSA;
    public float statPRA;
    public float statASA;
    public float statARA;
    public float statSSA;
    public float statSRA;
    public float statHSA;
    public float statHRA;

    public Combatant()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        statAP = baseAP;
        statHP = baseHP;
        statMP = baseMP;

        statATK = baseATK;
        statDFN = baseDFN;
        statMAG = baseMAG;
        statDFL = baseDFL;
        statSPI = baseSPI;

        statACC = baseACC;
        statCR = baseCR;
        statCD = baseCD;

        statSPD = baseSPD;
        statMOV = baseMOV;
        statFAT = baseFAT;

        statPSA = basePSA;
        statPRA = basePRA;
        statASA = baseASA;
        statARA = baseARA;
        statSSA = baseSSA;
        statSRA = baseSRA;
        statHSA = baseHSA;
        statHRA = baseHRA;
    }

    public void ChangeHP(int quantity)
    {
        if (!KO)
        {
            if (currentHP + quantity > statHP) currentHP = statHP;
            else if (currentHP + quantity < 0)
            {
                currentHP = 0;
                KnockOut();
            }
        }
    }

    public void ChangeMP(int quantity)
    {
        if (currentMP + quantity > statMP) currentMP = statMP;
        else if (currentMP + quantity < 0) currentMP = 0;
    }

    public void ChangeAP(int quantity)
    {
        if (currentAP + quantity > statAP) currentAP = statAP;
        else if (currentAP + quantity < 0) currentAP = 0;
    }

    public void KnockOut()
    {
        KO = true;
    }

    public void Revive(int healing)
    {
        KO = false;
        ChangeHP(healing);
    }

    public Sprite GetIcon(int i)
    {
        return Resources.LoadAll<Sprite>(iconPath)[i];
    }

    public Sprite GetProfile(int i)
    {
        return Resources.LoadAll<Sprite>(profilePath)[i];
    }

    public Sprite GetFace(int i)
    {
        return Resources.LoadAll<Sprite>(profilePath)[i];
    }

    public Sprite GetSpriteSheet()
    {
        return Resources.LoadAll<Sprite>(spriteSheetPath)[0];
    }

    public Animator GetAnimatorOW()
    {
        return Resources.Load<Animator>(animatorOWPath);
    }

    public Animator GetAnimatorCS()
    {
        return Resources.Load<Animator>(animatorCSPath);
    }
}
