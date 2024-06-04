using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    EnterCombat enterCombat;

    public EncounterData data;
    [SerializeField] List<string> foeNames;
    [SerializeField] List<int> foeLevels;
    [SerializeField] string combatScene;
    [SerializeField] bool isBoss;
    public bool ready = false;

    void Start()
    {
        enterCombat = GetComponentInParent<EnterCombat>();

        List<FoeData> foes = new();

        for (int i = 0; i < foeNames.Count; i++) foes.Add(Bestiary.Generate(foeNames[i], foeLevels[i]));

        data.UpdateEncounter(foes, isBoss);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (ready && collision.gameObject.layer == LayerMask.NameToLayer("Player")) enterCombat.LoadCombatScene(data, combatScene);
    }

    public void UpdateState()
    {
        if (data.defeated) gameObject.transform.parent.gameObject.SetActive(false);
        else if (data.fled)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            data.fled = false;
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(true);
            ready = true;
        }
    }
}
