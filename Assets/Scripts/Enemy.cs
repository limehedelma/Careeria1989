using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioEnemy : MonoBehaviour
{
    public AudioSource enemyAudio;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    private float idleTime = 0f;
    private float gameTimer = 0f;
    private Vector3 lastPosition;
    private bool gameOver = false;

    void Start()
    {
        if (enemyAudio == null)
            enemyAudio = GetComponent<AudioSource>();
        lastPosition = transform.position;
        InvokeRepeating("RandomizeAudioDistance", 2f, Random.Range(3f, 8f));
    }

    void Update()
    {
        if (gameOver) return;

        gameTimer += Time.deltaTime;
        
        if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
            idleTime += Time.deltaTime;
        else
            idleTime = 0f;

        lastPosition = transform.position;

        if (idleTime >= 15f)
            TriggerJumpscare("You stood still too long!");

        if (gameTimer >= 120f)
            TriggerJumpscare("You failed to escape!");
    }

    void RandomizeAudioDistance()
    {
        if (gameOver) return;
        float randomDistance = Random.Range(minDistance, maxDistance);
        enemyAudio.spatialBlend = 1f;
        enemyAudio.maxDistance = randomDistance;
        enemyAudio.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            Debug.Log("You escaped!");
            gameOver = true;
           
        }
    }

    void TriggerJumpscare(string message)
    {
        Debug.Log(message);
        gameOver = true;
        // Implement jumpscare animation/sound here
        // Play a jumpscare sound or animation
        
    }
}