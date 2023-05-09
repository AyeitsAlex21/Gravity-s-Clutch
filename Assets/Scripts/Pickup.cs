using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public LayerMask pickupLayer;
    public Transform holdPosition;
    public Transform orientation;
    public LayerMask ignoreLayers;

    private GameObject heldObject;
    private InputAction pickupAction;


    void Start()
    {
        InputActionMap actionMap = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        pickupAction = actionMap.FindAction("PickUp");
        //pickupAction.performed += PickupRN;
        //pickupAction.canceled += PickupRN;
        heldObject = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                if (Physics.Raycast(orientation.position, orientation.forward, out RaycastHit hit, 2f, pickupLayer))
                {
                    heldObject = hit.collider.gameObject;
                    heldObject.transform.SetParent(holdPosition);
                    heldObject.transform.localPosition = Vector3.zero;
                    Rigidbody objectRb = heldObject.GetComponent<Rigidbody>();

                    ApplyGravity gravity = heldObject.GetComponent<ApplyGravity>();
                    gravity.TurnGravityOff();
                }
            }
            else
            {
                heldObject.transform.SetParent(null);
                Rigidbody objectRb = heldObject.GetComponent<Rigidbody>();

                ApplyGravity gravity = heldObject.GetComponent<ApplyGravity>();
                gravity.TurnGravityOn();

                heldObject = null;
            }
        }
    }

    void FixedUpdate()
    {
        HandlePickup();
    }
    void HandlePickup()
    {
        if (heldObject == null)
        {
            return;
        }

        Rigidbody heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
        heldObjectRigidbody.velocity = Vector3.zero;

        Vector3 playerPosition = transform.position;
        Vector3 targetPosition = holdPosition.position;
        Vector3 direction = targetPosition - playerPosition;

        float holdDistance = Vector3.Distance(playerPosition, targetPosition);

        if (Physics.Raycast(playerPosition, direction, out RaycastHit hitInfo, holdDistance, ~ignoreLayers))
        {
            heldObject.transform.position = hitInfo.point;
        }
        else
        {
            heldObject.transform.position = targetPosition;
        }
    }

    void PickupRN(InputAction.CallbackContext context)
    {

        if (heldObject == null)
        {
            if (Physics.Raycast(orientation.position, orientation.forward, out RaycastHit hit, 2f, pickupLayer))
            {
                heldObject = hit.collider.gameObject;
                heldObject.transform.SetParent(holdPosition);
                heldObject.transform.localPosition = Vector3.zero;
                Rigidbody objectRb = heldObject.GetComponent<Rigidbody>();

                ApplyGravity gravity = heldObject.GetComponent<ApplyGravity>();
                gravity.TurnGravityOff();
            }
        }
        else
        {
            heldObject.transform.SetParent(null);
            Rigidbody objectRb = heldObject.GetComponent<Rigidbody>();

            ApplyGravity gravity = heldObject.GetComponent<ApplyGravity>();
            gravity.TurnGravityOn();

            heldObject = null;
        }
    }
}
