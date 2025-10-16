using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private InputSystem_Actions inputActions;
    private bool isGrounded;


    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private float groundCheckBuffer = 0.05f;
    [SerializeField] private LayerMask groundMask;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        inputActions = new InputSystem_Actions();

        inputActions.PlayerControls.Jump.performed += Jump;

        inputActions.PlayerControls.Enable();
    }
    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + groundCheckBuffer, groundMask);
    }
    private void FixedUpdate()
    {
        Vector2 inputVector = inputActions.PlayerControls.Move.ReadValue<Vector2>();
        playerRigidbody.AddForce(new Vector3(inputVector.x, 0, 0) * speed, ForceMode.Force);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnDisable()
    {
        inputActions.PlayerControls.Jump.performed -= Jump;
        inputActions.PlayerControls.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }
}
