using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    // Master info
    [SerializeField] public PlayerProperties playerProperties;
    [SerializeField] public CameraProperties cameraProperties;
    [SerializeField] public MapProperties mapProperties;
    [SerializeField] ExitCombat exitCombat;

    // Action info
    public int turn = 0;
    public bool initTurn;
    public GameObject actorCell;
    public GameObject subjectCell;
    public GameObject checkedCell;
    public Skill selectedSkill;
    public ConsumableItem selectedItem;
    public int phase;

    // Combatants
    public List<Combatant> combatants = new();
    public List<Profile> party = new();
    public List<FoeData> foes = new();

    // Turn meter HUD
    [SerializeField] GameObject toHideButton;
    [SerializeField] GameObject toShowButton;
    [SerializeField] GameObject allCards;
    [SerializeField] SlotGenerator partyCards;
    [SerializeField] SlotGenerator foeCards;

    // Act HUD: Combatant info
    [SerializeField] GameObject statusFrame;
    [SerializeField] TextMeshProUGUI combatantName;
    [SerializeField] Image combatantProfile;
    [SerializeField] TextMeshProUGUI combatantHP;
    [SerializeField] TextMeshProUGUI combatantMP;
    [SerializeField] Slider barHealth;
    [SerializeField] Slider barMana;

    // Act HUD: Action selector
    [SerializeField] GameObject actionFrame;
    [SerializeField] GameObject returnButton;
    [SerializeField] Image wieldIcon;
    [SerializeField] Sprite emptyIcon;
    [SerializeField] GameObject skillsFrame;
    [SerializeField] GameObject itemsFrame;
    [SerializeField] GameObject actionReturnButton;
    [SerializeField] GameObject fleeConfirmation;

    // Prompt HUD
    [SerializeField] GameObject promptPane;
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] TextMeshProUGUI confirmText;
    [SerializeField] TextMeshProUGUI cancelText;

    // Combat mode (primary indicator)
    // "move": Current combatant must select a cell to move towards
    // "act": Current combatant must select a skill/item to use
    // "use": Current combatant must select a target on which use the skill/item
    // "check": Player is checking a combatant other than the current turn's combatant
    public string mode;
    public List<string> modeHistory = new();

    void Start()
    {
        ClearHUD();
        cameraProperties.SetPivot(false);

        // Assign coordinates
        int totalCells = AllCells().Count;
        int valX = 0;
        int valY = 0;
        foreach (CellController cell in AllCells())
        {
            cell.posX = valX;
            cell.posY = valY;
            valX++;
            if (valX == Mathf.Sqrt(totalCells))
            {
                valX = 0;
                valY++;
            }
        }

        // Load Party
        foreach (Profile profile in playerProperties.party)
        {
            profile.MaxFatigue();
            party.Add(profile);
        }
        foreach (Profile profile in party) SetRandomPos(profile, 0, 0, 0, 5);

        // Load Foes
        foreach (FoeData foe in mapProperties.GetCurrentEncounter().GetFoes())
        {
            foe.MaxFatigue();
            foes.Add(foe);
        }
        foreach (FoeData foe in foes) SetRandomPos(foe, 5, 5, 0, 5);
    }

    void Update()
    {
        // As soon as there are no foes OR companions left, exit the combat (add WIN bool)
        if (FoeCells().Count <= 0 || PartyCells().Count <= 0) exitCombat.Exit();

        // If there are ready combatants AND a turn has not been initialized yet
        if (!initTurn)
        {
            if (ReadyCells().Count > 0)
            {
                initTurn = true;
                NewTurn(ReadyCells()[0]);
                ReorganizeCells();
            }
        }
    }

    public void LoadProfile(Combatant combatant)
    {
        combatantName.SetText(combatant.name);
        combatantProfile.sprite = combatant.GetFace(0);
        barHealth.maxValue = combatant.statHP;
        barHealth.value = combatant.currentHP;
        combatantHP.SetText(combatant.currentHP.ToString());
        barMana.maxValue = combatant.statMP;
        barMana.value = combatant.currentMP;
        combatantMP.SetText(combatant.currentMP.ToString());
    }

    public void TurnCards(bool show)
    {
        toHideButton.SetActive(show);
        allCards.SetActive(show);
        toShowButton.SetActive(!show);
    }

    public void ChangeWield(bool next)
    {
        Profile actor = (Profile)ActorCell().combatant;

        if (next)
        {
            if (actor.currentWield is null)
            {
                if (actor.wield1 is not null) actor.ChangeWield(1);
                else if (actor.wield2 is not null) actor.ChangeWield(2);
            }
            else if (actor.currentWield.Equals(actor.wield1))
            {
                if (actor.wield2 is not null) actor.ChangeWield(2);
                else actor.ChangeWield(0);
            }
            else if (actor.currentWield.Equals(actor.wield2))
            {
                actor.ChangeWield(0);
            }
        }
        else
        {
            if (actor.currentWield is null)
            {
                if (actor.wield2 is not null) actor.ChangeWield(2);
                else if (actor.wield1 is not null) actor.ChangeWield(1);
            }
            else if (actor.currentWield.Equals(actor.wield1))
            {
                actor.ChangeWield(0);
            }
            else if (actor.currentWield.Equals(actor.wield2))
            {
                if (actor.wield1 is not null) actor.ChangeWield(1);
                else actor.ChangeWield(0);
            }
        }

        actor.LoadStats();

        LoadProfile(actor);

        if (actor.currentWield is null) wieldIcon.sprite = emptyIcon;
        else wieldIcon.sprite = actor.currentWield.GetIcon();

        ActorCell().UpdateCombatantVisuals();
    }

    public TurnBarController EnterCombatant(Combatant combatant)
    {
        if (combatant.GetType().BaseType.Equals(typeof(Profile)))
        {
            GameObject obj = partyCards.Generate();
            obj.SetActive(true);
            obj.GetComponent<TurnBarController>().SetCombatant(combatant);
            return obj.GetComponent<TurnBarController>();
        }
        else if (combatant.GetType().BaseType.Equals(typeof(FoeData)))
        {
            GameObject obj = foeCards.Generate();
            obj.SetActive(true);
            obj.GetComponent<TurnBarController>().SetCombatant(combatant);
            return obj.GetComponent<TurnBarController>();
        }
        return null;
    }

    public void SetRandomPos(Combatant combatant, int minX, int maxX, int minY, int maxY)
    {
        bool done = false;
        while (done == false)
        {
            int actX = Random.Range(minX, maxX);
            int actY = Random.Range(minY, maxY);
            foreach (CellController cell in AllCells())
            {
                if (cell.combatant == null)
                {
                    if (cell.posX == actX && cell.posY == actY)
                    {
                        cell.ReceiveCombatant(combatant);
                        done = true;
                        break;
                    }
                }
            }
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
        actorCell = turnCell.gameObject;
        turn++;
        phase = 1;
        InitMode("move");
        /*
        if (ActorCell().combatant.GetType().BaseType.Equals(typeof(Profile)))
        {
        }
        else
        {
            AutoTurn();
        }
        */
    }

    public void EndTurn()
    {
        ClearHUD();
        ActorCell()
            .combatant
            .ChangeFAT(ActorCell().combatant.statFAT);
        initTurn = false;
        phase = 0;
    }

    public void SelectCell(CellController givenCell)
    {
        Deselect();

        switch (mode)
        {
            case "move":
                // If the destination cell is empty, the current combatant switches cells
                if (givenCell.combatant is null) Prompt(givenCell, "Move towards the selected cell?", "Move", "Cancel");
                else
                {
                    if (givenCell.Equals(ActorCell()))
                    {
                        if (phase == 1) SwitchMode("act", true);
                        else if (phase == 2) Prompt(givenCell, "Finalize turn without moving?", "End turn", "Cancel");
                    }
                    else CheckCombatant(givenCell, true);
                }
                break;
            case "act":
                // This case assumes selectable cells always have a combatant
                if (givenCell.Equals(ActorCell()))
                {
                    if (phase == 1) SwitchMode("move", true);
                    else if (phase == 2) Prompt(givenCell, "Finalize turn without acting?", "End turn", "Cancel");
                }
                else CheckCombatant(givenCell, true);
                break;
            case "use":
                if (selectedSkill is not null)
                {
                    switch (selectedSkill.multitarget)
                    {
                        case false: Prompt(givenCell, "Cast the skill " + selectedSkill.name + " onto the selected " + givenCell.combatant.name + "?", "Cast", "Cancel"); break;
                        case true: Prompt(givenCell, "Cast the skill " + selectedSkill.name + " onto the selected area?", "Cast", "Cancel"); break;
                    }
                }
                else if (selectedItem is not null)
                {
                    switch (selectedItem.multitarget)
                    {
                        case false: Prompt(givenCell, "Use the item " + selectedItem.name + " onto " + givenCell.combatant.name + "?", "Cast", "Cancel"); break;
                        case true: Prompt(givenCell, "Use the item " + selectedItem.name + " onto the selected area?", "Cast", "Cancel"); break;
                    }
                }
                break;
            case "check":
                if (givenCell.combatant is not null) CheckCombatant(givenCell, false);
                break;
        }
    }

    public void CheckCombatant(CellController givenCell, bool forward)
    {
        checkedCell = givenCell.gameObject;
        SwitchMode("check", forward);
    }

    public void Prompt(CellController givenCell, string prompt, string confirm, string cancel)
    {
        promptText.SetText(prompt);
        confirmText.SetText(confirm);
        cancelText.SetText(cancel);
        subjectCell = givenCell.gameObject;
        promptPane.SetActive(true);
    }

    public void Confirm()
    {
        Deselect();

        switch(mode)
        {
            case "move":
                if (SubjectCell().combatant is null)
                {
                    SubjectCell().ReceiveCombatant(ActorCell().combatant);
                    ActorCell().DismissCombatant();
                }
                actorCell = SubjectCell().gameObject;
                if (phase == 1)
                {
                    phase = 2;
                    SwitchMode("act", true);
                }
                else if (phase == 2) EndTurn();
                break;
            case "act":
                EndTurn();
                break;
            case "use":
                if (selectedSkill is not null)
                {
                    ActorCell().CastSkill(SubjectCell());
                }
                else if (selectedItem is not null)
                {
                    ActorCell().UseItem(SubjectCell());
                }
                if (phase == 1)
                {
                    phase = 2;
                    SwitchMode("move", true);
                }
                else if (phase == 2) EndTurn();
                break;
        }
    }

    public void Cancel()
    {
        Deselect();
        promptPane.SetActive(false);
    }

    void ClearActionInfo()
    {
        selectedSkill = null;
        selectedItem = null;
    }

    public void SkillsFrame()
    {
        returnButton.SetActive(false);
        ClearActionInfo();
        actionFrame.SetActive(false);
        skillsFrame.SetActive(true);
        actionReturnButton.SetActive(true);
    }

    public void SelectSkill(Skill skill)
    {
        ClearActionInfo();
        selectedSkill = skill;
        SwitchMode("use", true);
    }

    public void ItemsFrame()
    {
        returnButton.SetActive(false);
        ClearActionInfo();
        actionFrame.SetActive(false);
        itemsFrame.SetActive(true);
        actionReturnButton.SetActive(true);
    }

    public void SelectItem(ConsumableItem item)
    {
        ClearActionInfo();
        selectedItem = item;
        SwitchMode("use", true);
    }

    public void CloseActionSelector()
    {
        actionReturnButton.SetActive(false);
        itemsFrame.SetActive(false);
        skillsFrame.SetActive(false);
        actionFrame.SetActive(true);
        returnButton.SetActive(true);
    }

    public void PromptFlee()
    {
        actionFrame.SetActive(false);
        fleeConfirmation.SetActive(true);
    }

    public void CancelFlee()
    {
        fleeConfirmation.SetActive(false);
        actionFrame.SetActive(true);
    }

    public bool DirectionX(CellController target)
    {
        bool returnedVal = false;

        if (target.posX == ActorCell().posX) returnedVal = true;

        return returnedVal;
    }

    public bool OrientationGreater(CellController target, bool directionX)
    {
        bool returnedVal = false;

        if (directionX)
        {
            if (target.posY > ActorCell().posY) returnedVal = true;
        }
        else
        {
            if (target.posX > ActorCell().posX) returnedVal = true;
        }

        return returnedVal;
    }

    public bool CastSkill(CellController target)
    {
        CloseActionSelector();

        bool success = false;
        List<bool> mtSuccesses = new();

        // If the skill is not multitarget
        if (!selectedSkill.multitarget)
        {
            success = selectedSkill.Cast(
                ActorCell(), 
                    target);
        }
        // If the skill is multitarget
        else
        {
            List<CellController> affectedCells = new();
            if (!selectedSkill.friendly) affectedCells.AddRange(FoeCells());
            else affectedCells.AddRange(PartyCells());

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
                    if (willHit) mtSuccesses.Add(selectedSkill.Cast(ActorCell(), cell));
                }
                // Squared range
                else if (selectedSkill.squared)
                {
                    if (CalcDistanceX(ActorCell(), cell) <= selectedSkill.range && CalcDistanceY(ActorCell(), cell) <= selectedSkill.range) mtSuccesses.Add(selectedSkill.Cast(ActorCell(), cell));
                }
                // Simple range
                else
                {
                    if (CalcDistance(ActorCell(), cell) <= selectedSkill.range) mtSuccesses.Add(selectedSkill.Cast(ActorCell(), cell));
                }

                // If this wasn't the last hit, recover points
                if (affectedCells.IndexOf(cell) < affectedCells.Count - 1) selectedSkill.RecoverPoints(cell);
            }
        }

        // In case the hit was multitarget (mt):
        if (mtSuccesses.Count > 0) success = true;
        foreach (bool mtSuccess in mtSuccesses) if (!mtSuccess) success = false;

        return success;
    }

    public bool UseItem(CellController target)
    {
        CloseActionSelector();

        bool returnedVal = false;

        if (!selectedItem.multitarget) returnedVal = selectedItem.Consume(ActorCell(), target);
        else
        {
            List<CellController> affectedCells = new();
            if (!selectedItem.friendly) affectedCells.AddRange(FoeCells());
            else affectedCells.AddRange(PartyCells());

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

                    if (willHit) returnedVal = selectedItem.Consume(ActorCell(), cell);
                }
                else if (selectedItem.squared)
                {
                    if (CalcDistanceX(ActorCell(), cell) <= selectedItem.range && CalcDistanceY(ActorCell(), cell) <= selectedItem.range) returnedVal = selectedItem.Consume(ActorCell(), cell);
                }
                else
                {
                    if (CalcDistance(ActorCell(), cell) <= selectedItem.range) returnedVal = selectedItem.Consume(ActorCell(), cell);
                }
            }
        }

        return returnedVal;
    }

    public void InitMode(string initMode)
    {
        modeHistory.Clear();
        SwitchMode(initMode, true);
    }

    public void SwitchMode(string newMode, bool forward)
    {
        if (forward) modeHistory.Add(mode);
        mode = newMode;

        foreach (CellController cell in AllCells()) cell.UpdateVisuals();

        ClearHUD();
        switch (mode)
        {
            case "move":
                statusFrame.SetActive(true);
                LoadProfile(ActorCell().combatant);
                break;
            case "act":
                if (phase == 1) returnButton.SetActive(true);
                actionFrame.SetActive(true);
                statusFrame.SetActive(true);
                LoadProfile(ActorCell().combatant);
                break;
            case "use":
                returnButton.SetActive(true);
                break;
            case "check":
                statusFrame.SetActive(true);
                returnButton.SetActive(true);
                LoadProfile(CheckedCell().combatant);
                break;
        }

        Deselect();
    }

    void ClearHUD()
    {
        statusFrame.SetActive(false);
        returnButton.SetActive(false);
        actionFrame.SetActive(false);
        skillsFrame.SetActive(false);
        itemsFrame.SetActive(false);
        actionReturnButton.SetActive(false);
        fleeConfirmation.SetActive(false);
        promptPane.SetActive(false);
    }

    public void ReturnMode()
    {
        Deselect();
        while (modeHistory[modeHistory.Count - 1] == "check" || modeHistory[modeHistory.Count - 1] == "use") modeHistory.RemoveAt(modeHistory.Count - 1);
        SwitchMode(modeHistory[modeHistory.Count - 1], false);
        modeHistory.RemoveAt(modeHistory.Count - 1);
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
        foreach (CellController cell in AllCells()) cell.Deselect();
    }

    public List<CellController> AllCells()
    {
        return new List<CellController>(GetComponentsInChildren<CellController>());
    }

    public List<CellController> OccupiedCells()
    {
        List<CellController> returnedCells = new();

        foreach (CellController cell in AllCells())
        {
            if (cell.combatant != null)
            {
                returnedCells.Add(cell);
            }
        }
        return returnedCells;
    }

    public List<CellController> UnoccupiedCells()
    {
        List<CellController> returnedCells = new();

        foreach (CellController cell in AllCells())
        {
            if (cell.combatant == null)
            {
                returnedCells.Add(cell);
            }
        }
        return returnedCells;
    }

    public List<CellController> PartyCells()
    {
        List<CellController> returnedCells = new();

        foreach (CellController cell in OccupiedCells())
        {
            if (cell.combatant.GetType().BaseType.Equals(typeof(Profile)))
            {
                returnedCells.Add(cell);
            }
        }
        return returnedCells;
    }

    public List<CellController> FoeCells()
    {
        List<CellController> returnedCells = new();

        foreach (CellController cell in OccupiedCells())
        {
            if (cell.combatant.GetType().BaseType.Equals(typeof(FoeData)))
            {
                returnedCells.Add(cell);
            }
        }
        return returnedCells;
    }

    public List<CellController> ReadyCells()
    {
        List<CellController> returnedCells = new();

        foreach (CellController cell in OccupiedCells()) if (cell.CheckEnergy()) returnedCells.Add(cell);

        return returnedCells;
    }

    public CellController ActorCell()
    {
        return actorCell.GetComponent<CellController>();
    }

    public CellController SubjectCell()
    {
        return subjectCell.GetComponent<CellController>();
    }

    public CellController CheckedCell()
    {
        return checkedCell.GetComponent<CellController>();
    }

    public CellController GetCell(GameObject obj)
    {
        return obj.GetComponent<CellController>();
    }

    public CellController GetCell(int posX, int posY)
    {
        foreach (CellController cell in AllCells()) if (cell.posX == posX && cell.posY == posY) return cell;

        return null;
    }

    public List<CellController> GetCells(List<GameObject> objs)
    {
        List<CellController> cells = new();

        foreach (GameObject obj in objs) cells.Add(obj.GetComponent<CellController>());

        return cells;
    }

    public void AutoTurn()
    {
        foreach (CellController cell in AllCells())
        {
            if (cell.combatant is not null)
            {
                if (cell.combatant.GetType().BaseType.Equals(typeof(Profile)))
                {

                }
            }
        }
    }
}
