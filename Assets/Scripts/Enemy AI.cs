using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreepyNavAgent : MonoBehaviour
{
    public VideoPlayer videoSource;
    public AudioSource audioSource;
    public NavMeshAgent agent;
    public Transform player;
    public AudioSource noiseOnAwake;
    public AudioSource activeNoise;
    public float minTeleportRange = 2f;
    public float maxTeleportRange = 10f;
    public string jumpscareSceneName = "JumpscareScene";
    public Collider triggerBox;
    public float stillnessThreshold = 15f;

    public VoicelinePlayer voicelinePlayer;
    public float voicelineDelay = 2f;

    private Vector3 lastPlayerPosition;
    private float stillTime = 0f;
    private bool jumpscareTriggered = false;
    private bool hasPlayedActiveNoiseVoiceline = false;

    void Awake()
    {
        if (videoSource != null) videoSource.Stop();
        if (audioSource != null) audioSource.Stop();
        if (noiseOnAwake != null)
        {
            noiseOnAwake.Play();
            StartCoroutine(WaitForAwakeNoise());
        }
        lastPlayerPosition = player.position;
        StartCoroutine(RandomTeleport());
    }

    void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (jumpscareTriggered) return;

        agent.SetDestination(player.position);

        if (!noiseOnAwake.isPlaying && !activeNoise.isPlaying)
        {
            activeNoise.Play();

            // Ensure the voiceline only plays once
            if (!hasPlayedActiveNoiseVoiceline)
            {
                hasPlayedActiveNoiseVoiceline = true;
                StartCoroutine(PlayVoicelineAfterDelay());
            }
        }

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
            stillTime = 0f;
        }
        lastPlayerPosition = player.position;
    }

    IEnumerator RandomTeleport()
    {
        while (!jumpscareTriggered)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));

            Vector3 randomOffset;
            do
            {
                Vector3 behindDirection = -player.forward.normalized;
                float distance = Random.Range(minTeleportRange, maxTeleportRange);
                randomOffset = behindDirection * distance;
                randomOffset.y = 0;
            } while (randomOffset.magnitude < minTeleportRange);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(player.position + randomOffset, out hit, maxTeleportRange, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
        }
    }

    private void TriggerJumpscare()
    {
        if (jumpscareTriggered) return;

        jumpscareTriggered = true;
        Debug.Log("Jumpscare Triggered! Loading jumpscare scene...");
        SceneManager.LoadScene(jumpscareSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == triggerBox && !jumpscareTriggered)
        {
            TriggerJumpscare();
        }
    }

    IEnumerator WaitForAwakeNoise()
    {
        yield return new WaitWhile(() => noiseOnAwake.isPlaying);

        if (voicelinePlayer != null)
        {
            voicelinePlayer.PlayVoiceline(0);
            yield return new WaitForSeconds(voicelinePlayer.voicelines[0].length);
        }

        if (activeNoise != null)
        {
            activeNoise.Play();

            // Make sure the voiceline only plays once
            if (!hasPlayedActiveNoiseVoiceline)
            {
                hasPlayedActiveNoiseVoiceline = true;
                StartCoroutine(PlayVoicelineAfterDelay());
            }
        }
    }

    IEnumerator PlayVoicelineAfterDelay()
    {
        yield return new WaitForSeconds(voicelineDelay);

        if (voicelinePlayer != null)
        {
            voicelinePlayer.PlayVoiceline(1);
        }
    }
}
