using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
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

    // Main stats (integer)
    public int statHP = 100;
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
    public float statACC = 100;
    public float statCR = 5;
    public float statCD = 50;

    // Affinity stats (percentage)
    public float statPSA = 100;
    public float statPRA = 100;
    public float statASA = 100;
    public float statARA = 100;
    public float statFSA = 100;
    public float statFRA = 100;
    public float statHSA = 100;
    public float statHRA = 100;

    // Equipment
    public HeadItem head;
    public BodyItem body;
    public ArmsItem arms;
    public LegsItem legs;
    public WieldItem wield1;
    public WieldItem wield2;
    public AccessoryItem accessory1;
    public AccessoryItem accessory2;

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
    }
}
