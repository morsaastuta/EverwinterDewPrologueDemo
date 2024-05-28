using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCombat : MonoBehaviour
{
    [SerializeField] DataPersistenceManager dpm;
    [SerializeField] DataHUB propertyHUB;

    public void LoadCombatScene(EncounterData data, string combatScene)
    {
        propertyHUB.world.SetCurrentEncounter(data);
        propertyHUB.player.InitializeAP();
        dpm.SaveGame(0);
        SceneManager.LoadScene(combatScene, LoadSceneMode.Single);
    }
}
