using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{
    public GameObject objectToEnable; // Assign the object in the inspector

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger: " + other.gameObject.name); // Log what enters

        if (other.CompareTag("Player")) // Ensure only the player triggers it
        {
            Debug.Log("Player detected! Attempting to enable object...");

            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true); // Enable the object
                Debug.Log(objectToEnable.name + " has been enabled!");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("objectToEnable is not assigned!");
            }
        }
    }
}