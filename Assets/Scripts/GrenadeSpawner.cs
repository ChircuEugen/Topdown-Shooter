using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    [SerializeField] private Transform grenadePickUp;
    int grenadesSpawned = 0;

    float timePassed = 0;
    [SerializeField] float timeToCheck = 10f;

    private void Start()
    {
        for(int i=0; i<3; i++)
        {
            SpawnGrenade();
        }
        timePassed = timeToCheck;
    }

    private void Update()
    {
        //Debug.Log("Time.time = " + Time.time);
        //Debug.Log("timePassed = " + timePassed);
        if (Time.time > timePassed && grenadesSpawned < 3)
        {
            timePassed = Time.time + timeToCheck;
            SpawnGrenade();
        }
    }

    void SpawnGrenade()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-48, 49), 1.15f, Random.Range(-48, 49));
        Instantiate(grenadePickUp, randomPosition, Quaternion.identity);
        grenadesSpawned++;
    }

    public void GrenadeTaken()
    {
        grenadesSpawned--;
        if(grenadesSpawned == 2) timePassed = Time.time + timeToCheck;
    }
}
