using UnityEngine;

public class AccelerateActor : MonoBehaviour
{
    [SerializeField] Actor behaviour;

    void FixedUpdate()
    {
        if (GetComponent<Animator>().GetInteger("velocity") != behaviour.velocity) GetComponent<Animator>().SetInteger("velocity", behaviour.velocity);
    }
}
