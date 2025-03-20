using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public Collider[] triggerBoxes; // Assign 6 trigger boxes in the inspector
    public GameObject aiPrefab;     // Assign AI prefab to spawn
    public Transform spawnPoint;    // Where the AI should spawn
    public GameObject[] objectsToActivate;   // Objects to activate
    public GameObject[] objectsToDeactivate; // Objects to deactivate

    private int triggeredCount = 0; // Counter for triggered boxes
    private bool aiSpawned = false; // Ensures AI spawns only once

    private void Start()
    {
        // Ensure all trigger boxes have a TriggerBox script attached
        foreach (Collider col in triggerBoxes)
        {
            TriggerBox triggerBox = col.gameObject.GetComponent<TriggerBox>();
            if (triggerBox != null)
            {
                triggerBox.OnTriggered += HandleTriggerActivated;
            }
            else
            {
                Debug.LogError("A trigger box is missing the TriggerBox script!");
            }
        }
    }

    private void HandleTriggerActivated()
    {
        triggeredCount++;

        if (triggeredCount >= triggerBoxes.Length && !aiSpawned)
        {
            aiSpawned = true;

            // Spawn the AI
            Instantiate(aiPrefab, spawnPoint.position, spawnPoint.rotation);

            // Activate objects
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }

            // Deactivate objects
            foreach (GameObject obj in objectsToDeactivate)
            {
                obj.SetActive(false);
            }

            Debug.Log("AI spawned and objects toggled.");
        }
    }
}