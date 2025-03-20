using UnityEngine;
using UnityEngine.Video;

public class TVInteraction : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign your VideoPlayer component in the Inspector
    public Transform player; // Assign your player's Transform in the Inspector
    public float interactionDistance = 3f; // Distance to start playing
    public float playingDistance = 6f; // Distance beyond which it pauses
    private bool hasStarted = false;
    public AudioSource audioSource; // Assign in Inspector
    public float floorHeightThreshold = 1f; // Acceptable Y-axis height difference

    void Update()
    {
        // Ensure the player is on the same floor by checking Y-axis height difference
        if (Mathf.Abs(transform.position.y - player.position.y) > floorHeightThreshold)
        {
            return; // Exit if player is on a different floor
        }

        // Calculate distance using both X and Z coordinates
        float distance = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.z),
            new Vector2(player.position.x, player.position.z)
        );

        if (!hasStarted && distance <= interactionDistance)
        {
            if (audioSource != null) audioSource.Stop();
            videoPlayer.Play();
            hasStarted = true; // Ensure it plays only once
        }

        if (hasStarted)
        {
            if (distance > playingDistance && videoPlayer.isPlaying)
            {
                videoPlayer.Pause(); // Pause when player leaves the playingDistance
            }
            else if (distance <= playingDistance && !videoPlayer.isPlaying)
            {
                if (audioSource != null) audioSource.Stop();
                videoPlayer.Play(); // Resume when player re-enters the playingDistance
            }
        }
    }
}