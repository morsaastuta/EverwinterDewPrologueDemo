using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurnBarController : MonoBehaviour
{
    public Combatant combatant = null;

    [SerializeField] Image face;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI fatigue;
    [SerializeField] Slider fatigueBar;

    void Update()
    {
        if (combatant != null) UpdateFatigue();
    }

    public void SetCombatant(Combatant newCombatant)
    {
        combatant = newCombatant;
        UpdateVisuals();
        fatigueBar.maxValue = combatant.statFAT;
    }

    void UpdateVisuals()
    {
        face.sprite = combatant.GetFace(0);
        name.SetText(combatant.name);
    }

    void UpdateFatigue()
    {
        if (!fatigue.text.Equals(combatant.currentFAT.ToString()))
        {
            fatigue.SetText(combatant.currentFAT.ToString());
            fatigueBar.value = combatant.statFAT - combatant.currentFAT;
        }
    }
}
