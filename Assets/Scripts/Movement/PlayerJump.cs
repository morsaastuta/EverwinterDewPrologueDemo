using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerProperties player;

    [SerializeField] private Rigidbody body;
    [SerializeField] private bool grounded = false;
    [SerializeField] private float force = 1f;
    [SerializeField] private float impulse = 2000f;
    private Vector3 jump;
    private int safeCheck = 0;
    private int safeCheckMax = 10;

    private void Start()
    {
        jump = new Vector3(0f, force, 0f);
    }

    void Update()
    {
        if (player.canJump)
        {
            if (grounded && player.CompareKey(player.jumpKey))
            {
                body.AddForce(jump * impulse, ForceMode.Impulse);
                grounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            grounded = true;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain") && safeCheck >= safeCheckMax)
        {
            safeCheck = 0;
            grounded = true;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }
        else if (!grounded) safeCheck++;
    }
}
