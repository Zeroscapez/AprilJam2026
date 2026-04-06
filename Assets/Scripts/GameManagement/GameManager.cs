using UnityEngine;
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
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    }
}
public enum GameState
{
    Playing,
    GameOver,
    Debug
}