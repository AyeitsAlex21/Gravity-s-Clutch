using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorials : MonoBehaviour
{
    public float timeTextOnScreen;
    public TextMeshProUGUI TutorialText;

    private int tutorial_num;
    private int playerLayerIndex;
    private Color color;


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
            TutorialText.text = "Use \"Shift\" to sprint";
        }
        else if(tutorial_num == 1)
        {
            TutorialText.text = "Press \"Q\" in the direction you want to change gravity";
        }
        else if (tutorial_num == 2)
        {
            TutorialText.text = "Press \"E\" to pickup and drop objects";
        }
        else if (tutorial_num == 3)
        {
            TutorialText.text = "Find the other circuit block and place them next to eachother\n";
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
            Tutorial_setter();
            other.gameObject.SetActive(false);
        }
    }
}
