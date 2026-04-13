using System.IO;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static GameConfig config;

    string configPath;

    void Awake()
    {
#if UNITY_EDITOR
        configPath = Application.dataPath + "/config.json";
#else
    configPath = Application.dataPath + "/../config.json";
#endif

        if (!File.Exists(configPath))
        {
            CreateDefaultConfig();
        }

        LoadConfig();
    }

    void LoadConfig()
    {
        string json = File.ReadAllText(configPath);
        config = JsonUtility.FromJson<GameConfig>(json);

        Debug.Log("Config Loaded!");
    }

    public static FishConfig GetFishConfig(string type)
    {
        type = type.ToUpper();

        foreach (var f in config.fishConfigs)
        {
            if (f.type.ToUpper() == type)
                return f;
        }

        return null;
    }

    public static TrashConfig GetTrashConfig(string type)
    {
        type = type.ToUpper();

        foreach (var t in config.trashConfigs)
        {
            if (t.type.ToUpper() == type)
                return t;
        }

        return null;
    }

    void CreateDefaultConfig()
    {
        GameConfig defaultConfig = new GameConfig()
        {
            fishMinSpeed = 1f,
            fishMaxSpeed = 3f,
            fishSize = 0.5f,

            trashMinSpeed = 0.5f,
            trashMaxSpeed = 1.5f,
            trashSize = 2.5f,

            hungerDecreaseRate = 5f,
            hungerCooldown = 3f,
            detectionRadius = 3f,

            fishConfigs = new FishConfig[]
            {
            new FishConfig { type = "ANGELFISH", minSpeed = 2f, maxSpeed = 4f, size = 0.5f },
            new FishConfig { type = "COD", minSpeed = 1.5f, maxSpeed = 2.5f, size = 0.6f },
            new FishConfig { type = "POMFRET", minSpeed = 1.2f, maxSpeed = 2f, size = 0.5f },
            new FishConfig { type = "SEAHORSE", minSpeed = 0.8f, maxSpeed = 1.5f, size = 0.7f },
            new FishConfig { type = "STARFISH", minSpeed = 0.3f, maxSpeed = 0.8f, size = 0.5f }
            },

            trashConfigs = new TrashConfig[]
            {
            new TrashConfig { type = "CHIPS", minSpeed = 0.5f, maxSpeed = 1f, size = 0.3f },
            new TrashConfig { type = "LOG", minSpeed = 0.2f, maxSpeed = 0.6f, size = 0.5f }
            }
        };

        string json = JsonUtility.ToJson(defaultConfig, true);
        File.WriteAllText(configPath, json);

        Debug.Log("Default config created at: " + configPath);
    }
}