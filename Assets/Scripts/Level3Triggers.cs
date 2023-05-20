using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level3Triggers : MonoBehaviour
{
    public float timeTextOnScreen;
    public TextMeshProUGUI TutorialText;

    private int tutorial_num;
    private int playerLayerIndex;
    private Color color;
    private string text;


    // Start is called before the first frame update
    void Start()
    {
        tutorial_num = -1;
        playerLayerIndex = LayerMask.NameToLayer("Tutorial");
        TutorialText.text = "";
        color = TutorialText.color;

    }

    void Tutorial_setter()
    {
        if (tutorial_num == 0)
        {
            StartCoroutine(InitialText("Im finally outside and I can see my ship!"));
        }
        else if (tutorial_num == 1)
        {
            StartCoroutine(InitialText(text));
        }
        else if (tutorial_num == 2)
        {
            StartCoroutine(InitialText(text));
        }

        TutorialText.canvasRenderer.SetAlpha(1);
        TutorialText.CrossFadeAlpha(0f, timeTextOnScreen, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // trigger is different then collider cuz trigger checks if something is overlapping with it
        // you have to turn on the tirgger to actually use it
        if (other.gameObject.layer == playerLayerIndex)
        {
            tutorial_num += 1;
            text = other.CompareTag("GravSwitch") ? "My gravity power won't affect me outside, but it might affect other objects still." : "Looks like the ship needs two cicuit blocks to start up";
            Tutorial_setter();
            other.gameObject.SetActive(false);
        }
    }
    IEnumerator InitialText(string text)
    {
        //yield return new WaitForSeconds(1.0f); // Wait for the fade to finish

        float duration = tutorial_num * 2; // Duration of the fade in
        float timer = 0;
        TutorialText.text = text;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;


            // Fade in the escape text
            TutorialText.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, progress);
            yield return null;
        }
    }
}
