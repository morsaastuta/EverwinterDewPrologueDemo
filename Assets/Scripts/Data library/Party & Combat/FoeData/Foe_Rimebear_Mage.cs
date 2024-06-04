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
        facePath = "Sprites/NPCs/Foes/Rimebear/facesheet_mage";
        spritesheetOWPath = "Sprites/NPCs/Foes/Rimebear/OW_spritesheet";
        spritesheetCSPath = "Sprites/NPCs/Foes/Rimebear/CS_mage";
        animatorOWPath = "Animations/NPCs/Foes/Rimebear/OW_controller";
        animatorCSPath = "Animations/NPCs/Foes/Rimebear/CS_mage";

        level = initLV;
        lootXP = 5 + .8f * initLV;

        baseAP = 1;
        baseHP = 60;
        baseMP = 12;
        baseATK = 2;
        baseDFN = 4;
        baseMAG = 6;
        baseDFL = 4;
        baseSPI = 2;
        baseSPD = 3;
        baseMOV = 1;
        basePSA = 150;
        basePRA = 125;

        incrHP = 4.0f;
        incrMP = 1.6f;
        incrATK = 0.7f;
        incrDFN = 0.7f;
        incrMAG = 1.8f;
        incrDFL = 0.9f;
        incrSPI = 0.5f;
        incrSPD = 0.7f;

        LoadStats();
        FullRestore();

        AddSkill("basic", new Skill_BasicAttack(""));
        AddSkill("blow", new Skill_UrsidBlow(""));
        AddSkill("snow", new Skill_Snowball(""));
        AddSkill("stone", new Skill_Boulder(""));

        lootItems.Add(new ClawRimebear(), 0.2f);
        lootItems.Add(new PeltRimebear(), 0.3f);
        lootItems.Add(new FlowerSnowdrop(), 0.1f);
        lootItems.Add(new EtherI(), 0.2f);
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
            if (CheckCost(skillID.GetValueOrDefault("stone")) && scene.CalcDistance(scene.ActorCell(), target) < skillID.GetValueOrDefault("stone").range)
            {
                Skill skill = skillID.GetValueOrDefault("stone");

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

                UseSkill(scene, target, skill);

                yield return new WaitForSeconds(1f);
                scene.EndTurn();
            }
            else if (CheckCost(skillID.GetValueOrDefault("snow")))
            {
                Skill skill = skillID.GetValueOrDefault("snow");

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

                yield return new WaitForSeconds(1f);
                scene.EndTurn();
            }
            else
            {
                if (CheckCost(skillID.GetValueOrDefault("blow")) && UnityEngine.Random.Range(0, 3) > 1) scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("blow"), scene, target));
                else scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("basic"), scene, target));
            }
        }
        else scene.EndTurn();
    }
}
