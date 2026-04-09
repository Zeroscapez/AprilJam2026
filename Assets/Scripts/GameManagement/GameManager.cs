using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState GAME = GameState.Playing;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerHealth.Instance.OnPlayerDied += Stop;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        StartCoroutine(GameStartSequence());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GameStartSequence()
    {
        // You can add any pre-game animations or effects here
        yield return new WaitForSeconds(3f); // Example delay before starting the game
        UIManager.Instance.ShowBanner();
        yield return new WaitForSeconds(1f);
        UIManager.Instance.HideBanner();
        StartGame();
    }


    public void StartGame()
    {
        UIStageTimer.Instance.StartTimer();
        EnemySpawner.Instance.StartTimeline();
    }
    public void Stop()
    {
        if (GAME != GameState.Playing)
        {
            return;
        }

        GAME = GameState.GameOver;

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