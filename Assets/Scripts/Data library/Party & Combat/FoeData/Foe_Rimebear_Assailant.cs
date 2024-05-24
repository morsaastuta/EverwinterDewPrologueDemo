using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Foe_Rimebear_Assailant : FoeData
{
    public Foe_Rimebear_Assailant(int initLV)
    {
        name = "Rimebear (Assailant)";
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
        baseHP = 80;
        baseMP = 6;
        baseATK = 5;
        baseDFN = 4;
        baseMAG = 2;
        baseDFL = 3;
        baseSPI = 1;
        baseSPD = 4;
        baseMOV = 3;
        basePSA = 115;
        basePRA = 115;

        incrHP = 6.0f;
        incrMP = 0.5f;
        incrATK = 1.2f;
        incrDFN = 0.7f;
        incrMAG = 0.3f;
        incrDFL = 0.5f;
        incrSPI = 0.1f;
        incrSPD = 0.3f;

        LoadStats();
        FullRestore();

        AddSkill("basic", new Skill_BasicAttack());
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
        if (target is not null) scene.StartCoroutine(BasicAttack(scene, target));
        else scene.EndTurn();
    }
}
