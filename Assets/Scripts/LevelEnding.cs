using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject Cube;
    bool m_IsCubeAtExit;
    
    public Animator animator;
    
    public void FadeToLevel (int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }
    

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Cube)
        {
            FadeToNextLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
