using System;
using TMPro;
using UnityEngine;

public class ObtainedItemHelper : MonoBehaviour
{
    // Helper
    [SerializeField] HelpCall helper;

    // Shown help
    [SerializeField] GameObject pane;
    TextMeshProUGUI name;
    TextMeshProUGUI classification;
    TextMeshProUGUI description;

    void Start()
    {
        // Determine texts
        TextMeshProUGUI[] texts = pane.GetComponentsInChildren<TextMeshProUGUI>();
        name = texts[0];
        classification = texts[1];
        description = texts[2];
    }

    public void CallForHelp(Item item)
    {
        name.SetText(item.name);

        description.SetText(item.description);

        string itemType = "";
        Type type = item.GetType().BaseType;
        if (type.Equals(typeof(ConsumableItem))) itemType = "Consumable";
        else if (type.Equals(typeof(MaterialItem))) itemType = "Material";
        else if (type.Equals(typeof(KeyItem))) itemType = "Key item";
        else if (type.Equals(typeof(HeadItem))) itemType = "Head gear";
        else if (type.Equals(typeof(BodyItem))) itemType = "Body gear";
        else if (type.Equals(typeof(ArmsItem))) itemType = "Arm gear";
        else if (type.Equals(typeof(LegsItem))) itemType = "Leg gear";
        else if (type.Equals(typeof(AccessoryItem))) itemType = "Accessory";
        else if (type.Equals(typeof(SwordItem))) itemType = "Sword";
        else if (type.Equals(typeof(ShieldItem))) itemType = "Shield";
        else if (type.Equals(typeof(BowItem))) itemType = "Bow";

        classification.SetText(itemType);

        helper.CallForHelp(pane);
    }

    public void Dismiss()
    {
        helper.Dismiss();
    }
}
