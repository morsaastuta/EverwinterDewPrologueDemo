using UnityEngine;

public class ExitCombat : MonoBehaviour
{
    [SerializeField] DataPersistenceManager dpm;
    [SerializeField] PlayerBehaviour player;
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
        foreach (FoeData foe in world.activeEncounter.GetFoes()) player.GetLoot(foe);
        Exit();
    }

    public void Defeat()
    {
        world.ExitToTitle();
    }

    void Exit()
    {
        player.MaximizeAP();
        camera.SetPivot(true);
        dpm.ChangeScene(world.currentScene);
    }
}
