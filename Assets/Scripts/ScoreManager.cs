using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score")]
    [SerializeField] private int currentScore = 0;

    // Fire this event to notify UI or other systems when score changes
    public UnityEvent<int> OnScoreChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }

    /// <summary>
    /// Call this from an enemy's Die() method, passing in its point value.
    /// </summary>
    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"[ScoreManager] +{points} pts → Total: {currentScore}");
        OnScoreChanged?.Invoke(currentScore);
    }

    public int GetScore() => currentScore;

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }
}