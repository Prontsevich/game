using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 6f;
    [SerializeField] private float currentX = 0f;
    [SerializeField] private float currentY = 20f;
    [SerializeField] private float sensitivityX = 4f;
    [SerializeField] private float sensitivityY = 1f;
    [SerializeField] private bool inverseY = false;
    [SerializeField] private float rotationSmoothTime = 1.2f;

    private const float Y_ANGLE_MIN = 5f;
    private const float Y_ANGLE_MAX = 50f;

    private Vector2 lookInput;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    public void LookInput(InputAction.CallbackContext context)
    {
        Vector2 contextV2 = context.ReadValue<Vector2>();
        lookInput = contextV2;
    }

    private void LateUpdate()
    {
        currentX += lookInput.x * sensitivityX;
        currentY = currentY + (inverseY ? -1 : 1) * (lookInput.y * sensitivityY);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(currentY, currentX),
            ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * distance;
    }
}
