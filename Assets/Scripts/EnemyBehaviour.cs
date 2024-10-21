using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IEnemy
{
    [SerializeField] private Transform playerPosition;
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] float attackRange = 2f;
    bool isAttackRange;

    int enemyDamage = 20;

    [SerializeField] private LayerMask playerLayer;

    public float timeBetweenAttack = 1f;
    float timer;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ChasePlayer();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        isAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        //Debug.Log(isAttackRange);

        if (isAttackRange /*&& timer >= timeBetweenAttack*/)
        {
            //Debug.Log("PLAYER IN RANGE");
            AttackPlayer();
        }
        else
        {
            //Debug.Log("PLAYER NOT IN RANGE");
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerPosition.position);
        //if(agent.remainingDistance <= 2)
        //{
        //    agent.isStopped = true;
        //}
    }

    public void AttackPlayer()
    {
        if (timer >= timeBetweenAttack)
        {
            timer = 0f;
            agent.isStopped = true;
            agent.SetDestination(transform.position);
            anim.SetTrigger("attack");

            PlayerHealth playerHealth = playerPosition.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(enemyDamage);
            }
            Debug.Log("ENEMY ATTACKED");
        }
    }

    public void Disable()
    {
        enabled = false;
    }
}
