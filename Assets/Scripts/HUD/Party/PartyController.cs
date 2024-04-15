using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public Profile currentProfile;
    List<Profile> party = new List<Profile>();

    // Section panes
    [SerializeField] GameObject statusPane;
    [SerializeField] GameObject equipmentPane;
    [SerializeField] GameObject skillsPane;

    // Controllers
    [SerializeField] StatusController statusController;
    [SerializeField] EquipmentController equipmentController;
    [SerializeField] SkillsController skillsController;

    void Awake()
    {
        Nikolaos nikolaos = new Nikolaos();
        party.Add(nikolaos);

        foreach (Profile profile in party) if (profile.GetType().Equals(typeof(Nikolaos))) SelectCharacter(profile);
    }

    public void CloseAll()
    {
        statusPane.SetActive(false);
        equipmentPane.SetActive(false);
        skillsPane.SetActive(false);
    }

    public void Status()
    {
        CloseAll();
        statusPane.SetActive(true);
        statusController.LoadStats();
    }

    public void Equipment()
    {
        CloseAll();
        equipmentPane.SetActive(true);
    }

    public void Skills()
    {
        CloseAll();
        skillsPane.SetActive(true);
    }

    public void SelectCharacter(Profile profile)
    {
        currentProfile = profile;

        UpdateHUD();
    }

    public void UpdateHUD()
    {
        equipmentController.LoadGearSlots(currentProfile);
    }
}
