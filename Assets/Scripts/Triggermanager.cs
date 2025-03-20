using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public Collider[] triggerBoxes; // Assign 6 trigger boxes in the inspector
    public GameObject aiObject;     // Assign AI GameObject to activate
    public GameObject[] objectsToActivate;   // Objects to activate
    public GameObject[] objectsToDeactivate; // Objects to deactivate

    private int triggeredCount = 0; // Counter for triggered boxes
    private bool aiActivated = false; // Ensures AI activates only once

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

        if (triggeredCount >= triggerBoxes.Length && !aiActivated)
        {
            aiActivated = true;

            // Activate the AI GameObject
            if (aiObject != null)
            {
                aiObject.SetActive(true);
            }
            else
            {
                Debug.LogError("AI object is not assigned!");
            }

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

            Debug.Log("AI activated and objects toggled.");
        }
    }
}