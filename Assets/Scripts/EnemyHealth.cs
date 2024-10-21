using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 200;
    private int currentHealth;

    private IEnemy disableBehaviour;
    private NavMeshAgent disableAgent;
    private Animator anim;
    public int enemyScore;
    bool isDead;

    private void Start()
    {
        disableAgent = GetComponent<NavMeshAgent>();
        disableBehaviour = GetComponent<IEnemy>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GetComponent<CapsuleCollider>().enabled = false;
        disableAgent.enabled = false;
        disableBehaviour.Disable();
        anim.SetTrigger("dead");
        ScoreManager.score += enemyScore;
        ScoreManager.instance.UpdateScore();
        Destroy(gameObject, 5f);
    }
}
