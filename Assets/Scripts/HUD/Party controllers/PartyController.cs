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
        equipmentFrame.SetActive(false);
        skillsFrame.SetActive(false);
    }

    public void Status()
    {
        CloseAll();
        statusFrame.SetActive(true);
        statusController.LoadStats();
    }

    public void Equipment()
    {
        CloseAll();
        equipmentFrame.SetActive(true);
    }

    public void Skills()
    {
        CloseAll();
        skillsFrame.SetActive(true);
    }

    public void UpdateHUD()
    {
        equipmentController.LoadGearSlots(playerProperties.currentProfile);
    }
}
