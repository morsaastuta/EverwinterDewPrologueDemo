using System;
using System.Collections.Generic;

[Serializable]
public abstract class Profile : Combatant
{
    // General info
    public string fullname;
    public Job job;
    public Job subjob;

    // Point meters
    public int currentJP;

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

    public void LoadSkills()
    {
        skillset.Clear();
        skillset.Add(new Skill_BasicAttack());

        for (int i = 0; i < level; i++) skillset.AddRange(job.SkillsByLevel(i));
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

        statPSA = basePSA;
        statPRA = basePRA;
        statASA = baseASA;
        statARA = baseARA;
        statSSA = baseSSA;
        statSRA = baseSRA;
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
                statSSA += gearItem.statSSA;
                statSRA += gearItem.statSRA;
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
            statSSA += currentWield.statSSA;
            statSRA += currentWield.statSRA;
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
