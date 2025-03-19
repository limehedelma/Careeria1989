using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CreepyNavAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public AudioSource noiseOnAwake;
    public AudioSource activeNoise;
    public float minTeleportRange = 2f; // Minimum distance from player
    public float maxTeleportRange = 10f; // Maximum distance from player
    public float stillTimeThreshold = 15f;
    public float boxTriggerTime = 10f;
    public GameObject jumpscareObject;

    private Vector3 lastPlayerPosition;
    private float stillTime = 0f;
    private bool isPlayerStill = false;
    private bool boxTriggered = false;

    void Awake()
    {
        if (noiseOnAwake) noiseOnAwake.Play();
        lastPlayerPosition = player.position;
        StartCoroutine(RandomTeleport());
        
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

}
