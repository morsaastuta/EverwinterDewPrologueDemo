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
        facePath = "Sprites/NPCs/Foes/Rimebear/facesheet";
        spritesheetOWPath = "Sprites/NPCs/Foes/Rimebear/OW_spritesheet";
        spritesheetCSPath = "Sprites/NPCs/Foes/Rimebear/CS_spritesheet";
        animatorOWPath = "Animations/NPCs/Foes/Rimebear/OW_controller";
        animatorCSPath = "Animations/NPCs/Foes/Rimebear/CS_controller";

        level = initLV;

        baseAP = 2;
        baseHP = 100;
        baseMP = 6;
        baseATK = 2;
        baseDFN = 4;
        baseMAG = 4;
        baseDFL = 5;
        baseSPI = 5;
        baseSPD = 3;
        baseMOV = 2;
        basePSA = 115;
        basePRA = 115;

        incrHP = 8.0f;
        incrMP = 0.8f;
        incrATK = 0.4f;
        incrDFN = 0.7f;
        incrMAG = 0.8f;
        incrDFL = 0.7f;
        incrSPI = 1.2f;
        incrSPD = 0.3f;

        LoadStats();
        FullRestore();

        AddSkill("basic", new Skill_BasicAttack());
        AddSkill("heal", new Skill_TreatWounds());
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

        // Get Heal target (INFINITE LOOP)
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
                        Debug.Log(healTarget.combatant.name);
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

                scene.EndTurn();
            }
            else if (target is not null) BasicAttack(scene, target);
        }
        else scene.EndTurn();
    }
}
