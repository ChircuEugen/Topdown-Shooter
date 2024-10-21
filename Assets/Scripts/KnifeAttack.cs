using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPos;
    public float timeBetweenAttack = 1.07f;
    float timer;

    public int damage = 30;
    public float attackRange = 6;
    bool isAttacking = false;

    [SerializeField] private LayerMask enemyLayer;
    private Animator anim;

    private PlayerShoot playerShoot;
    private MeshRenderer knifeMesh;

    private AudioSource knifeHit;
    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerShoot = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerShoot>();
        knifeMesh = GetComponent<MeshRenderer>();
        knifeHit = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F) && timer >= timeBetweenAttack && !isAttacking)
        {
            StartCoroutine(KnifeStrike());
        }
    }

    IEnumerator KnifeStrike()
    {
        isAttacking = true;
        playerShoot.gameObject.SetActive(false);
        knifeMesh.enabled = true;
        anim.SetTrigger("knife");
        Collider[] enemies = Physics.OverlapSphere(attackPos.position, attackRange, enemyLayer);
        for(int i=0; i<enemies.Length; i++)
        {
            EnemyHealth enemyToDamage = enemies[i].GetComponent<EnemyHealth>();
            if (enemyToDamage != null)
            {
                enemyToDamage.TakeDamage(damage);
                knifeHit.Play();
                Debug.Log("ENEMY HIT");
            }
        }
        yield return new WaitForSeconds(timeBetweenAttack);
        isAttacking = false;

        playerShoot.gameObject.SetActive(true);
        knifeMesh.enabled = false;


    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(attackPos.position, attackRange);
    //}

}
