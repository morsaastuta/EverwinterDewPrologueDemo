using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    // Sprited locations
    [SerializeField] SpriteRenderer cellFrame;
    [SerializeField] SpriteRenderer cellType;
    [SerializeField] SpriteRenderer combatantProjection;
    [SerializeField] Animator combatantAnimator;
    [SerializeField] GameObject cursorLower;
    [SerializeField] GameObject cursorUpper;

    // Sprite resources
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite selectCompanion;
    [SerializeField] Sprite selectFoe;
    [SerializeField] Sprite safeDestination;
    [SerializeField] Sprite unsafeDestination;
    [SerializeField] Sprite combatantRange;
    [SerializeField] Sprite castCompanion;
    [SerializeField] Sprite castFoe;
    [SerializeField] Sprite closeCombatant;

    // Cell info
    public Combatant combatant = null;
    public int posX;
    public int posY;
    public bool selectable;

    // Combat references
    CombatController scene;

    void Start()
    {
        UpdateReferences();

        if (Random.Range(0, 1).Equals(0))
        {
            ReceiveCombatant(scene.playerProperties.currentProfile);
        }
        else
        {
            DismissCombatant();
        }
    }

    public void Select()
    {
        UpdateReferences();

        if (selectable)
        {
            scene.Deselect();

            cellType.color = new Color(cellType.color.r, cellType.color.b, cellType.color.g, .8f);

            if (combatant != null) cursorUpper.SetActive(true);
            else cursorLower.SetActive(true);
        }
    }

    public void Deselect()
    {
        cursorUpper.SetActive(false);
        cursorLower.SetActive(false);
        cellType.color = new Color(cellType.color.r, cellType.color.b, cellType.color.g, .5f);
    }

    public void ReceiveCombatant(Combatant newCombatant)
    {
        combatant = newCombatant;
        combatantProjection.sprite = newCombatant.spriteCS;
        combatantAnimator = newCombatant.animatorCS;
    }

    public void DismissCombatant()
    {
        combatant = null;
        combatantProjection.sprite = emptySprite;
        combatantAnimator = null;
    }

    public bool CheckEnergy()
    {
        // If the combatant's current fatigue is still greater than 0, rest (substract SPD from FAT)
        if (combatant.currentFAT > 0)
        {
            combatant.currentFAT -= combatant.statSPD;
            return false;
        }
        // If the combatant's current fatigue has reached 0, act (add MAX FAT to FAT)
        else
        {
            combatant.currentFAT += combatant.statFAT;
            return true;
        }
    }

    public void SelectSkill(Skill skill)
    {
        UpdateReferences();

        scene.SelectSkill(skill);
    }

    public void SelectItem(ConsumableItem item)
    {
        UpdateReferences();

        scene.SelectItem(item);
    }

    public bool CastSkill(CellController targetCell)
    {
        UpdateReferences();

        return scene.CastSkill(targetCell);
    }

    public bool UseItem(CellController targetCell)
    {
        UpdateReferences();

        return scene.UseItem(targetCell);
    }

    public void UpdateReferences()
    {
        scene = GetComponentInParent<CombatController>();
    }

    public void UpdateVisuals()
    {
        UpdateReferences();

        selectable = false;

        switch (scene.mode)
        {
            // If current mode is MOVE
            case "move":
                // If the cell is occupied...
                if (combatant != null)
                {
                    selectable = true;
                    // Check if the cell has a companion or a foe
                    if (combatant.GetType().BaseType.Equals(typeof(Profile))) cellType.sprite = selectCompanion;
                    else if (combatant.GetType().BaseType.Equals(typeof(FoeData))) cellType.sprite = selectFoe;
                }
                // If the cell is NOT occupied...
                else
                {
                    // Check if the cell is inside the movement range of the combatant
                    if (scene.CalcDistance(this, scene.actorCell) <= scene.actorCell.combatant.statMOV)
                    {
                        selectable = true;
                        cellType.sprite = safeDestination;

                        // Also check if it is inside the movement range of any foe
                        foreach (CellController cell in scene.foeCells)
                        {
                            if (scene.CalcDistance(this, cell) <= cell.combatant.statMOV + 1)
                            {
                                cellType.sprite = unsafeDestination;
                                break;
                            }
                        }
                    }
                    // If it is not, the cell does not matter and its type is left blank
                    else cellType.sprite = emptySprite;
                }
                break;

            // If current mode is ACT
            case "act":
                // If the cell is occupied...
                if (combatant != null)
                {
                    // Check if the cell has a companion or a foe
                    if (combatant.GetType().BaseType.Equals(typeof(Profile))) cellType.sprite = selectCompanion;
                    else if (combatant.GetType().BaseType.Equals(typeof(FoeData))) cellType.sprite = selectFoe;
                }
                // If the cell is NOT occupied...
                else cellType.sprite = emptySprite;
                break;

            // If current mode is USE
            case "use":
                // If the cell is occupied...
                if (combatant != null)
                {
                    // Varies on skill/item
                    if (scene.selectedSkill != null)
                    {
                        // The cell must be in range
                        if (scene.CalcDistance(this, scene.actorCell) <= scene.selectedSkill.range)
                        {
                            selectable = true;
                            if (!scene.selectedSkill.friendly) cellType.sprite = castFoe;
                            else cellType.sprite = castCompanion;
                        }
                        else cellType.sprite = emptySprite;
                    }
                    else if (scene.selectedItem != null)
                    {
                        // The cell must be in range
                        if (scene.CalcDistance(this, scene.actorCell) <= scene.selectedItem.range)
                        {
                            selectable = true;
                            if (!scene.selectedItem.friendly) cellType.sprite = castFoe;
                            else cellType.sprite = castCompanion;
                        }
                        else cellType.sprite = emptySprite;
                    }
                }
                break;

            // If current mode is CHECK
            case "check":
                // If the cell is occupied...
                if (combatant != null)
                {
                    selectable = true;
                    // Check if the cell has a companion or a foe
                    if (combatant.GetType().BaseType.Equals(typeof(Profile))) cellType.sprite = selectCompanion;
                    else if (combatant.GetType().BaseType.Equals(typeof(FoeData))) cellType.sprite = selectFoe;
                }
                // If the cell is NOT occupied...
                else
                {
                    // Check if the cell is inside the movement range of the combatant
                    if (scene.CalcDistance(this, scene.actorCell) <= scene.actorCell.combatant.statMOV) cellType.sprite = combatantRange;
                    // If it is not, the cell does not matter and its type is left blank
                    else cellType.sprite = emptySprite;
                }
                break;
        }
    }
}
