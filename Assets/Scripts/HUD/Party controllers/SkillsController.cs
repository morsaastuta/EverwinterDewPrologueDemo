using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsController : MonoBehaviour
{
    [SerializeField] PartyController partyController;
    [SerializeField] Sprite emptyIcon;

    // Showcase
    [SerializeField] Image skillIcon;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI skillDescription;
    [SerializeField] GameObject physicalSet;
    [SerializeField] GameObject magicalSet;

    // Stat meters
    [SerializeField] TextMeshProUGUI costAP;
    [SerializeField] TextMeshProUGUI costMP;
    [SerializeField] TextMeshProUGUI range;

    // Wields
    [SerializeField] Image wieldSlot1;
    [SerializeField] Image wieldSlot2;
    [SerializeField] Image wieldSlot3;
    [SerializeField] Image wieldSlot4;

    // Elements
    [SerializeField] Image elementSlot1;
    [SerializeField] Image elementSlot2;
    [SerializeField] Image elementSlot3;
    [SerializeField] Image elementSlot4;

    // Skill list
    [SerializeField] SlotGenerator slotGenerator;
    [SerializeField] RectTransform skillSelectionPane;
    List<GameObject> skillSlots = new();

    public void Physical()
    {
        ClearSkills();
        physicalSet.SetActive(true);
        magicalSet.SetActive(false);

        foreach (Skill skill in partyController.playerProperties.currentProfile.skillset)
        {
            if (skill.GetType().BaseType.Equals(typeof(PhysicalSkill)))
            {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(skillSelectionPane);
                slot.GetComponent<SkillController>().SetSkill(skill);
                slot.GetComponent<SkillController>().IsMenuShowcase();
                skillSlots.Add(slot);
            }
        }
    }

    public void Magical()
    {
        ClearSkills();
        physicalSet.SetActive(false);
        magicalSet.SetActive(true);

        foreach (Skill skill in partyController.playerProperties.currentProfile.skillset)
        {
            if (skill.GetType().BaseType.Equals(typeof(MagicalSkill))) {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(skillSelectionPane);
                slot.GetComponent<SkillController>().SetSkill(skill);
                slot.GetComponent<SkillController>().IsMenuShowcase();
                skillSlots.Add(slot);
            }
        }
    }

    public void Mixed()
    {
        ClearSkills();
        physicalSet.SetActive(true);
        magicalSet.SetActive(true);

        foreach (Skill skill in partyController.playerProperties.currentProfile.skillset)
        {
            if (skill.GetType().BaseType.Equals(typeof(MixedSkill)))
            {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(skillSelectionPane);
                slot.GetComponent<SkillController>().SetSkill(skill);
                slot.GetComponent<SkillController>().IsMenuShowcase();
                skillSlots.Add(slot);
            }
        }
    }

    public void ClearSkills()
    {
        Deselect();

        foreach (GameObject obj in skillSlots)
        {
            Destroy(obj);
        }

        skillSlots.Clear();
    }

    public void Select(Skill skill)
    {
        Deselect();

        skillIcon.sprite = skill.GetIcon();
        skillName.SetText(skill.name);
        skillDescription.SetText(skill.description);

        costAP.SetText(skill.costAP.ToString());
        if(skill.GetType().BaseType.Equals(typeof(MagicalSkill)) || skill.GetType().BaseType.Equals(typeof(MixedSkill))) costMP.SetText(((MagicalSkill)skill).costMP.ToString());
        else costMP.SetText("-");
        range.SetText(skill.range.ToString());

        if (skill.GetType().BaseType.Equals(typeof(PhysicalSkill)))
        {
            wieldSlot1.sprite = ((PhysicalSkill)skill).GetWield(0);
            wieldSlot2.sprite = ((PhysicalSkill)skill).GetWield(1);
            wieldSlot3.sprite = ((PhysicalSkill)skill).GetWield(2);
            wieldSlot4.sprite = ((PhysicalSkill)skill).GetWield(3);
        } else if (skill.GetType().BaseType.Equals(typeof(MixedSkill)))
        {
            wieldSlot1.sprite = ((MixedSkill)skill).GetWield(0);
            wieldSlot2.sprite = ((MixedSkill)skill).GetWield(1);
            wieldSlot3.sprite = ((MixedSkill)skill).GetWield(2);
            wieldSlot4.sprite = ((MixedSkill)skill).GetWield(3);
        }

        if (skill.GetType().BaseType.Equals(typeof(MagicalSkill)) || skill.GetType().BaseType.Equals(typeof(MixedSkill)))
        {
            elementSlot1.sprite = ((MagicalSkill)skill).GetElement(0);
            elementSlot2.sprite = ((MagicalSkill)skill).GetElement(1);
            elementSlot3.sprite = ((MagicalSkill)skill).GetElement(2);
            elementSlot4.sprite = ((MagicalSkill)skill).GetElement(3);
        }

    }

    public void Deselect()
    {
        ClearShowcase();

        foreach (GameObject obj in skillSlots)
        {
            obj.GetComponent<SkillController>().Deselect();
        }
    }

    public void ClearShowcase()
    {
        skillIcon.sprite = emptyIcon;
        skillName.SetText("");
        skillDescription.SetText("");

        costAP.SetText("");
        costMP.SetText("");
        range.SetText("");

        wieldSlot1.sprite = emptyIcon;
        wieldSlot2.sprite = emptyIcon;
        wieldSlot3.sprite = emptyIcon;
        wieldSlot4.sprite = emptyIcon;

        elementSlot1.sprite = emptyIcon;
        elementSlot2.sprite = emptyIcon;
        elementSlot3.sprite = emptyIcon;
        elementSlot4.sprite = emptyIcon;
    }
}
