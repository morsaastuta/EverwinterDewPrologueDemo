using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class FoeData : Combatant
{
    [OdinSerialize] protected Dictionary<string, Skill> skillID = new();
    [OdinSerialize] protected int lootXP;
    [OdinSerialize] protected Dictionary<Item, float> lootItems = new();

    public virtual IEnumerator AutoTurn(CombatController scene)
    {
        yield return new WaitForSeconds(0.1f);
    }

    public virtual bool CheckCost(Skill skill)
    {
        if (currentAP < skill.costAP) return false;

        if (skill.GetType().BaseType.Equals(typeof(MagicalSkill)) || skill.GetType().BaseType.Equals(typeof(MixedSkill)))
        {
            if (currentMP < ((MagicalSkill)skill).costMP) return false;
        }

        return true;
    }

    public void AddSkill(string id, Skill skill)
    {
        skillID.Add(id, skill);
        skillset.Add(skill);
    }

    protected IEnumerator BasicAttack(CombatController scene, CellController target)
    {
        if (scene.CalcDistance(scene.ActorCell(), target) == 1)
        {
            UseSkill(scene, target, skillID.GetValueOrDefault("basic"));
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
                        if (scene.CalcDistance(cell, target) == 1)
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
                            if (scene.CalcDistance(cell, target) == i)
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

                if (scene.CalcDistance(scene.ActorCell(), target) == 1)
                {
                    UseSkill(scene, target, skillID.GetValueOrDefault("basic"));
                }
            }
        }

        scene.EndTurn();
    }

    public void MoveToCell(CombatController scene, int posX, int posY)
    {
        scene.GetCell(posX, posY).ReceiveCombatant(this);
        scene.ActorCell().DismissCombatant();
        scene.actorCell = scene.GetCell(posX, posY).gameObject;

        scene.UpdateVisuals();
    }

    public void UseSkill(CombatController scene, CellController target, Skill skill)
    {
        scene.selectedSkill = skill;
        scene.ActorCell().CastSkill(target);
    }

    public void GetLoot(PlayerProperties player)
    {
        foreach (Profile character in player.party) character.ObtainXP(lootXP);

        foreach (Item item in lootItems.Keys)
        {
            if (UnityEngine.Random.Range(0f, 1f) >= lootItems.GetValueOrDefault(item))
            {
                player.AddItem(item, 1);
            }
        }

    }
}
