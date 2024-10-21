using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private LayerMask groundLayer;

    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        RotateAtMouse();
        Vector3 move = new Vector3(horizontalMovement, 0, verticalMovement);
        move.Normalize();
        animator.SetFloat("speed", Mathf.Abs(verticalMovement)+ Mathf.Abs(horizontalMovement));
        controller.Move(move * Time.deltaTime * playerSpeed);

    }

    void RotateAtMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        
        if(Physics.Raycast(camRay, out floorHit, 100, groundLayer))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion rotateTo = Quaternion.LookRotation(playerToMouse);
            transform.rotation = rotateTo;
        }
    }
}
