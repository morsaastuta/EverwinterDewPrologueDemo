using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Foe_Rimebear_Healer : FoeData
{
    public Foe_Rimebear_Healer(int initLV)
    {
        name = "Rimebear (Healer)";
        description = "A bear, slightly influenced by the flow of Pagos, that commonly inhabitates the southern forests of continental Heimonas.";

        iconPath = "Sprites/NPCs/Foes/Rimebear/iconsheet";
        profilePath = "Sprites/NPCs/Foes/Rimebear/profilesheet";
        facePath = "Sprites/NPCs/Foes/Rimebear/facesheet_healer";
        spritesheetOWPath = "Sprites/NPCs/Foes/Rimebear/OW_spritesheet";
        spritesheetCSPath = "Sprites/NPCs/Foes/Rimebear/CS_healer";
        animatorOWPath = "Animations/NPCs/Foes/Rimebear/OW_controller";
        animatorCSPath = "Animations/NPCs/Foes/Rimebear/CS_healer";

        level = initLV;
        lootXP = 4 + .8f * initLV;

        baseAP = 2;
        baseHP = 100;
        baseMP = 6;
        baseATK = 3;
        baseDFN = 4;
        baseMAG = 4;
        baseDFL = 5;
        baseSPI = 8;
        baseSPD = 4;
        baseMOV = 2;
        basePSA = 125;
        basePRA = 150;

        incrHP = 8.0f;
        incrMP = 0.8f;
        incrATK = 0.9f;
        incrDFN = 0.8f;
        incrMAG = 1.3f;
        incrDFL = 0.8f;
        incrSPI = 1.6f;
        incrSPD = 0.8f;

        LoadStats();
        FullRestore();

        AddSkill("basic", new Skill_BasicAttack(""));
        AddSkill("heal", new Skill_TreatWounds(""));
        AddSkill("blow", new Skill_UrsidBlow(""));

        lootItems.Add(new ClawRimebear(), 0.3f);
        lootItems.Add(new PeltRimebear(), 0.6f);
        lootItems.Add(new HerbsThrascias(), 0.6f);
        lootItems.Add(new FlowerSnowdrop(), 0.1f);
        lootItems.Add(new RationI(), 0.2f);
    }

    public override IEnumerator AutoTurn(CombatController scene)
    {
        CellController target = null;
        CellController healTarget = null;
        int detectionRange = 99;
        float lowestHP = 1;

        // Get Profile target
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

        // Get Heal target
        foreach (CellController cell in scene.AllCells())
        {
            if (cell.combatant is not null)
            {
                if (cell.combatant.GetType().BaseType.Equals(typeof(FoeData)))
                {
                    if (cell.combatant.currentHP < cell.combatant.statHP && cell.combatant.currentHP / cell.combatant.statHP < lowestHP)
                    {
                        lowestHP = cell.combatant.currentHP / cell.combatant.statHP;
                        healTarget = cell;
                    }
                }
            }
        }

        yield return new WaitForSeconds(1f);

        // Act on target
        if (healTarget is not null || target is not null)
        {
            if (healTarget is not null && CheckCost(skillID.GetValueOrDefault("heal")))
            {
                if (scene.CalcDistance(scene.ActorCell(), healTarget) <= 1)
                {
                    UseSkill(scene, healTarget, skillID.GetValueOrDefault("heal"));
                }
                else
                {
                    int rangeLevel = statMOV;
                    int tarPosX = scene.ActorCell().posX;
                    int tarPosY = scene.ActorCell().posY;

                    while (rangeLevel > 0)
                    {
                        foreach (CellController cell in scene.AllCells())
                        {
                            if (cell.combatant is null && scene.CalcDistance(scene.ActorCell(), cell) == rangeLevel)
                            {
                                if (scene.CalcDistance(cell, healTarget) == 1)
                                {
                                    tarPosX = cell.posX;
                                    tarPosY = cell.posY;
                                    rangeLevel = 0;
                                }
                            }
                        }

                        rangeLevel--;
                    }

                    if (tarPosX == scene.ActorCell().posX && tarPosY == scene.ActorCell().posY)
                    {
                        CellController nearest = scene.ActorCell();

                        foreach (CellController cell in scene.AllCells())
                        {
                            if (cell.combatant is null && scene.CalcDistance(scene.ActorCell(), cell) <= statMOV)
                            {
                                for (int i = 1; i <= statMOV; i++)
                                {
                                    if (scene.CalcDistance(cell, healTarget) == i)
                                    {
                                        nearest = cell;
                                        break;
                                    }
                                }
                            }
                        }

                        tarPosX = nearest.posX;
                        tarPosY = nearest.posY;
                    }

                    // After everything, only if a new position has been decided:
                    if (!(tarPosX == scene.ActorCell().posX && tarPosY == scene.ActorCell().posY))
                    {
                        MoveToCell(scene, tarPosX, tarPosY);

                        yield return new WaitForSeconds(1f);

                        if (scene.CalcDistance(scene.ActorCell(), healTarget) == 1)
                        {
                            UseSkill(scene, healTarget, skillID.GetValueOrDefault("heal"));
                        }
                    }
                }

                yield return new WaitForSeconds(1f);
                scene.EndTurn();
            }
            else if (target is not null)
            {
                if (CheckCost(skillID.GetValueOrDefault("blow")) && UnityEngine.Random.Range(0, 3) > 1) scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("blow"), scene, target));
                else scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("basic"), scene, target));
            }
            else scene.EndTurn();
        }
        else scene.EndTurn();
    }
}
