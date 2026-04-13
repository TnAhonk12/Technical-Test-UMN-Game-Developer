using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;

    public AudioClip[] fishSpawn;
    public AudioClip fishClick;
    public AudioClip trashClick;
    public AudioClip feed;

    private float spawnCooldown = 0f;
    public float spawnDelay = 0.5f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySpawn()
    {
        if (spawnCooldown > 0f)
            return;

        spawnCooldown = spawnDelay;

        PlaySound(fishSpawn[Random.Range(0, fishSpawn.Length)]);
    }

    void Update()
    {
        if (spawnCooldown > 0f)
            spawnCooldown -= Time.deltaTime;
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}