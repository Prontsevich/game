using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform raycastPoint;

    public float moveSpeed = 6.0f;
    public float rotSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    private Vector2 inputDir;
    private float vertSpeed;
    private bool isJump;
    private ControllerColliderHit contact;

    private CharacterController characterController;

    private void Start()
    {
        vertSpeed = minFall;
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        float horInput = inputDir.x;
        float vertInput = inputDir.y;
        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(raycastPoint.position, Vector3.down, out hit))
        {
            float check = 0.2f;
            Debug.DrawRay(raycastPoint.position, Vector3.down, Random.ColorHSV(), check);
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            if (isJump)
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed = minFall;
            }
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }

            if (characterController.isGrounded)
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        Vector2 contextV2 = context.ReadValue<Vector2>();

        inputDir = contextV2;
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        float contextJump = context.ReadValue<float>();
        isJump = contextJump > 0;
    }
}
