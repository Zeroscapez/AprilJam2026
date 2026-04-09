using System;
using System.Xml.Schema;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class UIEnemyCountTracker : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemySpawner.OnTimelineComplete += EnemyCount;
        EnemySpawner.OnEnemyCountUpdated += UpdateEnemyCount;
    }

    void EnemyCount()
    {

        if (enemyCountText != null)
        {
            enemyCountText.text = $"{EnemySpawner.Instance.totalEnemies:000}";
        }
    }

    void OnEnable()
    {


    }
    void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateEnemyCount(int count)
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = $"{count:000}";
        }
    }
}