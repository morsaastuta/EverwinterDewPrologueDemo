using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Foe_Rimebear_Mage : FoeData
{
    public Foe_Rimebear_Mage(int initLV)
    {
        name = "Rimebear (Mage)";
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
        baseHP = 60;
        baseMP = 8;
        baseATK = 1;
        baseDFN = 3;
        baseMAG = 5;
        baseDFL = 4;
        baseSPI = 2;
        baseSPD = 3;
        baseMOV = 1;
        basePSA = 115;
        basePRA = 115;

        incrHP = 4.0f;
        incrMP = 1.0f;
        incrATK = 0.2f;
        incrDFN = 0.6f;
        incrMAG = 1.2f;
        incrDFL = 0.8f;
        incrSPI = 0.5f;
        incrSPD = 0.2f;

        LoadStats();
        FullRestore();

        AddSkill("basic", new Skill_BasicAttack());
        AddSkill("ranged", new Skill_Boulder());
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
            if (CheckCost(skillID.GetValueOrDefault("ranged")))
            {
                Skill skill = skillID.GetValueOrDefault("ranged");

                if (scene.CalcDistance(scene.ActorCell(), target) < skill.range)
                {
                    // Calc most safe distance
                    int disToMove = skill.range - scene.CalcDistance(scene.ActorCell(), target);

                    // Get random in-range cell
                    int addX = 0;
                    int addY = 0;
                    for (int i = 0; i < disToMove; i++)
                    {
                        if (UnityEngine.Random.Range(0, 1) == 0) addX++;
                        else addY++;
                    }

                    // Find if any of the cells with symmetric X and Y obtained exists and is empty
                    bool willMove = true;
                    int finalX = scene.ActorCell().posX;
                    int finalY = scene.ActorCell().posY;
                    if (scene.CheckExistingCell(scene.ActorCell().posX + addX, scene.ActorCell().posY + addY))
                    {
                        if (scene.GetCell(scene.ActorCell().posX + addX, scene.ActorCell().posY + addY).combatant is null)
                        {
                            finalX += addX;
                            finalY += addY;
                        }
                    }
                    else if (scene.CheckExistingCell(scene.ActorCell().posX - addX, scene.ActorCell().posY + addY))
                    {
                        if (scene.GetCell(scene.ActorCell().posX - addX, scene.ActorCell().posY + addY).combatant is null)
                        {
                            finalX -= addX;
                            finalY += addY;
                        }
                    }
                    else if (scene.CheckExistingCell(scene.ActorCell().posX - addX, scene.ActorCell().posY - addY))
                    {
                        if (scene.GetCell(scene.ActorCell().posX - addX, scene.ActorCell().posY - addY).combatant is null)
                        {
                            finalX -= addX;
                            finalY -= addY;
                        }
                    }
                    else if (scene.CheckExistingCell(scene.ActorCell().posX + addX, scene.ActorCell().posY - addY))
                    {
                        if (scene.GetCell(scene.ActorCell().posX + addX, scene.ActorCell().posY - addY).combatant is null)
                        {
                            finalX += addX;
                            finalY -= addY;
                        }
                    }
                    else willMove = false;

                    // Only if a permitted cell has been found
                    if (willMove && scene.GetCell(finalX, finalY).combatant is null)
                    {
                        MoveToCell(scene, finalX, finalY);
                    }
                }
                UseSkill(scene, target, skill);
                scene.EndTurn();
            }
            else scene.StartCoroutine(BasicAttack(scene, target));
        }
        else scene.EndTurn();
    }
}
