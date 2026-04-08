using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIScoreTracker : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (scoreText == null)
        {
            Debug.LogError("UIScoreTracker requires a TextMeshProUGUI component.");
        }

        ScoreManager.Instance.OnScoreChanged += UpdateScoreDisplay;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: 0";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScoreDisplay(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {newScore}";
        }
    }

}
