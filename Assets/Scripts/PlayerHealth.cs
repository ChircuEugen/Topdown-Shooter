using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    bool isDead = false;
    private Animator anim;

    private PlayerShoot playerShoot;
    private PlayerMovement playerMovement;
    private GrenadeThrow grenadeThrow;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Image gameOverScreen;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerShoot = GetComponentInChildren<PlayerShoot>();
        playerMovement = GetComponent<PlayerMovement>();
        grenadeThrow = GetComponent<GrenadeThrow>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        isDead = true;
        playerMovement.enabled = false;
        playerShoot.enabled = false;
        grenadeThrow.enabled = false;
        anim.SetTrigger("dead");
        gameOverScreen.gameObject.SetActive(true);
    }
}
