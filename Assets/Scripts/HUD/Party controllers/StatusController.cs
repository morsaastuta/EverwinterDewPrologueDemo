using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // Important stuff
    [SerializeField] PartyController partyController;
    [SerializeField] GameObject metricsTab;
    [SerializeField] GameObject statsTab;


    // Character main info
    [SerializeField] TextMeshProUGUI characterName;

    // Level info
    [SerializeField] TextMeshProUGUI showcaseLevel;
    [SerializeField] TextMeshProUGUI showcaseMainJobName;
    [SerializeField] TextMeshProUGUI showcaseMainJobLevel;
    [SerializeField] TextMeshProUGUI showcaseSubJobName;
    [SerializeField] TextMeshProUGUI showcaseSubJobLevel;

    // Stat slots
    [SerializeField] TextMeshProUGUI showcaseHP;
    [SerializeField] TextMeshProUGUI showcaseMP;

    [SerializeField] TextMeshProUGUI showcaseATK;
    [SerializeField] TextMeshProUGUI showcaseDFN;
    [SerializeField] TextMeshProUGUI showcaseMAG;
    [SerializeField] TextMeshProUGUI showcaseDFL;
    [SerializeField] TextMeshProUGUI showcaseSPI;

    [SerializeField] TextMeshProUGUI showcaseACC;
    [SerializeField] TextMeshProUGUI showcaseCR;
    [SerializeField] TextMeshProUGUI showcaseCD;

    [SerializeField] TextMeshProUGUI showcaseSPD;
    [SerializeField] TextMeshProUGUI showcaseMOV;

    [SerializeField] TextMeshProUGUI showcasePSA;
    [SerializeField] TextMeshProUGUI showcasePRA;
    [SerializeField] TextMeshProUGUI showcaseASA;
    [SerializeField] TextMeshProUGUI showcaseARA;
    [SerializeField] TextMeshProUGUI showcaseSSA;
    [SerializeField] TextMeshProUGUI showcaseSRA;
    [SerializeField] TextMeshProUGUI showcaseHSA;
    [SerializeField] TextMeshProUGUI showcaseHRA;

    // Points
    [SerializeField] TextMeshProUGUI statAP;

    [SerializeField] TextMeshProUGUI counterHP;
    [SerializeField] Slider meterHP;

    [SerializeField] TextMeshProUGUI counterMP;
    [SerializeField] Slider meterMP;

    [SerializeField] TextMeshProUGUI counterCXP;
    [SerializeField] Slider meterCXP;

    [SerializeField] GameObject mainJobBox;
    [SerializeField] TextMeshProUGUI counterMXP;
    [SerializeField] Slider meterMXP;

    [SerializeField] GameObject subJobBox;
    [SerializeField] TextMeshProUGUI counterSXP;
    [SerializeField] Slider meterSXP;

    public void LoadCharacterData()
    {
        Profile profile = partyController.playerProperties.currentProfile;

        profile.LoadStats();

        characterName.SetText(profile.fullname);

        showcaseLevel.SetText(profile.level.ToString());
        meterCXP.maxValue = XPThresholdIndex.GetCharacterRemainder(profile, true);
        meterCXP.value = XPThresholdIndex.GetCharacterRemainder(profile, false);
        counterCXP.SetText((meterCXP.maxValue - meterCXP.value).ToString());

        if (profile.mainJob is not null)
        {
            mainJobBox.SetActive(true);
            showcaseMainJobName.SetText(profile.mainJob.name);
            showcaseMainJobLevel.SetText(profile.mainJob.level.ToString());
            meterMXP.maxValue = XPThresholdIndex.GetJobRemainder(profile.mainJob, true);
            meterMXP.value = XPThresholdIndex.GetJobRemainder(profile.mainJob, false);
            counterMXP.SetText((meterMXP.maxValue - meterMXP.value).ToString());
        }
        else mainJobBox.SetActive(false);

        if (profile.subJob is not null)
        {
            subJobBox.SetActive(true);
            showcaseSubJobName.SetText(profile.subJob.name);
            showcaseSubJobLevel.SetText(profile.subJob.level.ToString());
            meterSXP.maxValue = XPThresholdIndex.GetJobRemainder(profile.subJob, true);
            meterSXP.value = XPThresholdIndex.GetJobRemainder(profile.subJob, false);
            counterSXP.SetText((meterSXP.maxValue - meterSXP.value).ToString());
        }
        else subJobBox.SetActive(false);

        showcaseHP.SetText(profile.statHP.ToString());
        showcaseMP.SetText(profile.statMP.ToString());

        showcaseATK.SetText(profile.statATK.ToString());
        showcaseDFN.SetText(profile.statDFN.ToString());
        showcaseMAG.SetText(profile.statMAG.ToString());
        showcaseDFL.SetText(profile.statDFL.ToString());
        showcaseSPI.SetText(profile.statSPI.ToString());

        showcaseACC.SetText(profile.statACC.ToString() + "%");
        showcaseCR.SetText(profile.statCR.ToString() + "%");
        showcaseCD.SetText(profile.statCD.ToString() + "%");

        showcaseSPD.SetText(profile.statSPD.ToString());
        showcaseMOV.SetText(profile.statMOV.ToString());

        showcasePSA.SetText(profile.statPSA.ToString() + "%");
        showcasePRA.SetText(profile.statPRA.ToString() + "%");
        showcaseASA.SetText(profile.statASA.ToString() + "%");
        showcaseARA.SetText(profile.statARA.ToString() + "%");
        showcaseSSA.SetText(profile.statSSA.ToString() + "%");
        showcaseSRA.SetText(profile.statSRA.ToString() + "%");
        showcaseHSA.SetText(profile.statHSA.ToString() + "%");
        showcaseHRA.SetText(profile.statHRA.ToString() + "%");

        counterHP.SetText(profile.currentHP.ToString());
        meterHP.maxValue = profile.statHP;
        meterHP.value = profile.currentHP;

        counterMP.SetText(profile.currentMP.ToString());
        meterMP.maxValue = profile.statMP;
        meterMP.value = profile.currentMP;

        statAP.SetText(profile.statAP.ToString());
    }

    public void SeeMetrics()
    {
        statsTab.SetActive(false);
        LoadCharacterData();
        metricsTab.SetActive(true);
    }

    public void SeeStats()
    {
        metricsTab.SetActive(false);
        LoadCharacterData();
        statsTab.SetActive(true);
    }
}
