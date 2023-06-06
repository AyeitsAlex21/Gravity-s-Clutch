using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPrompt : MonoBehaviour
{
    public GameObject Text;

    // Start is called before the first frame update
    void Start()
    {
        Text.SetActive(false);

    }

    void OnTriggerEnter (Collider other)
    {
        Destroy(Text);
        /*
        if (other.gameObject.tag == "Player")
        {
            Text.SetActive(true);
            StartCoroutine("WaitForSec");
        }*/
    }
    IEnumerator WaitForSec()
    {
        Destroy(Text);
        yield return new WaitForSeconds(5);
        
    }
}
