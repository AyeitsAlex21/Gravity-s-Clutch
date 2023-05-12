using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    public Collider Playerobj;
    public LayerMask pickupLayer;
    public Transform holdPosition;
    public Transform orientation;
    public LayerMask ignoreLayers;

    private GameObject heldObject;
    private InputAction pickupAction;
    private Rigidbody objectRb;
    private Collider objectCollider;
    private int playerLayerIndex;
    private int pickupLayerIndex;

    void Start()
    {
        InputActionMap actionMap = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        pickupAction = actionMap.FindAction("PickUp");
        //pickupAction.performed += PickupRN;
        //pickupAction.canceled += PickupRN;
        playerLayerIndex = LayerMask.NameToLayer("Player");
        pickupLayerIndex = LayerMask.NameToLayer("Pickup");

        heldObject = null;
        objectCollider = null;
        objectRb = null;
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
                    objectRb = heldObject.GetComponent<Rigidbody>();
                    //objectRb.isKinematic = true;

                    objectCollider = heldObject.GetComponent<Collider>();
                    Physics.IgnoreCollision(Playerobj, objectCollider, true);

                    heldObject.layer = playerLayerIndex;

                    ApplyGravity gravity = heldObject.GetComponent<ApplyGravity>();
                    gravity.TurnGravityOff();
                }
            }
            else
            {
                heldObject.transform.SetParent(null);

                Physics.IgnoreCollision(Playerobj, objectCollider, false);

                //objectRb.isKinematic = false;
                ApplyGravity gravity = heldObject.GetComponent<ApplyGravity>();
                gravity.TurnGravityOn();

                objectRb.velocity = GetComponent<Rigidbody>().velocity;

                heldObject.layer = pickupLayerIndex;

                objectCollider = null;
                heldObject = null;
                objectRb = null;
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

        objectRb.velocity = Vector3.zero;

        Vector3 playerPosition = transform.position;
        Vector3 targetPosition = holdPosition.position;
        Vector3 direction = targetPosition - playerPosition;

        float holdDistance = Vector3.Distance(playerPosition, targetPosition) + (heldObject.transform.localScale.y / 2f);

        if (Physics.Raycast(playerPosition, direction, out RaycastHit hitInfo, holdDistance, ~ignoreLayers))
        {
            heldObject.transform.position = hitInfo.point - (direction.normalized * (heldObject.transform.localScale.y / 2f));
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
