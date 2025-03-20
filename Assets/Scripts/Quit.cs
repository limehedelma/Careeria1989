using UnityEngine;

public class QuitOnAudioEnd : MonoBehaviour
{
    public AudioSource audioSource; // Assign your AudioSource in the inspector

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
            return;
        }
    }

    void Update()
    {
        // Check if the audio has finished playing
        if (!audioSource.isPlaying && audioSource.time > 0f)
        {
            QuitGame();
        }
    }

    void QuitGame()
    {
        Debug.Log("Audio finished. Quitting game...");
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
#else
            Application.Quit(); // Quit the built game
#endif
    }
}