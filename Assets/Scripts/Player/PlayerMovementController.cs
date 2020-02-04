using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float damping = 5f;

    private Transform cameraTransform;

    private CharacterController characterController;
    private float velocityY;
    private Vector3 currentImpact;
    private Vector3 movementInput;
    private Vector2 inputDir;
    private bool isJump;
    private Quaternion targetRot;

    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;

    private readonly float gravity = Physics.gravity.y;

    public void MoveInput(InputAction.CallbackContext context)
    {
        Vector2 contextV2 = context.ReadValue<Vector2>();

        //if (Mathf.Abs(contextV2.x) < 0.2f)
        //{
        //    contextV2.x = 0;
        //}

        //if (Mathf.Abs(contextV2.y) < 0.2f)
        //{
        //    contextV2.y = 0;
        //}

        inputDir = contextV2.normalized;
        movementInput = new Vector3(0, 0f, contextV2.y);
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
        cameraTransform = Camera.main.transform;
        targetRot = transform.rotation;
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


        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, 0) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        Vector3 velocity = movementInput.normalized * movementSpeed + Vector3.up * velocityY;

        if (currentImpact.magnitude > 0.2f)
        {
            velocity += currentImpact;
        }

        velocity = transform.rotation * velocity;

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
