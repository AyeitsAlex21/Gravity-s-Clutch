using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravitySwitcher : MonoBehaviour
{
    public Transform playerOrientation;
    public Quaternion CurrentRotation; // THE VARIABLE WE CARE ABOUT AND SHARE ACROSS TO ROTATE EVERYTHING

    private InputAction ShiftGravLeft;


    Quaternion leftGrav = Quaternion.Euler(0, 0, -90);  // 90 degrees around the positive X-axis
    Quaternion rightGrav = Quaternion.Euler(0, 0, 90); // 90 degrees around the negative X-axis

    Quaternion floorGrav = Quaternion.Euler(0, 0, 0);  // 90 degrees around the positive Y-axis
    Quaternion ceilingGrav = Quaternion.Euler(0, 0, 180); // 90 degrees around the negative Y-axis

    Quaternion forwardGrav = Quaternion.Euler(-90, 0, 0);  // 90 degrees around the positive Z-axis
    Quaternion backwardGrav = Quaternion.Euler(90, 0, 0); // 90 degrees around the negative Z-axis

    Quaternion rotate0 = Quaternion.Euler(0, 0, 0);


    private Vector3[] directions;
    private Quaternion[] rotations;
    // Start is called before the first frame update
    void Start()
    {
        InputActionMap actionMap = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        ShiftGravLeft = actionMap.FindAction("ShiftGravLeft");

        ShiftGravLeft.performed += RotateLeft;


        CurrentRotation = Quaternion.Euler(0, 0, 0);

        directions = new Vector3[] { Vector3.right, -Vector3.right, Vector3.forward, -Vector3.forward, Vector3.up, -Vector3.up };
        rotations = new Quaternion[] { rightGrav, leftGrav, forwardGrav, backwardGrav, ceilingGrav, floorGrav };
    }


    void RotateLeft(InputAction.CallbackContext context)
    {

        int bestDirectionIndex = 0;
        float maxDot = float.MinValue;

        for (int i = 0; i < directions.Length; i++)
        {
            float dot = Vector3.Dot(playerOrientation.forward, directions[i]);

            if (dot > maxDot)
            {
                maxDot = dot;
                bestDirectionIndex = i;
            }
        }
        CurrentRotation = rotations[bestDirectionIndex];
    }

    /*
    void RotateLeft(InputAction.CallbackContext context)
    {
        
        Vector3[] directions = { Vector3.right, -Vector3.right, Vector3.forward, -Vector3.forward, Vector3.up, -Vector3.up };

        int bestDirectionIndex = 0;
        float maxDot = float.MinValue;

        for (int i = 0; i < directions.Length; i++)
        {
            float dot = Vector3.Dot(playerOrientation.forward, directions[i]);

            if (dot > maxDot)
            {
                maxDot = dot;
                bestDirectionIndex = i;
            }
        }
        Debug.Log(bestDirectionIndex);

        Vector3 Eulers = CurrentRotation.ToEulerAngles();

        Quaternion[] rotations = { rotate90NegX, rotate90PosX, rotate90NegZ, rotate90PosZ, 
            Eulers.z == 80 || Eulers.z == 270 ? rotate90NegX : rotate90NegZ,
            Eulers.z == 0 || Eulers.z == 180 ? rotate90PosZ : rotate90NegX};

        CurrentRotation *= rotations[bestDirectionIndex];
        CurrentRotation = Quaternion.Normalize(CurrentRotation);
    }
    */

}
