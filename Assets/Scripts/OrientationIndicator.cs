using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrientationIndicator : MonoBehaviour
{
    Quaternion leftGrav = Quaternion.Euler(0, 0, -90);  // 90 degrees around the positive X-axis
    Quaternion rightGrav = Quaternion.Euler(0, 0, 90); // 90 degrees around the negative X-axis

    Quaternion floorGrav = Quaternion.Euler(0, 0, 0);  // 90 degrees around the positive Y-axis
    Quaternion ceilingGrav = Quaternion.Euler(0, 0, 180); // 90 degrees around the negative Y-axis

    Quaternion forwardGrav = Quaternion.Euler(-90, 0, 0);  // 90 degrees around the positive Z-axis
    Quaternion backwardGrav = Quaternion.Euler(90, 0, 0); // 90 degrees around the negative Z-axis

    Quaternion currentOrientation;
    public TextMeshProUGUI orientationText;

    private void Start()
    {
        currentOrientation = GetComponent<GravitySwitcher>().RotateTo;
    }

    void FixedUpdate()
    {
        currentOrientation = GetComponent<GravitySwitcher>().RotateTo;

        if (currentOrientation == leftGrav)
            orientationText.text = "Left Wall";
        else if (currentOrientation == rightGrav)
            orientationText.text = "Right Wall";
        else if (currentOrientation == floorGrav)
            orientationText.text = "Floor";
        else if (currentOrientation == ceilingGrav)
            orientationText.text = "Ceiling";
        else if (currentOrientation == forwardGrav)
            orientationText.text = "Forward Wall";
        else if (currentOrientation == backwardGrav)
            orientationText.text = "Back Wall";
    }
}
