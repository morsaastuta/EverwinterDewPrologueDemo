using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableChar : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement movement;

    private void Update()
    {
        if (animator.GetInteger("velocity") != movement.velocity) animator.SetInteger("velocity", movement.velocity);
        if (animator.GetInteger("direction") != movement.direction) animator.SetInteger("direction", movement.direction);
    }
}
