using UnityEngine;

[RequireComponent(typeof(AudioSource))] // Ensures AudioSource is attached
public class FlashlightToggle : MonoBehaviour
{
    public Light flashlight; // Assign your flashlight (Spot Light)
    public AudioClip flashlightOnSound; // Sound for turning on
    public AudioClip flashlightOffSound; // Sound for turning off

    private AudioSource audioSource;
    private bool isOn = false; // Flashlight state

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Ensure the flashlight is assigned
        if (flashlight == null)
        {
            Debug.LogError("FlashlightToggle: No Light assigned!");
        }

        // Ensure the audio clips are assigned
        if (flashlightOnSound == null || flashlightOffSound == null)
        {
            Debug.LogError("FlashlightToggle: Missing flashlight sound effects!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Press 'F' to toggle
        {
            isOn = !isOn;
            if (flashlight) flashlight.enabled = isOn;

            // Play the correct sound
            if (audioSource)
            {
                audioSource.PlayOneShot(isOn ? flashlightOnSound : flashlightOffSound);
            }
        }
    }
}