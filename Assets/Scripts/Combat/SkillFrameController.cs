using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class SkillsFrameController : MonoBehaviour
{
    [SerializeField] public CombatController scene;
    [SerializeField] SlotGenerator slotGenerator;
    [SerializeField] RectTransform skillSelectionPane;
    List<GameObject> skillSlots = new();

    public void Physical()
    {
        ClearSkills();

        foreach (Skill skill in scene.ActorCell().combatant.skillset)
        {
            if (skill.GetType().BaseType.Equals(typeof(PhysicalSkill)))
            {
                if (((PhysicalSkill)skill).ContainsWield(((Profile)scene.ActorCell().combatant).currentWield))
                {
                    GameObject slot = slotGenerator.Generate();
                    slot.transform.SetParent(skillSelectionPane);
                    slot.GetComponent<SkillSlotController>().SetSkill(skill);
                    slot.GetComponent<SkillSlotController>().IsMenuShowcase();
                    skillSlots.Add(slot);
                }
            }
        }
    }

    public void Magical()
    {
        ClearSkills();

        foreach (Skill skill in scene.ActorCell().combatant.skillset)
        {
            if (skill.GetType().BaseType.Equals(typeof(MagicalSkill)))
            {
                GameObject slot = slotGenerator.Generate();
                slot.transform.SetParent(skillSelectionPane);
                slot.GetComponent<SkillSlotController>().SetSkill(skill);
                slot.GetComponent<SkillSlotController>().IsMenuShowcase();
                skillSlots.Add(slot);
            }
        }
    }

    public void Mixed()
    {
        ClearSkills();

        foreach (Skill skill in scene.ActorCell().combatant.skillset)
        {
            if (skill.GetType().BaseType.Equals(typeof(MixedSkill)))
            {
                if (((MixedSkill)skill).ContainsWield(((Profile)scene.ActorCell().combatant).currentWield))
                {
                    GameObject slot = slotGenerator.Generate();
                    slot.transform.SetParent(skillSelectionPane);
                    slot.GetComponent<SkillSlotController>().SetSkill(skill);
                    slot.GetComponent<SkillSlotController>().IsMenuShowcase();
                    skillSlots.Add(slot);
                }
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

    public void Deselect()
    {
        foreach (GameObject obj in skillSlots)
        {
            obj.GetComponent<SkillSlotController>().Deselect();
        }
    }

    public void Select(Skill skill)
    {
        if (scene.mode.Equals("use"))
        {
            scene.ReturnMode();
            if (!skill.Equals(scene.selectedSkill)) scene.SelectSkill(skill);
        }
        else scene.SelectSkill(skill);
    }

    public void Exit()
    {
        scene.ReturnMode();
    }
}
