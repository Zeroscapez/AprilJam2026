using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState GAME = GameState.Playing;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene") return;

        // Re-subscribe since PlayerHealth is a new instance each scene load
        if (PlayerHealth.Instance != null)
            PlayerHealth.Instance.OnPlayerDied += Stop;

        GAME = GameState.Playing;
        StopAllCoroutines();
        StartCoroutine(GameStartSequence());
    }

    IEnumerator GameStartSequence()
    {
        yield return new WaitForSecondsRealtime(3f);
        UIManager.Instance.ShowBanner();
        yield return new WaitForSecondsRealtime(1f);
        UIManager.Instance.HideBanner();
        StartGame();
    }

    public void StartGame()
    {
        UIStageTimer.Instance.StartTimer();
        EnemySpawner.Instance.StartTimeline();
    }

    IEnumerator GameOverSequence()
    {
        yield return new WaitForSecondsRealtime(3f);
        UIManager.Instance.ShowTimeBanner();
    }

    public void Stop()
    {
        if (GAME != GameState.Playing) return;
        StartCoroutine(GameOverSequence());
        GAME = GameState.GameOver;
        StopAllCoroutines();

        EnemySpawner.Instance.StopTimeline();
        UIStageTimer.Instance.StopTimer();
        ScoreManager.Instance.SubmitScore();
        SceneManager.LoadScene("MainMenuScene");
    }
}

public enum GameState
{
    Playing,
    GameOver,
    Debug
}