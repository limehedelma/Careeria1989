using UnityEngine;

public class KiinaOllivoiceline : MonoBehaviour
{
    public AudioSource voicelineAudioSource; // Assign in Inspector
    public AudioSource backgroundAudioSource; // Assign in Inspector
    public AudioClip voiceline; // Single voiceline
    public GameObject[] objectsToLookAt; // Objects that trigger the voiceline
    private bool voiceLinePlayed = false; // Flag to ensure voiceline plays only once
    private bool[] wasPreviouslyVisible; // Track previous visibility state

    void Start()
    {
        wasPreviouslyVisible = new bool[objectsToLookAt.Length]; // Initialize array
        Invoke(nameof(StartCheckingVisibility), 1.5f); // Small delay before checking
    }

    void StartCheckingVisibility()
    {
        enabled = true; // Enable Update() after delay
    }

    void Update()
    {
        if (voiceLinePlayed || voiceline == null || voicelineAudioSource == null)
            return;

        for (int i = 0; i < objectsToLookAt.Length; i++)
        {
            GameObject obj = objectsToLookAt[i];
            if (obj != null)
            {
                bool isVisibleNow = IsObjectVisible(obj);

                // Only trigger if the object was previously invisible and now is visible
                if (isVisibleNow && !wasPreviouslyVisible[i])
                {
                    PlayVoiceline();
                    voiceLinePlayed = true; // Prevent further triggers
                    return;
                }

                wasPreviouslyVisible[i] = isVisibleNow; // Update visibility state
            }
        }
    }

    bool IsObjectVisible(GameObject obj)
    {
        Camera cam = Camera.main;
        if (cam == null) return false;

        Vector3 viewportPoint = cam.WorldToViewportPoint(obj.transform.position);
        bool isInViewport = viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;

        if (!isInViewport)
            return false;

        // Perform a Raycast from the camera to the object to check for obstructions
        Ray ray = new Ray(cam.transform.position, obj.transform.position - cam.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject == obj) // The object is directly in the player's line of sight
            {
                return true;
            }
        }

        return false; // Object is either obstructed or not in sight
    }

    void PlayVoiceline()
    {
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Pause(); // Pause background audio
        }

        voicelineAudioSource.clip = voiceline; // Assign chosen clip
        voicelineAudioSource.Play();

        Invoke(nameof(RestoreBackgroundAudio), voiceline.length);
    }

    void RestoreBackgroundAudio()
    {
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Play(); // Resume background audio
        }
    }
}
