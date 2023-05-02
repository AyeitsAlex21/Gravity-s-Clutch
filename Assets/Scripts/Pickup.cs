using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public LayerMask pickupLayer;
    public Transform holdPosition;
    public Transform orientation;

    private GameObject heldObject;
    private InputAction pickupAction;


    void Start()
    {
        InputActionMap actionMap = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        pickupAction = actionMap.FindAction("PickUp");
        pickupAction.performed += OnPickup;
        heldObject = null;
    }

    void FixedUpdate()
    {
        HandlePickup();
    }
    void HandlePickup()
    {
        if (heldObject != null)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f, pickupLayer))
        {
            heldObject = hit.collider.gameObject;
        }
    }

    void OnPickup(InputAction.CallbackContext context)
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
