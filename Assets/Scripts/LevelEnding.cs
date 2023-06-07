using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEnding : MonoBehaviour
{
    //public float fadeDuration = 1f;
    public GameObject Cube;
    bool m_IsCubeAtExit;

    public Animator Transition;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Cube)
        {
            LoadNextLevel();
           
                //m_IsCubeAtExit = true;
           
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        //yield return new WaitForSeconds(fadeDuration);

        SceneManager.LoadScene(levelIndex);


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
