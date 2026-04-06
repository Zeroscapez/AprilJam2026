using UnityEngine;

public static class SaveManager
{
    // Keys for PlayerPrefs
    private const string KEY_HIGH_SCORE = "HighScore";
    private const string KEY_LAST_SCORE = "LastScore";
    private const string KEY_STAGE_TIME = "StageTime";

    // -------------------------
    //  Score
    // -------------------------

    public static int GetHighScore() => PlayerPrefs.GetInt(KEY_HIGH_SCORE, 0);
    public static int GetLastScore() => PlayerPrefs.GetInt(KEY_LAST_SCORE, 0);

    public static void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(KEY_HIGH_SCORE, score);
        Save();
    }

    public static void SaveLastScore(int score)
    {
        PlayerPrefs.SetInt(KEY_LAST_SCORE, score);
        Save();
    }

    // -------------------------
    //  Stage Time
    // -------------------------

    public static float GetStageTime() => PlayerPrefs.GetFloat(KEY_STAGE_TIME, 0f);

    public static void SaveStageTime(float seconds)
    {
        PlayerPrefs.SetFloat(KEY_STAGE_TIME, seconds);
        Save();
    }

    // -------------------------
    //  Persistence
    // -------------------------

    public static void Save()
    {
        PlayerPrefs.Save();
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Save();
    }
}