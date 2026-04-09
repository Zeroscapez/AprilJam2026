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


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreDisplay;
        if (scoreText != null)
        {
            scoreText.text = $"000";
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
            scoreText.text = $"{newScore:000}";
        }
    }


}
