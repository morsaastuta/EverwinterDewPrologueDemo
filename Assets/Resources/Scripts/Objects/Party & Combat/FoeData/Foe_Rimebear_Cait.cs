using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Foe_Rimebear_Cait : FoeData
{
    public Foe_Rimebear_Cait(int initLV)
    {
        name = "Rimebear (Cait)";
        description = "A bear, slightly influenced by the flow of Pagos, that commonly inhabitates the southern forests of continental Heimonas.";

        iconPath = "Sprites/NPCs/Foes/Rimebear/iconsheet";
        profilePath = "Sprites/NPCs/Foes/Rimebear/profilesheet";
        facePath = "Sprites/NPCs/Foes/Rimebear/facesheet";
        spritesheetOWPath = "Sprites/NPCs/Foes/Rimebear/OW_spritesheet";
        spritesheetCSPath = "Sprites/NPCs/Foes/Rimebear/CS_spritesheet";
        animatorOWPath = "Animations/NPCs/Foes/Rimebear/OW_controller";
        animatorCSPath = "Animations/NPCs/Foes/Rimebear/CS_controller";

        level = initLV;
        lootXP = 300;

        baseAP = 1;
        baseHP = 1;
        baseMP = 6;
        baseATK = 6;
        baseDFN = 4;
        baseMAG = 2;
        baseDFL = 3;
        baseSPI = 1;
        baseSPD = 4;
        baseMOV = 3;
        basePSA = 125;
        basePRA = 125;

        incrHP = 0f;
        incrMP = 0.5f;
        incrATK = 1.8f;
        incrDFN = 0.9f;
        incrMAG = 0.3f;
        incrDFL = 0.7f;
        incrSPI = 0.1f;
        incrSPD = 0.9f;

        LoadStats();
        FullRestore();

        AddSkill("basic", new Skill_BasicAttack(""));
        AddSkill("blow", new Skill_UrsidBlow(""));

        lootItems.Add(new ClawRimebear(), 0.8f);
        lootItems.Add(new PeltRimebear(), 0.6f);
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
            if (CheckCost(skillID.GetValueOrDefault("blow")) && UnityEngine.Random.Range(0, 3) > 1) scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("blow"), scene, target));
            else scene.StartCoroutine(MeleeSingleAttack(skillID.GetValueOrDefault("basic"), scene, target));
        }
        else scene.EndTurn();
    }
}
