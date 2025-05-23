using UnityEngine;

public class DataHUB : MonoBehaviour
{
    [SerializeField] public PlayerBehaviour player;
    [SerializeField] public CameraProperties camera;
    [SerializeField] public WorldProperties world;
    [SerializeField] public MapProperties map;

    [SerializeField] public SavingSystem savingSystem;
    [SerializeField] public ItemGatherer itemGatherer;
    [SerializeField] public GrowthGatherer growthGatherer;
    [SerializeField] public AudioMachine audioMachine;
}
