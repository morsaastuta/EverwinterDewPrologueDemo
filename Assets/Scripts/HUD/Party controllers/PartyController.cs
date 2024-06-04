using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public PlayerProperties playerProperties;

    // Section panes
    [SerializeField] GameObject statusFrame;
    [SerializeField] GameObject equipmentFrame;
    [SerializeField] GameObject skillsFrame;

    // Controllers
    [SerializeField] StatusController statusController;
    [SerializeField] EquipmentController equipmentController;
    [SerializeField] SkillsController skillsController;

    public void CloseAll()
    {
        statusFrame.SetActive(false);
        equipmentController.ClearSelection();
        equipmentFrame.SetActive(false);
        skillsFrame.SetActive(false);
    }

    public void Status()
    {
        CloseAll();
        statusFrame.SetActive(true);
        statusController.LoadCharacterData();
    }

    public void Equipment()
    {
        CloseAll();
        equipmentFrame.SetActive(true);
        equipmentController.LoadGearSlots(playerProperties.currentProfile);
    }

    public void Skills()
    {
        CloseAll();
        skillsFrame.SetActive(true);
        skillsController.ClearSkills();
    }

    public void UpdateHUD()
    {
        equipmentController.LoadGearSlots(playerProperties.currentProfile);
    }
}
