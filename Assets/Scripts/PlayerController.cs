using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float drag;
    public float airMultiplier;
    public LayerMask groundLayer;

    private GravitySwitcher gravSwitcher;
    private Quaternion currentOrientation;
    private Rigidbody rb;
    private Transform orienation;
    private InputAction jumpAction;
    private float rbMass;
    private float movementX;
    private float movementZ;
    private float playerStandingHeight;
    private float playerCrouchHeight;
    private float playerCurrentHeight;
    private bool jumpTriggered = false;
    private bool isGrounded;
    private Vector3 Gravity = new Vector3(0, -9.81f, 0);

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody>();
        orienation = transform.Find("Orientation").transform;

        playerStandingHeight = transform.localScale.y;
        playerCrouchHeight = playerStandingHeight / 2;
        playerCurrentHeight = playerStandingHeight;
        rbMass = rb.mass;

        InputActionMap actionMap = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        jumpAction = actionMap.FindAction("Jump");

        InputBinding binding = jumpAction.bindings[0];
        string path = binding.path;

        //Debug.Log(path);

        gravSwitcher = GetComponent<GravitySwitcher>();
        currentOrientation = gravSwitcher.CurrentRotation;
    }

    // Update is called once per frame
    void Update() // update each and every single frame
    {
        //Debug.Log("Update");
        if (Input.GetKeyDown(KeyCode.Space)  && isGrounded) // jumpAction.WasPressedThisFrame()
        {
            //Debug.Log("in jump");
            jumpTriggered = true;
        }

        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");
    }

    void FixedUpdate() // called before preforming any physics calculations
    {
        currentOrientation = gravSwitcher.CurrentRotation;
        rb.AddForce(currentOrientation * (Gravity * rbMass), ForceMode.Force);
        transform.rotation = currentOrientation;

        isGrounded = Physics.CheckSphere(transform.position, -playerCurrentHeight * 1.1f, groundLayer);
        // Calculate local input vector
        Vector3 localInput = new Vector3(movementX, 0, movementZ);

        // Calculate the local forward and right directions based on the player's orientation
        Vector3 localForward = Vector3.Cross(orienation.right, currentOrientation * Vector3.up);
        Vector3 localRight = Vector3.Cross(currentOrientation * Vector3.up, localForward);

        // Calculate move direction based on local input and local forward/right directions
        Vector3 moveDirection = localForward * localInput.z + localRight * localInput.x;

        if (isGrounded)
        {
            rb.drag = drag;
            rb.AddForce(moveDirection * (speed * 10));
        }
        else
        {
            rb.drag = 0;
            rb.AddForce(moveDirection * (speed * 10 * airMultiplier));
        }
        SpeedControl();

        if (jumpTriggered)
        {

            //rb.velocity = currentOrientation * new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(currentOrientation * new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            Debug.Log("VEL" + " " + rb.velocity);

            jumpTriggered = !jumpTriggered;
        }
    }

    /*
    void OnMove(InputValue movementValue) // movementValue is a vec2 for x, y movement
    {
        Debug.Log("OnMove");
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;

        //Debug.Log(movementX + " " + movementZ);
    }
    */

    private void SpeedControl()
    {
        Vector3 localVelocity = Quaternion.Inverse(currentOrientation) * rb.velocity;
        Vector3 localFlatVelocity = new Vector3(localVelocity.x, 0, localVelocity.z);

        if (localFlatVelocity.magnitude > speed)
        {
            localFlatVelocity = localFlatVelocity.normalized * speed;
            localVelocity = new Vector3(localFlatVelocity.x, localVelocity.y, localFlatVelocity.z);
            rb.velocity = currentOrientation * localVelocity;
        }
    }
}
