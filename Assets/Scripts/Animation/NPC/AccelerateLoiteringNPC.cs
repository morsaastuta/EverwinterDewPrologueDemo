using UnityEngine;

public class AccelerateLoiteringNPC : MonoBehaviour
{
    [SerializeField] Loitering behaviour;

    void Update()
    {
        if (GetComponent<Animator>().GetInteger("velocity") != behaviour.velocity) GetComponent<Animator>().SetInteger("velocity", behaviour.velocity);
    }
}