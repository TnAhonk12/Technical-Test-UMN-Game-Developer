using DG.Tweening;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Tween moveTween;
    
    public float hunger = 100f;

    private float currentSpeed;
    private Vector2 targetPosition;

    private enum State { Swim, SeekFood, Flee }
    private State currentState;

    private float cooldownTimer;

    public float minSpeed;
    public float maxSpeed;

    private SpriteRenderer sr;

    void Start()
    {
        SetRandomSpeed();
        SetRandomTarget();
        currentState = State.Swim;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleHunger();
        UpdateColor();

        switch (currentState)
        {
            case State.Swim:
                Swim();
                if (hunger <= 0)
                {
                    currentState = State.SeekFood;
                }
                break;

            case State.SeekFood:
                SeekFood();
                break;

            case State.Flee:
                Flee();
                break;
        }
    }

    void UpdateColor()
    {
        float t = hunger / 100f;

        Color color = Color.Lerp(Color.yellow, Color.white, t);

        sr.color = color;
    }

    void HandleHunger()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        hunger -= ConfigManager.config.hungerDecreaseRate * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0, 100);
    }

   
    void Swim()
    {
        MoveTo(targetPosition);

        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            SetRandomTarget();
        }
    }

    void SeekFood()
    {
        GameObject food = FindClosestFood();

        if (food == null)
        {
            currentState = State.Swim;
            return;
        }

        MoveTo(food.transform.position);

        if (Vector2.Distance(transform.position, food.transform.position) < 0.3f)
        {
            Destroy(food);
            hunger = 100;
            cooldownTimer = ConfigManager.config.hungerCooldown;
            currentState = State.Swim;
        }
    }

    GameObject FindClosestFood()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");

        float minDist = Mathf.Infinity;
        GameObject closest = null;

        foreach (var f in foods)
        {
            float dist = Vector2.Distance(transform.position, f.transform.position);

            if (dist < ConfigManager.config.detectionRadius && dist < minDist)
            {
                minDist = dist;
                closest = f;
            }
        }

        return closest;
    }

    void Flee()
    {
        MoveTo(targetPosition);

        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            currentState = State.Swim;
        }
    }

    public void TriggerFlee(Vector2 dangerPos)
    {
        Vector2 dir = (Vector2)transform.position - dangerPos;
        targetPosition = (Vector2)transform.position + dir.normalized * 3f;
        AudioManager.Instance.PlaySound(AudioManager.Instance.fishClick);
        currentState = State.Flee;
    }


    void MoveTo(Vector2 target)
    {
        moveTween?.Kill();

        float distance = Vector2.Distance(transform.position, target);

        float duration = distance / currentSpeed; 

        moveTween = transform.DOMove(target, duration)
            .SetEase(Ease.Linear);

        Vector2 dir = target - (Vector2)transform.position;

        if (dir.x != 0)
        {
            Vector3 scale = transform.localScale;

            scale.x = Mathf.Sign(dir.x) * Mathf.Abs(scale.x);

            transform.localScale = scale;
        }
    }

    void SetRandomTarget()
    {
        targetPosition = new Vector2(
            Random.Range(-8f, 8f),
            Random.Range(-4f, 4f)
        );

        MoveTo(targetPosition);
    }

    void SetRandomSpeed()
    {
        currentSpeed = Random.Range(minSpeed,maxSpeed);
    }
    void OnMouseDown()
    {
        TriggerFlee(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}