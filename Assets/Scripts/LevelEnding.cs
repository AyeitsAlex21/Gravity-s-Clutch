using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject Cube;
    bool m_IsCubeAtExit;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Cube)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            //m_IsCubeAtExit = true;
        }
    }

    /*
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
       
    }*/
}
