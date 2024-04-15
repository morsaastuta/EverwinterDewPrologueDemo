using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Profile
{
    // General info
    public string name;
    public string fullname;
    public string description;
    public Job job;
    public Job subjob;
    public int level;

    // Visuals
    public Sprite icon;
    public Sprite profile;
    public Sprite face;
    public Animator animator;

    // Base stats
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
    protected float baseFSA = 100;
    protected float baseFRA = 100;
    protected float baseHSA = 100;
    protected float baseHRA = 100;

    // Actual stats
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
    public float statFSA;
    public float statFRA;
    public float statHSA;
    public float statHRA;

    // Equipment
    public HeadItem head;
    public BodyItem body;
    public ArmsItem arms;
    public LegsItem legs;
    public WieldItem wield1;
    public WieldItem wield2;
    public AccessoryItem accessory1;
    public AccessoryItem accessory2;

    public WieldItem currentWield;

    public List<GearItem> gearItems = new List<GearItem>();
    public List<WieldItem> wieldItems = new List<WieldItem>();

    public Profile()
    {
        LoadStats();
    }

    public Type GetWield(int slot)
    {
        Type wield = null;
        switch (slot)
        {
            case 1:
                wield = job.wield1;
                break;
            case 2:
                wield = job.wield2;
                break;
        }
        return wield;
    }

    public void SaveGear(GearItem headItem, GearItem bodyItem, GearItem armsItem, GearItem legsItem, GearItem wield1Item, GearItem wield2Item, GearItem accessory1Item, GearItem accessory2Item)
    {
        head = (HeadItem)headItem;
        body = (BodyItem)bodyItem;
        arms = (ArmsItem)armsItem;
        legs = (LegsItem)legsItem;
        wield1 = (WieldItem)wield1Item;
        wield2 = (WieldItem)wield2Item;
        accessory1 = (AccessoryItem)accessory1Item;
        accessory2 = (AccessoryItem)accessory2Item;

        LoadStats();
    }

    public void InitializeLists()
    {
        gearItems.Clear();
        gearItems.Add(head);
        gearItems.Add(body);
        gearItems.Add(arms);
        gearItems.Add(legs);
        gearItems.Add(accessory1);
        gearItems.Add(accessory2);

        wieldItems.Clear();
        wieldItems.Add(wield1);
        wieldItems.Add(wield2);
    }

    public void LoadStats()
    {
        InitializeLists();

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

        statPSA = basePSA;
        statPRA = basePRA;
        statASA = baseASA;
        statARA = baseARA;
        statFSA = baseFSA;
        statFRA = baseFRA;
        statHSA = baseHSA;
        statHRA = baseHRA;

        foreach (GearItem gearItem in gearItems)
        {
            if (gearItem != null)
            {
                statHP += gearItem.statHP;
                statMP += gearItem.statMP;

                statATK += gearItem.statATK;
                statDFN += gearItem.statDFN;
                statMAG += gearItem.statMAG;
                statDFL += gearItem.statDFL;
                statSPI += gearItem.statSPI;

                statACC += gearItem.statACC;
                statCR += gearItem.statCR;
                statCD += gearItem.statCD;

                statSPD += gearItem.statSPD;
                statMOV += gearItem.statMOV;

                statPSA += gearItem.statPSA;
                statPRA += gearItem.statPRA;
                statASA += gearItem.statASA;
                statARA += gearItem.statARA;
                statFSA += gearItem.statFSA;
                statFRA += gearItem.statFRA;
                statHSA += gearItem.statHSA;
                statHRA += gearItem.statHRA;
            }
        }

        foreach (GearItem gearItem in wieldItems)
        {
            if (gearItem != null)
            {
                statHP += gearItem.statHP;
                statMP += gearItem.statMP;
            }
        }

        if (currentWield != null)
        {
            statATK += currentWield.statATK;
            statDFN += currentWield.statDFN;
            statMAG += currentWield.statMAG;
            statDFL += currentWield.statDFL;
            statSPI += currentWield.statSPI;

            statACC += currentWield.statACC;
            statCR += currentWield.statCR;
            statCD += currentWield.statCD;

            statSPD += currentWield.statSPD;
            statMOV += currentWield.statMOV;

            statPSA += currentWield.statPSA;
            statPRA += currentWield.statPRA;
            statASA += currentWield.statASA;
            statARA += currentWield.statARA;
            statFSA += currentWield.statFSA;
            statFRA += currentWield.statFRA;
            statHSA += currentWield.statHSA;
            statHRA += currentWield.statHRA;
        }
    }

    public void ChangeWield(int wield)
    {
        switch (wield)
        {
            case 0:
                currentWield = null;
                break;
            case 1:
                currentWield = wield1;
                break;
            case 2:
                currentWield = wield2;
                break;
        }
    }
}
