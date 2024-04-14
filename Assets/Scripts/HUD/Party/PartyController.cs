using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Profiling;

public class PartyController : MonoBehaviour
{
    public Profile currentProfile;
    List<Profile> party = new List<Profile>();

    // Controllers
    [SerializeField] EquipmentController equipmentController;

    void Awake()
    {
        party.Add(new Nikolaos());
        SelectCharacter(typeof(Nikolaos));
    }

    public void SelectCharacter(Type type)
    {
        foreach(Profile profile in party)
        {
            if (profile.GetType().Equals(type))
            {
                currentProfile = profile;
            }
        }

        UpdateHUD();
    }

    public void UpdateHUD()
    {
        equipmentController.LoadGearSlots(currentProfile);
    }

    public void EditStat(string stat, int value)
    {
        switch(stat)
        {
            case "HP":
                currentProfile.statHP = value;
                break;
            case "MP":
                currentProfile.statMP = value;
                break;

            case "ATK":
                currentProfile.statATK = value;
                break;
            case "DFN":
                currentProfile.statDFN = value;
                break;
            case "MAG":
                currentProfile.statMAG = value;
                break;
            case "DFL":
                currentProfile.statDFL = value;
                break;
            case "SPI":
                currentProfile.statSPI = value;
                break;

            case "ACC":
                currentProfile.statACC = value;
                break;
            case "CR":
                currentProfile.statCR = value;
                break;
            case "CD":
                currentProfile.statCD = value;
                break;

            case "SPD":
                currentProfile.statSPD = value;
                break;
            case "MOV":
                currentProfile.statMOV = value;
                break;

            case "PSA":
                currentProfile.statPSA = value;
                break;
            case "PRA":
                currentProfile.statPRA = value;
                break;
            case "ASA":
                currentProfile.statASA = value;
                break;
            case "ARA":
                currentProfile.statARA = value;
                break;
            case "FSA":
                currentProfile.statFSA = value;
                break;
            case "FRA":
                currentProfile.statFRA = value;
                break;
            case "HSA":
                currentProfile.statHSA = value;
                break;
            case "HRA":
                currentProfile.statHRA = value;
                break;
        }
    }
}
