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

    private Rigidbody rb;
    private Transform orienation;
    private InputAction jumpAction;
    private float movementX;
    private float movementZ;
    private float playerStandingHeight;
    private float playerCrouchHeight;
    private float playerCurrentHeight;
    private bool jumpTriggered = false;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody>();
        orienation = transform.Find("Orientation").transform;

        playerStandingHeight = transform.localScale.y;
        playerCrouchHeight = playerStandingHeight / 2;
        playerCurrentHeight = playerStandingHeight;

        InputActionMap actionMap = GetComponent<PlayerInput>().actions.FindActionMap("Player");

        jumpAction = actionMap.FindAction("Jump");
        jumpAction.performed += OnJumpPerformed;
    }

    // Update is called once per frame
    void Update() // update each and every single frame
    {
    }

    void FixedUpdate() // called before preforming any physics calculations
    {
        isGrounded = Physics.CheckSphere(transform.position, -playerCurrentHeight, groundLayer);
        Vector3 moveDirection = orienation.forward * movementZ + orienation.right * movementX;

        if (isGrounded)
        {
            rb.drag = drag;
            rb.AddForce(moveDirection * speed * 10);
            SpeedControl();
        }
        else
        {
            rb.drag = 0;

            rb.AddForce(moveDirection * speed * 10 * airMultiplier);

            /*
            float dot = Vector3.Dot(new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized, moveDirection.normalized);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if(angle > 90)
            {
                rb.AddForce
            }
            */
        }

        if (jumpTriggered)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jumpTriggered = !jumpTriggered;
        }
    }

    private void OnMove(InputValue movementValue) // movementValue is a vec2 for x, y movement
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (isGrounded)
            jumpTriggered = true;
    }

    private void SpeedControl()
    {
        Vector3 cur_velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //Debug.Log("Before "+cur_velocity.magnitude);
        if (cur_velocity.magnitude > speed)
        {
            cur_velocity = cur_velocity.normalized * speed;
            rb.velocity = new Vector3(cur_velocity.x, rb.velocity.y, cur_velocity.z);
        }
        //Debug.Log("After " + cur_velocity.magnitude);
    }
}
