using UnityEngine;

public class MapProperties : MonoBehaviour
{
    [SerializeField] private DataPersistenceManager dpm;

    public bool pausedGame;

    void Start()
    {
        dpm.LoadGame(0);
    }

    public void SetPaused(bool state)
    {
        pausedGame = state;
    }
}
