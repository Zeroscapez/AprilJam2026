using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score")]
    public int CurrentScore { get; private set; }

    // Fire this event to notify UI or other systems when score changes

    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreBeaten;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }


    // Call this from an enemy's Die() method, passing in its point value.

    public void AddScore(int points)
    {
        CurrentScore += points;

        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void SubmitScore()
    {
        SaveManager.SaveLastScore(CurrentScore);

        if (CurrentScore > SaveManager.GetHighScore())
        {
            SaveManager.SaveHighScore(CurrentScore);
            OnHighScoreBeaten?.Invoke(CurrentScore);
        }
    }


    public void ResetScore()
    {
        CurrentScore = 0;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}