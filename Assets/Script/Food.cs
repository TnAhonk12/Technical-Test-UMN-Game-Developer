using UnityEngine;

public class Food : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float bottomY = -5f;

    void Start()
    {
        gameObject.tag = "Food";
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        
        if (transform.position.y <= bottomY)
        {
            Destroy(gameObject);
        }
    }
}