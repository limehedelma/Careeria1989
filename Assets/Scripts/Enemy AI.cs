using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreepyNavAgent : MonoBehaviour
{
    public VideoPlayer videoSource; // Assign in Inspector
    public AudioSource audioSource; // Assign in Inspector
    public NavMeshAgent agent;
    public Transform player;
    public AudioSource noiseOnAwake;
    public AudioSource activeNoise;
    public float minTeleportRange = 2f; // Minimum distance from player
    public float maxTeleportRange = 10f; // Maximum distance from player
    public string jumpscareSceneName = "JumpscareScene"; // Assign jumpscare scene name in inspector
    public Collider triggerBox; // Assign a specific trigger box in the inspector
    public float stillnessThreshold = 15f; // Time before jumpscare when standing still
    public float triggerTimeoutThreshold = 60f; // Time before jumpscare if trigger isn't hit

    private Vector3 lastPlayerPosition;
    private float stillTime = 0f;
    private float triggerTimeout = 0f;

    void Awake()
    {
        if (videoSource != null) videoSource.Stop();
        if (audioSource != null) audioSource.Stop();
        if (noiseOnAwake) noiseOnAwake.Play();
        lastPlayerPosition = player.position;
        StartCoroutine(RandomTeleport());
        StartCoroutine(TriggerTimeoutCheck());
    }

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(player.position);

        if (!noiseOnAwake.isPlaying && !activeNoise.isPlaying)
        {
            activeNoise.Play();
        }

        // Check if player is still
        if (Vector3.Distance(player.position, lastPlayerPosition) < 0.1f)
        {
            stillTime += Time.deltaTime;
            if (stillTime >= stillnessThreshold)
            {
                TriggerJumpscare();
            }
        }
        else
        {
            stillTime = 0f; // Reset timer if player moves
        }
        lastPlayerPosition = player.position;
    }

    IEnumerator RandomTeleport()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));

            Vector3 randomOffset;
            do
            {
                // Get the direction behind the player
                Vector3 behindDirection = -player.forward.normalized;

                // Randomize distance within min/max range
                float distance = Random.Range(minTeleportRange, maxTeleportRange);
                randomOffset = behindDirection * distance;
                randomOffset.y = 0;
            } while (randomOffset.magnitude < minTeleportRange); // Ensure it's outside min range

            NavMeshHit hit;
            if (NavMesh.SamplePosition(player.position + randomOffset, out hit, maxTeleportRange, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
        }
    }

    IEnumerator TriggerTimeoutCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            triggerTimeout += 1f;

            if (triggerBox != null && triggerBox.bounds.Contains(player.position))
            {
                triggerTimeout = 0f; // Reset if player is inside trigger
            }

            if (triggerTimeout >= triggerTimeoutThreshold) // Customizable timeout
            {
                TriggerJumpscare();
            }
        }
    }

    private void TriggerJumpscare()
    {
        Debug.Log("Jumpscare Triggered! Loading jumpscare scene...");
        SceneManager.LoadScene(jumpscareSceneName);
    }
}
