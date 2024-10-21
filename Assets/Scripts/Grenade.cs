using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float explosionRange = 9;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private ParticleSystem explosionEffect;
    private AudioSource explosionSound;
    int damage = 300;

    private void Start()
    {
        explosionSound = GetComponent<AudioSource>();
    }

    public void FlyTowardsMouse(Vector3 mousePosition)
    {
        direction = mousePosition;
        StartCoroutine(GrenadeFly());
    }
    IEnumerator GrenadeFly()
    {
        while (Vector3.Distance(transform.position, direction) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            yield return null;
        }

        Explode();
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyHealth enemyToDamage = enemies[i].GetComponent<EnemyHealth>();
            if (enemyToDamage != null)
            {
                enemyToDamage.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, explosionRange);
    //}
}
