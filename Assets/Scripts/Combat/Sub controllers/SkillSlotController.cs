using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillSlotController : MonoBehaviour
{
    [SerializeField] Sprite emptyIcon;
    Skill skill;
    bool active = true;

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

    public Skill GetSkill()
    {
        return skill;
    }

    public void Deactivate(bool ap, bool mp)
    {
        active = false;

        List<Image> images = new();
        images.AddRange(gameObject.GetComponents<Image>());
        images.AddRange(gameObject.GetComponentsInChildren<Image>());
        foreach (Image image in images) image.color = new(.8f, .8f, .8f, .8f);

        List<Image> imagesAP = new();
        imagesAP.AddRange(meterAP.GetComponents<Image>());
        imagesAP.AddRange(meterAP.GetComponentsInChildren<Image>());
        if (ap) foreach (Image image in imagesAP) image.color = new(1f, .8f, .8f, .8f);

        List<Image> imagesMP = new();
        imagesMP.AddRange(meterMP.GetComponents<Image>());
        imagesMP.AddRange(meterMP.GetComponentsInChildren<Image>());
        if (mp) foreach (Image image in imagesMP) image.color = new(1f, .8f, .8f, .8f);
    }

    public void Load()
    {
        active = true;

        Clear();

        icon.sprite = skill.GetIcon();
        if (skill.GetType().BaseType.Equals(typeof(MagicalSkill)) || skill.GetType().BaseType.Equals(typeof(MixedSkill))) element.sprite = ((MagicalSkill)skill).GetElement(0);
        else element.sprite = elementNull;
        name.SetText(skill.name);

        if (skill.costAP > 0)
        {
            meterAP.SetActive(true);
            costAP.SetText(skill.costAP.ToString());
        }

        if (skill.GetType().BaseType.Equals(typeof(MagicalSkill)) || skill.GetType().BaseType.Equals(typeof(MixedSkill)))
        {
            MagicalSkill temp = (MagicalSkill)skill;
            if (temp.costMP > 0)
            {
                meterMP.SetActive(true);
                costMP.SetText(temp.costMP.ToString());
            }
        }
    }

    public void Clear()
    {
        icon.sprite = emptyIcon;
        element.sprite = emptyIcon;
        name.SetText("");

        costAP.SetText("0");
        meterAP.SetActive(false);
        costAP.SetText("0");
        meterMP.SetActive(false);
    }

    public void Select()
    {
        if (GetComponentInParent<SkillsFrameController>())
        {
            if (active)
            {
                GetComponentInParent<SkillsFrameController>().Select(skill);

                if (!selected)
                {
                    selected = true;
                    animator.SetTrigger("select");
                }
                else Deselect();
            }
        }
    }

    public void Deselect()
    {
        selected = false;
        animator.SetTrigger("deselect");
        animIcon.sprite = emptyIcon;
    }
}
