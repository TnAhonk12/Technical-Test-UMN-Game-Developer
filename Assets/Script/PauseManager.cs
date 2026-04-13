using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public GameObject pauseMenu;
    public CanvasGroup canvasGroup;

    public bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        canvasGroup.alpha = 0;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        canvasGroup.DOFade(1f, 0.3f).SetUpdate(true);

        pauseMenu.transform.localScale = Vector3.zero;
        pauseMenu.transform.DOScale(1f, 0.3f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void Resume()
    {
        isPaused = false;

        canvasGroup.DOFade(0f, 0.2f).SetUpdate(true);

        pauseMenu.transform.DOScale(0f, 0.2f)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            });
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}