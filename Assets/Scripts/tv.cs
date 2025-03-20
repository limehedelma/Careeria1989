using UnityEngine;
using UnityEngine.Video;

public class TVInteraction : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign your VideoPlayer component in the Inspector
    public Transform player; // Assign your player's Transform in the Inspector
    public float interactionDistance = 3f;
    private bool hasPlayed = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= interactionDistance)
        {
            if (!videoPlayer.isPlaying && !hasPlayed)
            {
                videoPlayer.Play();
                hasPlayed = true;
            }
        }
        else
        {
            hasPlayed = false; // Reset when player leaves interaction distance
        }
    }
}