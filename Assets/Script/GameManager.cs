using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public int maxFood = 7;
    public float spawnCooldown = 1f;

    private float cooldownTimer = 0f;

    private int spawnedFoodCount = 0;
    private bool isCooldownActive = false;

    void Update()
    {
        if (PauseManager.Instance.isPaused)
            return;

        if (isCooldownActive)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                isCooldownActive = false;
                spawnedFoodCount = 0;

                Debug.Log("Food system reset!");
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            HandleClick(mousePos);
        }
    }

    void HandleClick(Vector2 mousePos)
    {
        int layerMask = LayerMask.GetMask("GameObject");

        Collider2D hit = Physics2D.OverlapPoint(mousePos, layerMask);

        if (hit != null)
        {
            GameObject clickedObj = hit.gameObject;

            if (clickedObj.CompareTag("Trash"))
            {
                AudioManager.Instance.PlaySound(AudioManager.Instance.trashClick);
                Destroy(clickedObj);
                return;
            }

            if (clickedObj.CompareTag("Fish"))
            {
                Fish fish = clickedObj.GetComponent<Fish>();
                if (fish != null)
                {
                    fish.TriggerFlee(mousePos);
                }
                return;
            }
        }

        SpawnFood(mousePos);
    }

    void SpawnFood(Vector2 mousePos)
    {
      
        if (isCooldownActive)
            return;

        if (spawnedFoodCount >= maxFood)
        {
            isCooldownActive = true;
            cooldownTimer = spawnCooldown;

            Debug.Log("Cooldown started!");
            return;
        }

        Instantiate(foodPrefab, mousePos, Quaternion.identity);

        AudioManager.Instance.PlaySound(AudioManager.Instance.feed);

        spawnedFoodCount++;
    }
}