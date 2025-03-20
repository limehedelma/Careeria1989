using UnityEngine;

public class RadioInteraction : MonoBehaviour
{
    public AudioSource radioAudio;
    public Transform player; // Assign your player's Transform in the Inspector
    public float interactionDistance = 3f; // Distance to start playing
    public float playingDistance = 6f; // Distance beyond which it pauses
    private bool hasPlayed = false;
    private bool isPaused = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (!hasPlayed && distance <= interactionDistance)
        {
            radioAudio.Play();
            hasPlayed = true; // Ensure it plays only once
            isPaused = false;
        }
        
        if (hasPlayed && distance > playingDistance && radioAudio.isPlaying)
        {
            radioAudio.Pause(); // Pause the radio instead of stopping
            isPaused = true;
        }
        
        if (isPaused && distance <= interactionDistance)
        {
            radioAudio.UnPause(); // Resume playback if player returns
            isPaused = false;
        }
    }
}