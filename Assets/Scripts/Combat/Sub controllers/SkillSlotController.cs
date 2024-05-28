using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillSlotController : MonoBehaviour
{
    [SerializeField] Sprite emptyIcon;
    Skill skill;

    // Showcase
    [SerializeField] Image icon;
    [SerializeField] Image element;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Animator animator;
    [SerializeField] Image animIcon;

    // Cost meters
    [SerializeField] GameObject meterAP;
    [SerializeField] TextMeshProUGUI costAP;
    [SerializeField] GameObject meterMP;
    [SerializeField] TextMeshProUGUI costMP;

    // Elements
    [SerializeField] Sprite elementNull;

    bool selected = false;

    public void SetSkill(Skill newSkill)
    {
        skill = newSkill;
        Load();
    }

    public void Load()
    {
        Clear();

        icon.sprite = skill.GetIcon();
        if (skill.GetType().BaseType.Equals(typeof(MagicalSkill)) || skill.GetType().BaseType.Equals(typeof(MixedSkill))) element.sprite = ((MagicalSkill)skill).GetElement(0);
        else element.sprite = elementNull;
        name.SetText(skill.name);

        if (skill.costAP > 0)
        {
            costAP.SetText(skill.costAP.ToString());
            meterAP.SetActive(true);
        }

        if (skill.GetType().BaseType.Equals(typeof(MagicalSkill)) || skill.GetType().BaseType.Equals(typeof(MixedSkill)))
        {
            MagicalSkill temp = (MagicalSkill)skill;
            if (temp.costMP > 0)
            {
                costMP.SetText(temp.costMP.ToString());
                meterMP.SetActive(true);
            }
        }
    }

    public void Clear()
    {
        icon.sprite = emptyIcon;
        element.sprite = emptyIcon;
        name.SetText("");

        meterAP.SetActive(false);
        costAP.SetText("0");
        meterMP.SetActive(false);
        costAP.SetText("0");
    }

    public void IsMenuShowcase()
    {
        meterAP.SetActive(false);
        meterMP.SetActive(false);
    }

    public void Select()
    {
        GetComponentInParent<SkillsFrameController>().Select(skill);

        if (!selected)
        {
            selected = true;
            animator.SetTrigger("select");
        }
        else Deselect();
    }

    public void Deselect()
    {
        selected = false;
        animator.SetTrigger("deselect");
        animIcon.sprite = emptyIcon;
    }
}
