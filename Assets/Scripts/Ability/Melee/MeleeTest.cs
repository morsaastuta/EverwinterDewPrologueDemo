using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTest : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 5f;
    public LayerMask foeLayer;

    // Hit controller
    public int hitCooldownMax = 1;
    private int hitCooldown;
    private Color normalColor;

    void Start()
    {
        hitCooldown = hitCooldownMax;
        normalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hitCooldown == hitCooldownMax)
        {
            Attack();
        }

        if (hitCooldown < hitCooldownMax)
        {
            hitCooldown++;
            if (hitCooldown == 5)
            {
                gameObject.GetComponent<Renderer>().material.color = normalColor;
            }
        }
    }

    void Attack()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        Collider[] hitFoes = Physics.OverlapSphere(attackPoint.position, attackRange, foeLayer);

        foreach (Collider foe in hitFoes)
        {
            foe.GetComponent<DamageInputFoe>().TakeDamage(10);
        }

        hitCooldown = 0;
    }
}
