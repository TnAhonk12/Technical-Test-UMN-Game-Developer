using System.IO;
using UnityEngine;
using System;
using System.Globalization;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnFromFile(string fileName, Texture2D tex)
    {
        string fileNameNoExt = Path.GetFileNameWithoutExtension(fileName);
        string[] parts = fileNameNoExt.Split('_');

        if (!fileName.StartsWith("FISH_") && !fileName.StartsWith("TRASH_"))
        {
            Debug.LogWarning("Invalid file prefix: " + fileName);
            return;
        }

        if (parts.Length < 3)
        {
            Debug.LogWarning("Invalid file name: " + fileName);
            return;
        }

        string category = parts[0];
        string type = parts[1];
        string timestamp = parts[2];

        DateTime spawnTime;

        if (DateTime.TryParseExact(
            timestamp,
            "yyyyMMddHHmmss",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out spawnTime))
        {
            Debug.Log("Spawn Time: " + spawnTime);
        }
        else
        {
            Debug.LogWarning("Invalid timestamp: " + timestamp);
        }

        if (category == "FISH")
        {
            SpawnFish(tex, type);
        }
        else if (category == "TRASH")
        {
            SpawnTrash(tex, type);
        }
    }

    void SpawnFish(Texture2D tex, string type)
    {
        GameObject obj = new GameObject("Fish_" + type);

        obj.layer = LayerMask.NameToLayer("GameObject");

        obj.tag = "Fish";

        Vector2 targetPos = GetRandomPosition();

        Vector2 spawnPos = new Vector2(targetPos.x, 6f);

        obj.transform.position = spawnPos;

        AudioManager.Instance.PlaySpawn();

        var sr = obj.AddComponent<SpriteRenderer>();

        sr.sprite = Sprite.Create(tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f));

        FishConfig cfg = ConfigManager.GetFishConfig(type);

        float size = cfg != null ? cfg.size : ConfigManager.config.fishSize;

        obj.transform.localScale = Vector3.one * size;

        obj.AddComponent<PolygonCollider2D>();

        Fish fish = obj.AddComponent<Fish>();

        if (cfg != null)
        {
            fish.minSpeed = cfg.minSpeed;
            fish.maxSpeed = cfg.maxSpeed;
        }
        else
        {
            fish.minSpeed = ConfigManager.config.fishMinSpeed;
            fish.maxSpeed = ConfigManager.config.fishMaxSpeed;
        }
    }

    void SpawnTrash(Texture2D tex, string type)
    {
        GameObject obj = new GameObject("Trash_" + type);

        obj.layer = LayerMask.NameToLayer("GameObject");

        obj.tag = "Trash";

        Vector2 targetPos = GetRandomPosition();

        Vector2 spawnPos = new Vector2(targetPos.x, 3f);

        obj.transform.position = spawnPos;

        AudioManager.Instance.PlaySpawn();

        var sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f));

        TrashConfig cfg = ConfigManager.GetTrashConfig(type);

        float size = cfg != null ? cfg.size : ConfigManager.config.trashSize;

        obj.transform.localScale = Vector3.one * size;

        obj.AddComponent<PolygonCollider2D>();

        Trash trash = obj.AddComponent<Trash>();

        if (cfg != null)
        {
            trash.minSpeed = cfg.minSpeed;
            trash.maxSpeed = cfg.maxSpeed;
        }
        else
        {
            trash.minSpeed = ConfigManager.config.trashMinSpeed;
            trash.maxSpeed = ConfigManager.config.trashMaxSpeed;
        }
    }

    Vector2 GetRandomPosition(float radius = 1f)
    {
        for (int i = 0; i < 20; i++) 
        {
            Vector2 pos = new Vector2(
                Random.Range(-8f, 8f),
                Random.Range(-4f, 4f)
            );

            Collider2D hit = Physics2D.OverlapCircle(pos, radius);

            if (hit == null)
            {
                return pos; 
            }
        }

        return Vector2.zero; 
    }
}