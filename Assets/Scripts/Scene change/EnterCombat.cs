using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCombat : MonoBehaviour
{
    [SerializeField] DataPersistenceManager dpm;
    [SerializeField] MapProperties mapProperties;

    Encounter encounter;
    List<FoeData> foes = new();
    [SerializeField] List<string> foeNames;
    [SerializeField] List<int> foeLevels;
    [SerializeField] string combatScene;

    void Start()
    {
        for (int i = 0; i < foeNames.Count; i++) foes.Add(Bestiary.Generate(foeNames[i], foeLevels[i]));

        encounter = new Encounter(foes);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            mapProperties.SetCurrentEncounter(encounter);
            dpm.SaveGame(0);
            SceneManager.LoadScene(combatScene, LoadSceneMode.Single);
        }
    }
}
