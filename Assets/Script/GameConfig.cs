[System.Serializable]
public class FishConfig
{
    public string type;
    public float minSpeed;
    public float maxSpeed;
    public float size;
}

[System.Serializable]
public class TrashConfig
{
    public string type;
    public float minSpeed;
    public float maxSpeed;
    public float size;
}

[System.Serializable]
public class GameConfig
{
   
    public float fishMinSpeed;
    public float fishMaxSpeed;
    public float fishSize;

    public float trashMinSpeed;
    public float trashMaxSpeed;
    public float trashSize;

    public float hungerDecreaseRate;
    public float hungerCooldown;
    public float detectionRadius;

  
    public FishConfig[] fishConfigs;
    public TrashConfig[] trashConfigs;
}