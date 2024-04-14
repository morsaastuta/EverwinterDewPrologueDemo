using System;
using UnityEngine;

public class DamageInputFoe : MonoBehaviour
{
    // Health bar
    public float healthMax = 1;
    public float health;
    public HealthBar healthBar;

    // Hit controller
    public int hitCooldownMax = 1;
    private int hitCooldown;
    private Color normalColor;

    void Start()
    {
        health = healthMax;
        healthBar.SetHealthMax(healthMax);
        hitCooldown = hitCooldownMax;
        normalColor = gameObject.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if (health == 0)
        {
            Die();
        }

        if (hitCooldown < hitCooldownMax)
        {
            hitCooldown++;
            if (hitCooldown == 20)
            {
                gameObject.GetComponent<Renderer>().material.color = normalColor;
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<DamageInput>().TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        if (hitCooldown == hitCooldownMax)
        {
            health -= damage;
            healthBar.SetHealth(health);
            hitCooldown = 0;
            gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
    }
}
