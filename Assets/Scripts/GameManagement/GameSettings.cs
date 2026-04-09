using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}