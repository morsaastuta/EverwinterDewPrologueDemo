using UnityEngine;

public class SlotGenerator : MonoBehaviour
{
    [SerializeField] GameObject defaultSlot;

    public GameObject Generate()
    {
        return Instantiate(defaultSlot, Vector3.zero, Quaternion.identity);
    }

    public void Generate(int amount)
    {
        for (int i = 0; i < amount; i++) Instantiate(defaultSlot, Vector3.zero, Quaternion.identity);
    }
}