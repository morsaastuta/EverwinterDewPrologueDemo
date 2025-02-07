using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SavingPuddle : MonoBehaviour
{
    [SerializeField] Animator animator;

    DataHUB dataHUB;

    [SerializeField] float range;
    [SerializeField] LayerMask playerLayer;

    private void Start()
    {
        dataHUB = GetComponentInParent<DataHUB>();
    }

    void Update()
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
        if (dataHUB.player.canPause && animator.GetBool("active") && dataHUB.player.CompareKey(dataHUB.player.interactKey))
        {
            dataHUB.savingSystem.Initialize();
        }
    }
}