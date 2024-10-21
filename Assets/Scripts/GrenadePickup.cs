using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GrenadeThrow grenadeThrow = other.GetComponent<GrenadeThrow>();
            grenadeThrow.grenadeCount++;
            grenadeThrow.UpdateGrenadeUI();
            GrenadeSpawner grenadeSpawner = GameObject.FindGameObjectWithTag("GrenadeManager").GetComponent<GrenadeSpawner>();
            grenadeSpawner.GrenadeTaken();
            Destroy(gameObject);
        }
    }
}
