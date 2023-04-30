using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyGravity : MonoBehaviour
{
    public GameObject Player;

    private Vector3 Gravity = new Vector3(0, -9.81f, 0);
    private Quaternion CurrentRotation;
    private Rigidbody rb;
    private GravitySwitcher gravitySwitcher; 

    void Start()
    {
        gravitySwitcher = Player.GetComponent<GravitySwitcher>(); 
        CurrentRotation = gravitySwitcher.CurrentRotation;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CurrentRotation = gravitySwitcher.CurrentRotation; 
        rb.AddForce(CurrentRotation * Gravity);
    }
}
