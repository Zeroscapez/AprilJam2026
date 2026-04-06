using UnityEngine;
using System;

public static class SaveManager
{
    //Keys for PlayerPrefs
    private const string KEY_HIGH_SCORE = "HighScore";
    private const string KEY_LAST_SCORE = "LastScore";

    public static event Action<int> OnHighScoreBeaten;


    //Score Management

    public static int GetHighScore
    {
        get { return PlayerPrefs.GetInt(KEY_HIGH_SCORE, 0); }
    }

    public static int GetLastScore
    {
        get { return PlayerPrefs.GetInt(KEY_LAST_SCORE, 0); }
    }

    public static void SubmitScore(int score)
    {
        PlayerPrefs.SetInt(KEY_LAST_SCORE, score);
        if (score > GetHighScore)
        {
            PlayerPrefs.SetInt(KEY_HIGH_SCORE, score);
            Debug.Log($"New High Score: {score}!");
            OnHighScoreBeaten?.Invoke(score);
        }

        Save(); //Always save after updating scores
    }


    //Saving

    public static void Save()
    {
        PlayerPrefs.Save();
    }
}
