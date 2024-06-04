using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    [SerializeField] Sprite emptyIcon;
    Skill skill;

    // Showcase
    [SerializeField] Image icon;
    [SerializeField] Image element;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Animator animator;
    [SerializeField] Image animIcon;

    // Elements
    [SerializeField] Sprite elementNull;

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
    }

    public void Clear()
    {
        icon.sprite = emptyIcon;
        element.sprite = emptyIcon;
        name.SetText("");
    }

    public void Select()
    {
        GetComponentInParent<SkillsController>().Select(skill);
        animator.SetTrigger("select");
    }

    public void Deselect()
    {
        animator.SetTrigger("deselect");
        animIcon.sprite = emptyIcon;
    }
}
