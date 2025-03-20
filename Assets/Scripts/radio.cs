using UnityEngine;

public class RadioInteraction : MonoBehaviour
{
    public AudioSource radioAudio;
    public Transform player; // Assign your player's Transform in the Inspector
    public float interactionDistance = 3f;
    private bool hasPlayed = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= interactionDistance)
        {
            if (!radioAudio.isPlaying && !hasPlayed)
            {
                radioAudio.PlayOneShot(radioAudio.clip);
                hasPlayed = true;
            }
        }
        else
        {
            hasPlayed = false; // Reset when player leaves interaction distance
        }
    }
}