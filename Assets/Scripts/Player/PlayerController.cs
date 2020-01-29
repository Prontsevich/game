using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public Camera camera;

    public float moveSpeed;
    public float turnSpeed;

    public float lerpSpeed = 3f;

    private Quaternion newCameraRotation;
    private Quaternion newForwardRotation;

    private CharacterController body;

    private float forward;
    private float horizontal;
    private float look;
    private float turn;
    private float yRotation;
    private float xRotation;

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 contextV2 = context.ReadValue<Vector2>();
        Debug.Log("Move");
        Debug.Log(contextV2);
        horizontal = contextV2.x;
        forward = contextV2.y;
    }

    public void Look(InputAction.CallbackContext context)
    {
        //Debug.Log("Look");
        Vector2 contextV2 = context.ReadValue<Vector2>();
        turn = contextV2.x;
        look = contextV2.y;
    }

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;

        body = GetComponent<CharacterController>();

        newForwardRotation = transform.rotation;
        newCameraRotation = camera.transform.localRotation;

    }

    private void FixedUpdate()
    {
        //body.velocity = Vector3.zero;

        //Turn the player based on the Turn Axis
        yRotation = transform.rotation.eulerAngles.y;
        yRotation += turnSpeed * turn;
        newForwardRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);

        //Pitch the camera based on the Look Axis
        xRotation = camera.transform.localRotation.eulerAngles.x;
        xRotation -= turnSpeed * look;
        newCameraRotation = Quaternion.Euler(xRotation, camera.transform.localRotation.eulerAngles.y, camera.transform.localRotation.eulerAngles.z);

        Vector3 direction = new Vector3(horizontal, 0, forward);
        body.Move(direction * moveSpeed * Time.deltaTime);
        //Move the rigidbody based on forward input
        //body.AddForce(transform.forward * 1000 * moveSpeed * forward);

        //Move the rigidbody based on horizontal input
        //body.AddForce(transform.right * 1000 * moveSpeed * horizontal);

        //Lerp the rotations
        transform.rotation = Quaternion.Lerp(transform.rotation, newForwardRotation, lerpSpeed);
        //camera.transform.localRotation = Quaternion.Lerp(camera.transform.localRotation, newCameraRotation, lerpSpeed);

    }
}
