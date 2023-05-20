using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderHitboxes : MonoBehaviour
{
    public GameObject player;
    public GameObject batHolder;

    public GameObject bat1;
    public GameObject bat2;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == bat1 || other.gameObject == bat2)
        {
            GameObject candidate = other.gameObject == bat1 ? bat1 : bat2;

            // remove grav
            Destroy(other.gameObject.GetComponent<ApplyGravity>());
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.layer = 0;
            other.gameObject.transform.SetParent(batHolder.transform);
            candidate.transform.position = transform.position;
            candidate.transform.rotation = transform.rotation;

            Pickup obj = player.GetComponent<Pickup>();

            if (obj.heldObject != null)
            {

                obj.heldObject = null;
                obj.objectCollider = null;
                obj.objectRb = null;
            }

            GetComponentInParent<BatteryHolder>().HandleBatteryTrigger(transform, other.gameObject);

            gameObject.SetActive(false);
        }
    }
}
