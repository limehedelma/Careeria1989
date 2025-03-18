using UnityEngine;

public class RadioInteraction : MonoBehaviour
{
    public AudioSource radioAudio;
    public Transform player; // Assign your player's Transform in the Inspector
    public float interactionDistance = 3f;
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= interactionDistance)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (radioAudio.isPlaying)
                {
                    radioAudio.Pause();
                }
                else
                {
                    radioAudio.Play();
                }
            }
        }
        else
        {
            
        }
    }
}