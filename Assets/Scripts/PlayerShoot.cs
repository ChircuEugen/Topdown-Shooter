using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform gunBarrel;
    public float timeBetweenShot = 0.15f;
    public float range = 50f;
    public TrailRenderer trail;
    [SerializeField] private ParticleSystem muzzle;

    [SerializeField] private int weaponDamage = 30;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    LineRenderer lineRenderer;
    AudioSource gunSound;
    public LayerMask shootableMask;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(Input.GetButton("Fire1") && timer >= timeBetweenShot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        timer = 0f;
        gunSound.Play();
        muzzle.Play();

        //trail.enabled = true;
        //trail.SetPosition(0, gunBarrel.position);

        shootRay.origin = gunBarrel.position + gunBarrel.forward * 0.5f;
        shootRay.direction = gunBarrel.transform.forward;

        var tracer = Instantiate(trail, shootRay.origin, Quaternion.identity);
        tracer.AddPosition(shootRay.origin);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            tracer.transform.position = shootHit.point;
            EnemyHealth enemy = shootHit.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(weaponDamage);
            }
        }
        else
        {
            tracer.transform.position = shootRay.origin + shootRay.direction * range;
        }

    }
}
