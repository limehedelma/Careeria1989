using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public delegate void TriggerAction();
    public event TriggerAction OnTriggered;
    private bool isTriggered = false; // Ensure it only triggers once

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player")) // Ensure it's triggered by the player
        {
            isTriggered = true;
            OnTriggered?.Invoke(); // Notify the TriggerManager
            Debug.Log(gameObject.name + " triggered!");
        }
    }
}

