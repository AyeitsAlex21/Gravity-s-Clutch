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
        if (other.gameObject.tag == "Player")
        {
            Text.SetActive(true);
            //StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(Text);
    }
}
