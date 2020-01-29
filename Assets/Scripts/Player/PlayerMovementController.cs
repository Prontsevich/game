using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float damping = 5f;

    private CharacterController characterController;
    private float velocityY;
    private Vector3 currentImpact;
    private Vector3 movementInput;
    private bool isJump;

    private readonly float gravity = Physics.gravity.y;

    public void MoveInput(InputAction.CallbackContext context)
    {
        Vector2 contextV2 = context.ReadValue<Vector2>();
        movementInput = new Vector3(contextV2.x, 0f, contextV2.y);
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        float contextJump = context.ReadValue<float>();
        isJump = contextJump > 0;
    }

    public void AddForce(Vector3 direction, float magnitude)
    {
        currentImpact += direction.normalized * magnitude / mass;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (characterController.isGrounded && velocityY < 0f)
        {
            velocityY = 0f;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = movementInput * movementSpeed + Vector3.up * velocityY;

        if (currentImpact.magnitude > 0.2f)
        {
            velocity += currentImpact;
        }

        characterController.Move(velocity * Time.deltaTime);

        currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, damping * Time.deltaTime);
    }

    private void Jump()
    {
        if (isJump && characterController.isGrounded)
        {
            AddForce(Vector3.up, jumpForce);
        }
    }
}
