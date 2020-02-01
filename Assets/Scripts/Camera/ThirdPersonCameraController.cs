using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private float distance = 6f;
    [SerializeField] private float currentX = 0f;
    [SerializeField] private float currentY = 20f;
    [SerializeField] private float sensitivityX = 4f;
    [SerializeField] private float sensitivityY = 1f;
    [SerializeField] private bool inverseY = false;
    [SerializeField] private float rotationSmoothTime = 1.2f;

    private const float Y_ANGLE_MIN = 5f;
    private const float Y_ANGLE_MAX = 50f;

    private Transform cameraTransform;
    private Camera cam;
    private Vector2 lookInput;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    public void LookInput(InputAction.CallbackContext context)
    {
        Vector2 contextV2 = context.ReadValue<Vector2>();
        lookInput = contextV2;
    }

    private void Start()
    {
        cameraTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        currentX += lookInput.x * sensitivityX;
        currentY = currentY + (inverseY ? -1 : 1) * (lookInput.y * sensitivityY);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(currentY, currentX),
            ref rotationSmoothVelocity, rotationSmoothTime);
        cameraTransform.eulerAngles = currentRotation;
        cameraTransform.position = lookAt.position - cameraTransform.forward * distance;

        //Vector3 dir = new Vector3(0, 0, -distance);
        //Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        //cameraTransform.position = lookAt.position + rotation * dir;
        //cameraTransform.LookAt(lookAt.position);
    }
}
