using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    EnterCombat enterCombat;

    public EncounterData data;
    List<FoeData> foes = new();
    [SerializeField] List<string> foeNames;
    [SerializeField] List<int> foeLevels;
    [SerializeField] string combatScene;
    public bool ready = false;

    void Start()
    {
        enterCombat = GetComponentInParent<EnterCombat>();

        for (int i = 0; i < foeNames.Count; i++) foes.Add(Bestiary.Generate(foeNames[i], foeLevels[i]));

        data = new EncounterData(foes);
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
    }
}
