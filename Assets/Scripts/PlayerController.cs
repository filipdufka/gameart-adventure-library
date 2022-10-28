using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public InputActionReference movementControl;
    [SerializeField]
    public InputActionReference jumpControl;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMainTransform;

    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction jumpAction;

    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        cameraMainTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector2 movement = movementControl.action.ReadValue<Vector2>();

        Vector3 move = new Vector3(movement.x, 0, movement.y);
        Debug.Log(move);
        //move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        //move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        //groundedPlayer = controller.isGrounded;
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}

        //if (jumpAction.triggered && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);

        //if (movement != Vector2.zero)
        //{
        //    float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
        //    Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        //}
    }
}
