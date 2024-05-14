using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCombat : MonoBehaviour
{
    [SerializeField] DataPersistenceManager dpm;
    [SerializeField] MapProperties mapProperties;

    public void Exit()
    {
        SceneManager.LoadScene(mapProperties.overworldScene, LoadSceneMode.Single);
        dpm.LoadGame(0);
    }
}
