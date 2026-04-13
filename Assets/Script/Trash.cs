using UnityEngine;

public class Trash : MonoBehaviour
{
    private float currentSpeed;
    private Vector2 targetPosition;

    public float minSpeed;
    public float maxSpeed;

    void Start()
    {
        SetRandomSpeed();
        SetRandomTarget();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            currentSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            SetRandomTarget();
            SetRandomSpeed();
        }
    }

    void SetRandomTarget()
    {
        targetPosition = new Vector2(
            Random.Range(-8f, 8f),
            Random.Range(-4f, 4f)
        );
    }

    void SetRandomSpeed()
    {
        currentSpeed = Random.Range(minSpeed,maxSpeed);
    }
}