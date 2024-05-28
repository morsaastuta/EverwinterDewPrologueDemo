using UnityEngine;

public class AccelerateChaser : MonoBehaviour
{
    [SerializeField] Chaser behaviour;

    void Update()
    {
        if (GetComponent<Animator>().GetInteger("velocity") != behaviour.velocity) GetComponent<Animator>().SetInteger("velocity", behaviour.velocity);
    }
}
