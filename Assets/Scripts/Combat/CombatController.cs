using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    // Master info
    public PlayerProperties playerProperties;

    // Overseer info
    public List<CellController> grid = new();
    public List<CellController> occupiedCells = new();
    public List<CellController> partyCells = new();
    public List<CellController> foeCells = new();

    // Action info
    public int turn = 0;
    public bool initRound;
    public bool initTurn;
    public List<CellController> readyCells = new();
    public CellController actorCell;
    public Skill selectedSkill;
    public ConsumableItem selectedItem;
    public int phase;

    // Combatant info
    [SerializeField] GameObject statusFrame;
    [SerializeField] TextMeshProUGUI combatantName;
    [SerializeField] Image combatantProfile;
    [SerializeField] Image combatantBG;
    [SerializeField] TextMeshProUGUI combatantHP;
    [SerializeField] TextMeshProUGUI combatantMP;

    // Combat mode (primary indicator)
    // "move": Current combatant must select a cell to move towards
    // "act": Current combatant must select a skill/item to use
    // "use": Current combatant must select a target on which use the skill/item
    // "check": Player is checking a combatant other than the current turn's combatant
    public string mode;
    public List<string> modeHistory = new();

    void Start()
    {
        grid.Clear();

        foreach (CellController cell in GetComponentsInChildren<CellController>()) grid.Add(cell);
    }

    void Update()
    {
        // If there are no ready combatants AND a round has not been initialized yet
        if (readyCells.Count <= 0 && !initRound)
        {
            foreach (CellController cell in occupiedCells) if (cell.CheckEnergy()) readyCells.Add(cell);
        }
        // Else, if a turn has not been initialized yet
        else if (!initTurn)
        {
            initTurn = true;
            NewTurn(readyCells[0]);
            readyCells.RemoveAt(0);
        }

        // If there are ready combatants AND a turn has not been initialized yet
        if (readyCells.Count > 0 && !initTurn)
        {
            ReorganizeCells();
            initRound = true;
        }
    }

    public void ReorganizeCells()
    {
        // All the received cells must be reorganized in case of a tie between characters
        // List<CellController> reorganizingCells.AddRange(readyCells);
        // readyCells.Clear();
    }

    public void NewTurn(CellController turnCell)
    {
        actorCell = turnCell;
        turn++;

        phase = 1;
        InitMode("move");
    }

    public void EndTurn()
    {
        initRound = false;
        initTurn = false;
        phase = 0;
    }

    public void SelectCell(CellController givenCell)
    {
        switch (mode)
        {
            case "move":
                // If the destination cell is empty, the current combatant switches cells
                if (givenCell.combatant == null)
                {
                    givenCell.combatant = actorCell.combatant;
                    actorCell.combatant = null;
                }
                SwitchMode("act");
                break;
            case "use":
                actorCell.CastSkill(givenCell);
                EndTurn();
                break;
        }
    }

    void ClearActionInfo()
    {
        selectedSkill = null;
        selectedItem = null;
    }

    public void SelectSkill(Skill skill)
    {
        ClearActionInfo();
        selectedSkill = skill;
        SwitchMode("use");
    }

    public void SelectItem(ConsumableItem item)
    {
        ClearActionInfo();
        selectedItem = item;
        SwitchMode("use");
    }

    public bool DirectionX(CellController target)
    {
        bool returnedVal = false;

        if (target.posX == actorCell.posX) returnedVal = true;

        return returnedVal;
    }

    public bool OrientationGreater(CellController target, bool directionX)
    {
        bool returnedVal = false;

        if (directionX)
        {
            if (target.posY > actorCell.posY) returnedVal = true;
        }
        else
        {
            if (target.posX > actorCell.posX) returnedVal = true;
        }

        return returnedVal;
    }

    public bool CastSkill(CellController target)
    {
        bool returnedVal = false;

        // If the skill is not multitarget
        if (!selectedSkill.multitarget)
        {
            returnedVal = selectedSkill.Cast(actorCell, target);
        }
        // If the skill is multitarget
        else
        {
            List<CellController> affectedCells = new();
            if (!selectedSkill.friendly) affectedCells.AddRange(foeCells);
            else affectedCells.AddRange(partyCells);

            foreach (CellController cell in affectedCells)
            {
                // Directional range
                if (selectedSkill.directional)
                {
                    // By default, we'll say the skill will always hit
                    bool willHit = true;

                    // If the direction is X
                    if (DirectionX(target))
                    {
                        // When the cells don't share posX, skill won't hit
                        if (cell.posX != target.posX) willHit = false;
                        else
                        {
                            // If the Y orientation is greater
                            if (OrientationGreater(target, DirectionX(target)))
                            {
                                // When the cell's posY is not greater, skill won't hit
                                if (target.posY <= cell.posY) willHit = false;
                            }
                            // If the Y orientation is lesser
                            else
                            {
                                // When the cell's posY is not lesser, skill won't hit
                                if (target.posY >= cell.posY) willHit = false;
                            }
                        }
                    }
                    // If the direction is Y
                    else
                    {
                        // When the cells don't share posY, skill won't hit
                        if (cell.posY != target.posY) willHit = false;
                        else
                        {
                            // If the X orientation is greater
                            if (OrientationGreater(target, DirectionX(target)))
                            {
                                // When the cell's posX is not greater, skill won't hit
                                if (target.posX <= cell.posX) willHit = false;
                            }
                            // If the X orientation is lesser
                            else
                            {
                                // When the cell's posX is not lesser, skill won't hit
                                if (target.posX >= cell.posX) willHit = false;
                            }
                        }
                    }

                    // Only after every calc, we check if the skill truly hits
                    if (willHit) returnedVal = selectedSkill.Cast(actorCell, cell);
                }
                // Squared range
                else if (selectedSkill.squared)
                {
                    if (CalcDistanceX(actorCell, cell) <= selectedSkill.range && CalcDistanceY(actorCell, cell) <= selectedSkill.range)  returnedVal = selectedSkill.Cast(actorCell, cell);
                }
                // Simple range
                else
                {
                    if (CalcDistance(actorCell, cell) <= selectedSkill.range) returnedVal = selectedSkill.Cast(actorCell, cell);
                }
            }
        }

        return returnedVal;
    }

    public bool UseItem(CellController target)
    {
        bool returnedVal = false;

        if (!selectedItem.multitarget)
        {
            returnedVal = selectedItem.Consume(actorCell, target);
        }
        else
        {
            List<CellController> affectedCells = new();
            if (!selectedItem.friendly) affectedCells.AddRange(foeCells);
            else affectedCells.AddRange(partyCells);

            foreach (CellController cell in affectedCells)
            {
                if (selectedItem.directional)
                {
                    bool willHit = true;

                    if (DirectionX(target))
                    {
                        if (cell.posX != target.posX) willHit = false;
                        else
                        {
                            if (OrientationGreater(target, DirectionX(target)))
                            {
                                if (target.posY <= cell.posY) willHit = false;
                            }
                            else
                            {
                                if (target.posY >= cell.posY) willHit = false;
                            }
                        }
                    }
                    else
                    {
                        if (cell.posY != target.posY) willHit = false;
                        else
                        {
                            if (OrientationGreater(target, DirectionX(target)))
                            {
                                if (target.posX <= cell.posX) willHit = false;
                            }
                            else
                            {
                                if (target.posX >= cell.posX) willHit = false;
                            }
                        }
                    }

                    if (willHit) returnedVal = selectedItem.Consume(actorCell, cell);
                }
                else if (selectedItem.squared)
                {
                    if (CalcDistanceX(actorCell, cell) <= selectedItem.range && CalcDistanceY(actorCell, cell) <= selectedItem.range) returnedVal = selectedItem.Consume(actorCell, cell);
                }
                else
                {
                    if (CalcDistance(actorCell, cell) <= selectedItem.range) returnedVal = selectedItem.Consume(actorCell, cell);
                }
            }
        }

        return returnedVal;
    }

    public void InitMode(string initMode)
    {
        modeHistory.Clear();
        mode = initMode;

        foreach (CellController cell in grid) cell.UpdateVisuals();
    }

    public void SwitchMode(string newMode)
    {
        modeHistory.Add(mode);
        mode = newMode;

        foreach(CellController cell in grid) cell.UpdateVisuals();
    }

    public void ReturnMode()
    {
        mode = modeHistory[modeHistory.Count - 1];
        modeHistory.RemoveAt(modeHistory.Count - 1);

        foreach (CellController cell in grid) cell.UpdateVisuals();
    }

    public int CalcDistance(CellController origin, CellController target)
    {
        int disX = Mathf.Abs(origin.posX - target.posX);
        int disY = Mathf.Abs(origin.posY - target.posY);
        return disX + disY;
    }

    public int CalcDistanceX(CellController origin, CellController target)
    {
        return Mathf.Abs(origin.posX - target.posX);
    }

    public int CalcDistanceY(CellController origin, CellController target)
    {
        return Mathf.Abs(origin.posY - target.posY);
    }

    public void Deselect()
    {
        foreach (CellController cell in grid) cell.Deselect();
    }
}
