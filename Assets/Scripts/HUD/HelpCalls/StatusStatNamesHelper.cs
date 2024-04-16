using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusStatNamesHelper : MonoBehaviour
{
    // Helper
    [SerializeField] HelpCall helper;

    // Shown help
    [SerializeField] GameObject pane;
    TextMeshProUGUI name;
    TextMeshProUGUI description;

    [SerializeField] GameObject iconHP;
    [SerializeField] GameObject iconMP;

    [SerializeField] GameObject iconATK;
    [SerializeField] GameObject iconDFN;
    [SerializeField] GameObject iconMAG;
    [SerializeField] GameObject iconDFL;
    [SerializeField] GameObject iconSPI;

    [SerializeField] GameObject iconACC;
    [SerializeField] GameObject iconCR;
    [SerializeField] GameObject iconCD;

    [SerializeField] GameObject iconSPD;
    [SerializeField] GameObject iconMOV;

    [SerializeField] GameObject iconPSA;
    [SerializeField] GameObject iconPRA;
    [SerializeField] GameObject iconASA;
    [SerializeField] GameObject iconARA;
    [SerializeField] GameObject iconFSA;
    [SerializeField] GameObject iconFRA;
    [SerializeField] GameObject iconHSA;
    [SerializeField] GameObject iconHRA;

    Dictionary<GameObject, string> statNames = new Dictionary<GameObject, string>();
    Dictionary<GameObject, string> statDescriptions = new Dictionary<GameObject, string>();

    void Start()
    {
        statNames.Add(iconHP, "Health");
        statDescriptions.Add(iconHP, "Represents a character's maximum health. The character cannot keep acting when health drops to 0.");

        statNames.Add(iconMP, "Mana");
        statDescriptions.Add(iconMP, "Represents a character's mana capacity. Magical skills require mana to be casted.");

        statNames.Add(iconATK, "Attack");
        statDescriptions.Add(iconATK, "Represents a character's physical strength. Increases damage output from physical skills.");

        statNames.Add(iconDFN, "Defense");
        statDescriptions.Add(iconDFN, "Represents a character's physical resistance. Decreases damage input from physical skills.");

        statNames.Add(iconMAG, "Magic");
        statDescriptions.Add(iconMAG, "Represents a character's magical strength. Increases damage input from magical skills.");

        statNames.Add(iconDFL, "Deflection");
        statDescriptions.Add(iconDFL, "Represents a character's magical resistance. Decreases damage input from magical skills.");

        statNames.Add(iconSPI, "Spirit");
        statDescriptions.Add(iconSPI, "Represents a character's healing ability. Increases potency of restoration skills.");

        statNames.Add(iconACC, "Accuracy");
        statDescriptions.Add(iconACC, "Represents a character's chance of landing a hit.");

        statNames.Add(iconCR, "Crit Rate");
        statDescriptions.Add(iconCR, "Represents a character's chance of landing a critical hit.");

        statNames.Add(iconCD, "Crit Damage");
        statDescriptions.Add(iconCD, "Represents a character's damage bonus from critical hits.");

        statNames.Add(iconSPD, "Speed");
        statDescriptions.Add(iconSPD, "Represents a character's velocity and reaction in combat. Increases turn frequency.");

        statNames.Add(iconMOV, "Movement");
        statDescriptions.Add(iconMOV, "Represents a character's range of movement in combat.");

        statNames.Add(iconPSA, "Pagos Strength Affinity");
        statDescriptions.Add(iconPSA, "Represents a character's strength using the Pagos element. Directly proportional to total damage output from Pagos skills.");

        statNames.Add(iconPRA, "Pagos Resistance Affinity");
        statDescriptions.Add(iconPRA, "Represents a character's resistance to the Pagos element. Inversely proportional to total damage input from Pagos skills.");

        statNames.Add(iconASA, "Aeras Strength Affinity");
        statDescriptions.Add(iconASA, "Represents a character's strength using the Aeras element. Directly proportional to total damage output from Aeras skills.");

        statNames.Add(iconARA, "Aeras Resistance Affinity");
        statDescriptions.Add(iconARA, "Represents a character's resistance to the Aeras element. Inversely proportional to total damage input from Aeras skills.");

        statNames.Add(iconFSA, "Floga Strength Affinity");
        statDescriptions.Add(iconFSA, "Represents a character's strength using the Floga element. Directly proportional to total damage output from Floga skills.");

        statNames.Add(iconFRA, "Floga Resistance Affinity");
        statDescriptions.Add(iconFRA, "Represents a character's resistance to the Floga element. Inversely proportional to total damage input from Floga skills.");

        statNames.Add(iconHSA, "Homa Strength Affinity");
        statDescriptions.Add(iconHSA, "Represents a character's strength using the Homa element. Directly proportional to total damage output from Homa skills.");

        statNames.Add(iconHRA, "Homa Resistance Affinity");
        statDescriptions.Add(iconHRA, "Represents a character's resistance to the Homa element. Inversely proportional to total damage input from Homa skills.");

        // Determine texts

        TextMeshProUGUI[] texts = pane.GetComponentsInChildren<TextMeshProUGUI>();

        name = texts[0];
        description = texts[1];
    }

    public void CallForHelp(GameObject stat)
    {
        name.SetText(statNames.GetValueOrDefault(stat));
        description.SetText(statDescriptions.GetValueOrDefault(stat));
        helper.CallForHelp(pane);
    }
}
