using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool downTrigger = false;

    [SerializeField] private bool upTrigger = false;
    public GameObject Sphere;

    [SerializeField] private AudioSource Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Sphere)
        {
            if (downTrigger)
            {
                
                myDoor.Play("Doordown", 0, 0.0f);
                Door.Play();
                gameObject.SetActive(false);
            }
            if (upTrigger)
            {
                myDoor.Play("Doorup", 0, 0.0f);
                Door.Play();
                gameObject.SetActive(false);
            }
        }
    }
}
