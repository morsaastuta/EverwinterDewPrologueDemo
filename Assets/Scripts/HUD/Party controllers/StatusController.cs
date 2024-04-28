using TMPro;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    // Important stuff
    [SerializeField] PartyController partyController;

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

    public void LoadStats()
    {
        Profile profile = partyController.playerProperties.currentProfile;

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
    }
}
