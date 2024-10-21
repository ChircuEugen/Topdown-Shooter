using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyBehaviour : MonoBehaviour, IEnemy
{
    [SerializeField] private Transform playerPosition;
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] float attackRange = 10f;
    bool isAttackRange;

    //int enemyDamage = 20;

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Transform fireBallSpawnPoint;
    [SerializeField] private GameObject fireBall;

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ChasePlayer();
    }

    private void Update()
    {
        isAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        //Debug.Log(isAttackRange);
        if (isAttackRange) AttackPlayer();
        else ChasePlayer();
    }

    public void ChasePlayer()
    {
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);
        agent.SetDestination(playerPosition.position);
    }

    public void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerPosition);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", true);
    }

    void SpawnFireball()
    {
        Instantiate(fireBall, fireBallSpawnPoint.position, fireBallSpawnPoint.rotation);
    }

    public void Disable()
    {
        enabled = false;
    }
}
