using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Foe_Rimebear_Boss : FoeData
{
    public Foe_Rimebear_Boss(int initLV)
    {
        name = "Elder Rimebear";
        description = "Thrascias Forest's elder rimebear, greatly influenced by the flow of Pagos energy over the course of the years.";

        iconPath = "Sprites/NPCs/Foes/Rimebear/iconsheet";
        profilePath = "Sprites/NPCs/Foes/Rimebear/profilesheet";
        facePath = "Sprites/NPCs/Foes/Rimebear/facesheet_boss";
        spritesheetOWPath = "Sprites/NPCs/Foes/Rimebear/OW_boss";
        spritesheetCSPath = "Sprites/NPCs/Foes/Rimebear/CS_boss";
        animatorOWPath = "Animations/NPCs/Foes/Rimebear/OW_boss";
        animatorCSPath = "Animations/NPCs/Foes/Rimebear/CS_boss";

        level = initLV;
        lootXP = 30;

        baseAP = 5;
        baseHP = 300;
        baseMP = 100;
        baseATK = 25;
        baseDFN = 25;
        baseMAG = 20;
        baseDFL = 20;
        baseSPI = 10;
        baseSPD = 10;
        baseMOV = 3;
        basePSA = 200;
        basePRA = 150;

        LoadStats();
        FullRestore();

        AddSkill("basic", new Skill_BasicAttack(""));
        AddSkill("blow", new Skill_UrsidBlow(""));
        AddSkill("frostbite", new Skill_FrostBite(""));

        lootItems.Add(new ClawRimebear(), 1f);
        lootItems.Add(new PeltRimebear(), 1f);
        lootItems.Add(new Galanthus(), 1f);
    }

    public override IEnumerator AutoTurn(CombatController scene)
    {
        CellController target = null;
        int detectionRange = 99;

        // Get target
        foreach (CellController cell in scene.AllCells())
        {
            if (cell.combatant is not null)
            {
                if (cell.combatant.GetType().BaseType.Equals(typeof(Profile)))
                {
                    if (scene.CalcDistance(scene.ActorCell(), cell) < detectionRange)
                    {
                        detectionRange = scene.CalcDistance(scene.ActorCell(), cell);
                        target = cell;
                    }
                }
            }
        }

        yield return new WaitForSeconds(1f);

        // Act on target
        if (target is not null)
        {
            if (CheckCost(skillID.GetValueOrDefault("frostbite"))) scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("frostbite"), scene, target));
            else if (CheckCost(skillID.GetValueOrDefault("blow")) && UnityEngine.Random.Range(0,3) > 1) scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("blow"), scene, target));
            else scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("basic"), scene, target));
        }
        else scene.EndTurn();
    }
}
