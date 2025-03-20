using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    public Image fadeImage; // Assign in Inspector
    public float fadeDuration = 2f;
    public GameObject player; // Assign the player GameObject in Inspector
    private MonoBehaviour playerMovementScript; // Store movement script

    void Start()
    {
        fadeImage.gameObject.SetActive(false); // Ensure it starts inactive
        if (player != null)
        {
            playerMovementScript = player.GetComponent<SC_FPSController>(); // Get movement script
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            TriggerFadeIn();
        }
    }

    public void TriggerFadeIn()
    {
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false; // Disable movement
        }

        fadeImage.gameObject.SetActive(true); // Activate the image
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true; // Re-enable movement after fade-in
        }
    }
}