using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SavingPuddle : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerProperties properties;
    [SerializeField] private SavingSystem savingSystem;

    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;

    private void Update()
    {
        // Change animation on proximity
        if(!animator.GetBool("active") && Physics.CheckSphere(transform.position, range, playerLayer)) {
            animator.SetBool("active", true);
        }
        else if (animator.GetBool("active") && !Physics.CheckSphere(transform.position, range, playerLayer))
        {
            animator.SetBool("active", false);
        }

        // Allow save on proximity
        if (properties.canPause && animator.GetBool("active") && properties.CompareKey(properties.interactKey))
        {
            savingSystem.Initialize();
        }
    }
}