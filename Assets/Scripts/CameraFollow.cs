using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraSmooth = 5f;

    Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void FixedUpdate()
    {
        Vector3 playerPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, playerPos, cameraSmooth);
    }
}
