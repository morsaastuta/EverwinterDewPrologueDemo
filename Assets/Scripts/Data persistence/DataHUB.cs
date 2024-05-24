using UnityEngine;

public class DataHUB : MonoBehaviour
{
    [SerializeField] public PlayerProperties player;
    [SerializeField] public CameraProperties camera;
    [SerializeField] public WorldProperties world;
    [SerializeField] public MapProperties map;

    [SerializeField] public SavingSystem savingSystem;
}
