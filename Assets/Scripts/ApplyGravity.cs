using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyGravity : MonoBehaviour
{
    public GameObject Player;
    public bool AffectedByGravSwitch;

    private Vector3 Gravity = new Vector3(0, -9.81f, 0);
    private Quaternion CurrentRotation;
    private Rigidbody rb;
    private GravitySwitcher gravitySwitcher; 
    private bool ifApplyGrav;

    void Start()
    {
        gravitySwitcher = Player.GetComponent<GravitySwitcher>(); 
        CurrentRotation = gravitySwitcher.CurrentRotation;
        rb = GetComponent<Rigidbody>();
        ifApplyGrav = true;
    }

    void FixedUpdate()
    {
        if (ifApplyGrav)
        {
            if (AffectedByGravSwitch)
            {
                CurrentRotation = gravitySwitcher.CurrentRotation;
                rb.AddForce(CurrentRotation * Gravity);
            }
            else
            {
                rb.AddForce(Gravity);
            }
        }
    }

    public void TurnGravityOn()
    {
        ifApplyGrav = true;
    }

    public void TurnGravityOff()
    {
        ifApplyGrav = false;
    }
}
