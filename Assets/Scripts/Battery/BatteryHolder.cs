using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BatteryHolder : MonoBehaviour
{
    public GameObject Batteries;
    public Image fadeOverlay; // Reference to the UI Image
    public TextMeshProUGUI escapeText; // Reference to the UI Text

    private int numRemaining;
    private int numBat;

    void Start()
    {
        numBat = Batteries.transform.childCount;
        numRemaining = numBat;

        // Initially, make the overlay transparent and the escape text invisible
        fadeOverlay.color = new Color(0, 0, 0, 0);
        escapeText.color = new Color(1, 1, 1, 0); // Assuming white text
    }

    public void HandleBatteryTrigger(Transform hitbox, GameObject battery)
    {
        numRemaining--;

        // If no batteries remaining, start the fade to black and display escape text
        if (numRemaining == 0)
        {
            StartCoroutine(FadeToBlack());
        }
    }

    IEnumerator FadeToBlack()
    {
        float duration = 5.0f; // Duration of the fade
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // Fade the overlay to black
            fadeOverlay.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, progress);

            yield return null;
        }
        StartCoroutine(ShowEscapeText());
    }

    IEnumerator ShowEscapeText()
    {
        yield return new WaitForSeconds(1.0f); // Wait for the fade to finish

        float duration = 2.0f; // Duration of the fade in
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // Fade in the escape text
            escapeText.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, progress);
            escapeText.text = "You Escaped";

            yield return null;
        }
    }
}
