using Autodesk.Fbx;
using UnityEngine;
using UnityEngine.Profiling;

public class CellController : MonoBehaviour
{
    // Sprited locations
    [SerializeField] SpriteRenderer cellFrame;
    [SerializeField] SpriteRenderer cellType;
    [SerializeField] SpriteRenderer combatantProjection;
    [SerializeField] public Transform combatantDirector;
    [SerializeField] Animator combatantAnimator;
    [SerializeField] GameObject cursorLower;
    [SerializeField] GameObject cursorUpper;
    [SerializeField] Transform projections;

    // When damaged, alter sprite hue
    Color combatantHue;
    bool affected = false;
    bool isPositive = false;

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
    Vector3 popupHeight = new(0f, .7f, 0f);
    public Combatant combatant = null;
    public int posX;
    public int posY;
    public bool selectable;
    public bool selected = false;
    public bool newlySelected = false;

    // Combatant info
    int prevHP = -1;
    int prevMP = -1;
    int prevAP = -1;

    // Combat references
    CombatController scene;
    TurnBarController card;
    PopupController popup;

    void Update()
    {
        if (combatant is not null)
        {
            if (combatant.currentHP != prevHP && prevHP >= 0)
            {
                popup.FullLaunch(combatant.currentHP - prevHP, "HP", transform.position + popupHeight, projections);
            }

            if (combatant.currentMP != prevMP && prevMP >= 0)
            {
                popup.FullLaunch(combatant.currentMP - prevMP, "MP", transform.position + popupHeight, projections);
            }

            if (combatant.currentAP != prevAP && prevAP >= 0)
            {
                popup.FullLaunch(combatant.currentAP - prevAP, "AP", transform.position + popupHeight, projections);
            }

            prevHP = combatant.currentHP;
            prevMP = combatant.currentMP;
            prevAP = combatant.currentAP;

            if (combatant.KO)
            {
                DismissCombatant();
                scene.UpdateVisuals();
            }

            if (affected)
            {
                if (isPositive)
                {
                    combatantHue.r += Time.deltaTime;
                    combatantHue.b += Time.deltaTime;
                }
                else
                {
                    combatantHue.g += Time.deltaTime;
                    combatantHue.b += Time.deltaTime;
                }

                if (combatantHue.r >= 255 && combatantHue.g >= 255 && combatantHue.b >= 255)
                {
                    combatantHue.r = 255;
                    combatantHue.g = 255;
                    combatantHue.b = 255;
                    affected = false;
                }

                combatantProjection.color = combatantHue;
            }
        }
    }

    public void EnterHover()
    {
        if (!selected)
        {
            if (combatant == null) cursorLower.SetActive(true);
            else cursorUpper.SetActive(true);
        }
    }

    public void ExitHover()
    {
        if (!selected)
        {
            cursorLower.SetActive(false);
            cursorUpper.SetActive(false);
        }
    }

    public void Select()
    {
        UpdateReferences();

        if (selectable)
        {
            newlySelected = true;
            scene.SelectCell(this);
            newlySelected = false;

            cellType.color = new Color(cellType.color.r, cellType.color.b, cellType.color.g, .8f);
            selected = true;
        }
    }

    public void Deselect()
    {
        if (!newlySelected)
        {
            cursorUpper.SetActive(false);
            cursorLower.SetActive(false);
        }
        cellType.color = new Color(cellType.color.r, cellType.color.b, cellType.color.g, .5f);
        selected = false;
    }

    public void ReceiveCombatant(Combatant newCombatant)
    {
        combatant = newCombatant;
        combatantProjection.sprite = newCombatant.GetSpritesheetCS(0);
        if (scene.turn != 0) combatantDirector.rotation = scene.ActorCell().combatantDirector.rotation;
        combatantAnimator.runtimeAnimatorController = newCombatant.GetAnimatorCS();
        UpdateCombatantVisuals();
        card = scene.EnterCombatant(newCombatant);
    }

    public void DismissCombatant()
    {
        combatant = null;
        combatantProjection.sprite = emptySprite;
        UpdateCombatantVisuals();
        Destroy(card.gameObject);

        prevHP = -1;
        prevMP = -1;
        prevAP = -1;
    }

    public bool CheckEnergy()
    {
        if (combatant != null)
        {
            // If the combatant's current fatigue is still greater than 0, substract SPD from FAT
            if (combatant.currentFAT > 0)
            {
                combatant.ChangeFAT(-combatant.statSPD);
                return false;
            }
            // If the combatant's current fatigue has reached 0, act (add MAX FAT to FAT)
            else
            {
                return true;
            }
        }
        else return false;
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

        Quaternion combatantRotation = combatantDirector.rotation;
        Vector3 rotationAngles = combatantRotation.eulerAngles;

        int disX = Mathf.Abs(targetCell.posX - posX);
        int disY = Mathf.Abs(targetCell.posY - posY);

        if (disX > disY)
        {
            if (targetCell.posX > posX) rotationAngles.y = 90;
            else rotationAngles.y = 270;
        }
        else if (disX < disY)
        {
            if (targetCell.posY > posY) rotationAngles.y = 0;
            else rotationAngles.y = 180;
        }
        else
        {
            if (targetCell.posX > posX) rotationAngles.y = 90;
            else rotationAngles.y = 270;
        }

        combatantRotation.eulerAngles = rotationAngles;
        combatantDirector.rotation = combatantRotation;

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
        popup = GetComponentInParent<PopupController>();
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
                    if (scene.CalcDistance(this, scene.ActorCell()) <= scene.ActorCell().combatant.statMOV)
                    {
                        selectable = true;
                        cellType.sprite = safeDestination;

                        // Also check if it is inside the movement range of any foe
                        foreach (CellController cell in scene.FoeCells())
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
                if (combatant is not null)
                {
                    selectable = true;

                    // Check if the cell has a companion or a foe
                    if (combatant.GetType().BaseType.Equals(typeof(Profile)))
                    {
                        // Get everything ready
                        cellType.sprite = selectCompanion;
                        Profile profile = (Profile)combatant;

                        // Change current animation depending on wield
                        if (profile.currentWield == null) combatantAnimator.SetTrigger("barehand");
                        else if (profile.currentWield.GetType().BaseType.Equals(typeof(SwordItem))) combatantAnimator.SetTrigger("sword");
                        else if (profile.currentWield.GetType().BaseType.Equals(typeof(ShieldItem))) combatantAnimator.SetTrigger("shield");
                    }
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
                    // Skill
                    if (scene.selectedSkill != null)
                    {
                        // Negative skill
                        if (!scene.selectedSkill.friendly)
                        {
                            // Is companion
                            if (combatant.GetType().BaseType.Equals(typeof(Profile)))
                            {
                                cellType.sprite = selectCompanion;
                            }
                            // Is foe (in range)
                            else if (scene.CalcDistance(this, scene.ActorCell()) <= scene.selectedSkill.range && combatant.GetType().BaseType.Equals(typeof(FoeData)))
                            {
                                selectable = true;
                                cellType.sprite = castFoe;
                            }
                            // Is foe (out of range)
                            else if (combatant.GetType().BaseType.Equals(typeof(FoeData)))
                            {
                                cellType.sprite = selectFoe;
                            }
                            else cellType.sprite = emptySprite;
                        }
                        // Positive skill
                        else
                        {
                            // Is companion (in range)
                            if (scene.CalcDistance(this, scene.ActorCell()) <= scene.selectedSkill.range && combatant.GetType().BaseType.Equals(typeof(Profile)))
                            {
                                selectable = true;
                                cellType.sprite = castCompanion;
                            }
                            // Is companion (out of range)
                            else if (combatant.GetType().BaseType.Equals(typeof(Profile)))
                            {
                                cellType.sprite = selectCompanion;
                            }
                            // Is foe
                            else if (combatant.GetType().BaseType.Equals(typeof(FoeData)))
                            {
                                cellType.sprite = selectFoe;
                            }
                            else cellType.sprite = emptySprite;
                        }
                    }
                    // Item
                    else if (scene.selectedItem != null)
                    {
                        if (!scene.selectedItem.friendly)
                        {
                            if (combatant.GetType().BaseType.Equals(typeof(Profile)))
                            {
                                cellType.sprite = selectCompanion;
                            }
                            else if (scene.CalcDistance(this, scene.ActorCell()) <= scene.selectedItem.range && combatant.GetType().BaseType.Equals(typeof(FoeData)))
                            {
                                selectable = true;
                                cellType.sprite = castFoe;
                            }
                            else if (combatant.GetType().BaseType.Equals(typeof(FoeData)))
                            {
                                cellType.sprite = selectFoe;
                            }
                            else cellType.sprite = emptySprite;
                        }
                        else
                        {
                            if (scene.CalcDistance(this, scene.ActorCell()) <= scene.selectedItem.range && combatant.GetType().BaseType.Equals(typeof(Profile)))
                            {
                                selectable = true;
                                cellType.sprite = castCompanion;
                            }
                            else if (combatant.GetType().BaseType.Equals(typeof(Profile)))
                            {
                                cellType.sprite = selectCompanion;
                            }
                            else if (combatant.GetType().BaseType.Equals(typeof(FoeData)))
                            {
                                cellType.sprite = selectFoe;
                            }
                            else cellType.sprite = emptySprite;
                        }
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
                    if (scene.CalcDistance(this, scene.CheckedCell()) <= scene.CheckedCell().combatant.statMOV) cellType.sprite = combatantRange;
                    // If it is not, the cell does not matter and its type is left blank
                    else cellType.sprite = emptySprite;
                }
                break;

            // If current mode is ENEMY
            case "enemy":
                cellType.sprite = emptySprite;
                if (scene.ActorCell().Equals(this))
                {
                    cellType.sprite = selectFoe;
                }
                break;

            // If current mode is EMPTY (turn has ended)
            case "":
                cellType.sprite = emptySprite;
                break;
        }
    }

    public void UpdateCombatantVisuals()
    {
        UpdateReferences();

        if (combatant != null)
        {
            // Check if the cell has a companion or a foe
            if (combatant.GetType().BaseType.Equals(typeof(Profile)))
            {
                // Get everything ready
                Profile profile = (Profile)combatant;

                // Change current animation depending on wield
                if (profile.currentWield == null) combatantAnimator.SetTrigger("barehand");
                else if (profile.currentWield.GetType().BaseType.Equals(typeof(SwordItem))) combatantAnimator.SetTrigger("sword");
                else if (profile.currentWield.GetType().BaseType.Equals(typeof(ShieldItem))) combatantAnimator.SetTrigger("shield");
            }
            else if (combatant.GetType().BaseType.Equals(typeof(FoeData)))
            {
                combatantAnimator.SetTrigger("physical");
            }
        }
        else combatantAnimator.SetTrigger("empty");
    }

    public void Affect(bool isBeneficial)
    {
        combatantHue = combatantProjection.color;
        if (isBeneficial)
        {
            combatantHue.r = 0;
            combatantHue.b = 0;
        }
        else
        {
            combatantHue.g = 0;
            combatantHue.b = 0;
        }
        affected = true;
        isPositive = isBeneficial;
    }

    public void RestoreColor()
    {
        affected = false;
        combatantHue.r = 255;
        combatantHue.g = 255;
        combatantHue.b = 255;
        combatantProjection.color = combatantHue;
    }
}