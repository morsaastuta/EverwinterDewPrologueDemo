using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrowthGatherer : MonoBehaviour
{
    [SerializeField] public DataHUB dataHUB;
    [SerializeField] GameObject notification;
    Profile character;

    int obtained;
    [SerializeField] TextMeshProUGUI obtainedShowcase;
    int prev;
    [SerializeField] TextMeshProUGUI prevShowcase;
    [SerializeField] TextMeshProUGUI nextShowcase;

    [SerializeField] GameObject paneMJ;
    [SerializeField] TextMeshProUGUI nameMJ;
    int obtainedMJ;
    [SerializeField] TextMeshProUGUI obtainedMJShowcase;
    int prevMJ;
    [SerializeField] TextMeshProUGUI prevMJShowcase;
    [SerializeField] TextMeshProUGUI nextMJShowcase;

    [SerializeField] GameObject paneSJ;
    [SerializeField] TextMeshProUGUI nameSJ;
    int obtainedSJ;
    [SerializeField] TextMeshProUGUI obtainedSJShowcase;
    int prevSJ;
    [SerializeField] TextMeshProUGUI prevSJShowcase;
    [SerializeField] TextMeshProUGUI nextSJShowcase;

    List<Skill> skillset = new();
    [SerializeField] SlotGenerator slotGenerator;
    [SerializeField] SlotGenerator slotGeneratorMJ;
    [SerializeField] SlotGenerator slotGeneratorSJ;

    public bool ready = false;

    public void Notify(Profile c, int levelC, int xpC, int levelMJ, int xpMJ, int levelSJ, int xpSJ, List<Skill> s)
    {
        ready = true;
        character = c;

        prev = levelC;
        obtained = xpC;
        if (character.mainJob is not null)
        {
            prevMJ = levelMJ;
            obtainedMJ = xpMJ;
        }
        if (character.subJob is not null)
        {
            prevSJ = levelSJ;
            obtainedSJ = xpSJ;
        }

        skillset.AddRange(s);
    }

    public void Launch()
    {
        dataHUB.player.SetActive(false);
        dataHUB.camera.SetActive(false);
        notification.SetActive(true);

        obtainedShowcase.SetText(obtained.ToString());
        prevShowcase.SetText(prev.ToString());
        nextShowcase.SetText(character.level.ToString());

        if (character.mainJob is not null)
        {
            paneMJ.SetActive(true);
            nameMJ.SetText(character.mainJob.name);
            obtainedMJShowcase.SetText(obtainedMJ.ToString());
            prevMJShowcase.SetText(prevMJ.ToString());
            nextMJShowcase.SetText(character.mainJob.level.ToString());
        }
        else paneMJ.SetActive(false);

        if (character.subJob is not null)
        {
            paneSJ.SetActive(true);
            nameSJ.SetText(character.subJob.name);
            obtainedSJShowcase.SetText(obtainedSJ.ToString());
            prevSJShowcase.SetText(prevSJ.ToString());
            nextSJShowcase.SetText(character.subJob.level.ToString());
        }
        else paneSJ.SetActive(false);

        foreach (Skill skill in skillset)
        {
            SkillSlotController slot = null;

            if (skill.source == "self") slot = slotGenerator.Generate().GetComponent<SkillSlotController>();
            else if (character.mainJob is not null)
            {
                if (skill.source == character.mainJob.name) slot = slotGeneratorMJ.Generate().GetComponent<SkillSlotController>();
            }
            else if (character.subJob is not null)
            {
                if (skill.source == character.mainJob.name) slot = slotGeneratorSJ.Generate().GetComponent<SkillSlotController>();
            }

            slot.gameObject.transform.localScale = new(.59f, .59f, .59f);

            if (slot is not null) slot.SetSkill(skill);
        }
    }

    public void Close()
    {
        foreach (Transform slot in slotGenerator.transform) Destroy(slot.gameObject);
        foreach (Transform slot in slotGeneratorMJ.transform) Destroy(slot.gameObject);
        foreach (Transform slot in slotGeneratorSJ.transform) Destroy(slot.gameObject);

        ready = false;
        skillset.Clear();

        notification.SetActive(false);
        dataHUB.camera.SetActive(true);
        dataHUB.player.SetActive(true);
    }
}