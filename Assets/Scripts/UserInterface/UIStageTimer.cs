using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIStageTimer : MonoBehaviour
{
    public static UIStageTimer Instance { get; private set; }
    public TextMeshProUGUI TimerText;

    public event Action<float> OnTimerStop;

    public float ElapsedTime { get; private set; }
    public bool IsRunning { get; private set; }






    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsRunning)
        {
            return;
        }

        ElapsedTime += Time.deltaTime;

        if (TimerText != null)
        {
            TimerText.text = $"{FormatTime(ElapsedTime)}";
        }

        if (ElapsedTime >= 60f)
        {
            GameManager.Instance.Stop();
        }



    }

    public void StartTimer()
    {
        ElapsedTime = 0f;
        IsRunning = true;
    }

    public void StopTimer()
    {
        if (!IsRunning)
        {
            return;
        }

        IsRunning = false;
        SaveManager.SaveStageTime(ElapsedTime);
        OnTimerStop?.Invoke(ElapsedTime);
    }


    // Formats as MM:SS e.g. "01:23"
    public static string FormatTime(float seconds)
    {
        int m = Mathf.FloorToInt(seconds / 60);
        int s = Mathf.FloorToInt(seconds % 60);
        return $"{s:000}";
    }
}
