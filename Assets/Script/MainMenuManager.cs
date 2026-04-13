using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    public string gameSceneName = "Game";

    public CanvasGroup canvasGroup;

    void Start()
    {    
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, 1f);
    }

    public void PlayGame()
    {
        canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
        {
            SceneManager.LoadScene(gameSceneName);
        });
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}