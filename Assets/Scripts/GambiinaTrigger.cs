using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambiinaTrigger : MonoBehaviour
{
    public VoicelinePlayer voicelinePlayer; // Reference to VoicelinePlayer
    public int voicelineIndex = 0; // Index of the voiceline to play
    private Collider triggerCollider;
    private static HashSet<int> playedVoicelines = new HashSet<int>(); // Track played voicelines

    void Start()
    {
        if (voicelinePlayer == null)
        {
            voicelinePlayer = FindObjectOfType<VoicelinePlayer>(); // Auto-find if not assigned
        }
        triggerCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player triggering
        {
            if (voicelinePlayer != null && !playedVoicelines.Contains(voicelineIndex))
            {
                voicelinePlayer.PlayVoiceline(voicelineIndex);
                playedVoicelines.Add(voicelineIndex); // Mark voiceline as played
                StartCoroutine(DestroyTriggerAfterAudio(voicelinePlayer.voicelines[voicelineIndex].length));
            }
        }
    }

    private IEnumerator DestroyTriggerAfterAudio(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (triggerCollider != null)
        {
            Destroy(triggerCollider); // Destroy the collider after audio plays
        }
    }
}