using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private Transform raycastPoint;

    public float moveSpeed = 6.0f;
    public float rotSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private Vector2 inputDir;
    private float vertSpeed;
    private ControllerColliderHit contact;
    private CharacterController characterController;

    void Start()
    {
        vertSpeed = minFall;
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        Vector3 movement = Vector3.zero;

        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(raycastPoint.position, Vector3.down, out hit))
        {
            float check = 0.2f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            vertSpeed = minFall;
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }

            if (characterController.isGrounded && contact != null)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }

        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        characterController.Move(movement);
    }
}
