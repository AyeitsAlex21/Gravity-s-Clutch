using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject Cube;
    bool m_IsCubeAtExit;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Cube)
        {
            m_IsCubeAtExit = true;
        }
    }

    void Update()
    {
        if (m_IsCubeAtExit)
        {
            EndLevel();
        }
    }
    void EndLevel()
    {
            Application.Quit();
       
    }
}
