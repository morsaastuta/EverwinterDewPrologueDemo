using UnityEngine;

public class ExitCombat : MonoBehaviour
{
    [SerializeField] DataPersistenceManager dpm;
    [SerializeField] CameraProperties camera;
    [SerializeField] WorldProperties world;

    public void Escape()
    {
        world.activeEncounter.fled = true;
        Exit();
    }

    public void Victory()
    {
        world.activeEncounter.defeated = true;
        Exit();
    }

    public void Defeat()
    {
        world.ExitToTitle();
    }

    void Exit()
    {
        camera.SetPivot(true);
        dpm.ChangeScene(world.currentScene);
    }
}
