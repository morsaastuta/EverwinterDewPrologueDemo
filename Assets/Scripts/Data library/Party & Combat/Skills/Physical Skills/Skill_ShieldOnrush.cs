using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Skill_ShieldOnrush : PhysicalSkill
{
    public Skill_ShieldOnrush(string s)
    {
        source = s;

        name = "Shield Onrush";
        description = "The user runs shield in hand along a straight line and hits a foe, sending it in the opposite direction with as much force as the user gathered during the rally.";
        sheetPath = "Sprites/HUD/Combat/Skills/skillsheet";
        sheetIndex = 3;

        costAP = 2;
        range = 3;
        directional = true;

        wields.Add(typeof(ShieldItem));
    }

    public override bool Cast(CellController user, CellController target)
    {
        SpendPoints(user.combatant);

        // Determine current and new position of user and target
        CombatController scene = user.scene;
        int userDistance;
        int targetDistance = 0;
        int userNextX = user.posX;
        int userNextY = user.posY;
        int targetNextX = target.posX;
        int targetNextY = target.posY;

        // If the attack occurs in the X axis:
        if (user.posX != target.posX)
        {
            userDistance = Math.Abs(target.posX - user.posX) - 1;
            if (userDistance > 0)
            {
                if (user.posX < target.posX)
                {
                    userNextX += userDistance;

                    targetDistance = scene.gridLength - 1 - target.posX;
                    if (userDistance <= targetDistance) targetDistance = userDistance;
                    targetNextX += targetDistance;
                }
                else
                {
                    userNextX -= userDistance;

                    targetDistance = target.posX;
                    if (userDistance <= targetDistance) targetDistance = userDistance;
                    targetNextX -= targetDistance;
                }
            }
        }
        // If the attack occurs in the Y axis:
        else
        {
            userDistance = Math.Abs(target.posY - user.posY) - 1;
            if (userDistance > 0)
            {
                if (user.posY < target.posY)
                {
                    userNextY += userDistance;

                    targetDistance = scene.gridLength - 1 - target.posY;
                    if (userDistance <= targetDistance) targetDistance = userDistance;
                    targetNextY += targetDistance;
                }
                else
                {
                    userNextY -= userDistance;

                    targetDistance = target.posY;
                    if (userDistance <= targetDistance) targetDistance = userDistance;
                    targetNextY -= targetDistance;
                }
            }
        }

        // Move user if needed
        if (userDistance > 0)
        {
            scene.GetCell(userNextX, userNextY).ReceiveCombatant(user.combatant);
            scene.actorCell = scene.GetCell(userNextX, userNextY).gameObject;
            user.DismissCombatant();
        }

        // Move target if needed
        if (targetDistance > 0)
        {
            scene.GetCell(targetNextX, targetNextY).ReceiveCombatant(target.combatant);
            target.DismissCombatant();
        }

        // Replace given cells with new ones (so the damage pop-ups show)
        user = scene.GetCell(userNextX, userNextY);
        target = scene.GetCell(targetNextX, targetNextY);

        if (Roll(user.combatant.statACC))
        {
            target.combatant.ChangeHP(-
                RollCrit(
                    // Physical damage
                    Formulate(user.combatant.statATK * 1.0f, target.combatant.statDFN * 0.5f) +
                    // Bonus rally damage
                    Formulate(user.combatant.statDFN * (0.5f * userDistance), 0),
                    // Critical augment
                    user.combatant.statCR, user.combatant.statCD)
                );
            return true;
        }
        else return false;
    }
}
