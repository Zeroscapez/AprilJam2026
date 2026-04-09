using UnityEngine;
using System;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public int EnemiesRemaining { get; private set; }


    public event Action<int> OnEnemyCountChanged;
    public event Action OnAllEnemiesDefeated;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

    }


    public void Start()
    {

    }

    /// <summary>
    /// Call this when an enemy spawns.
    /// </summary>
    public void RegisterEnemy()
    {
        EnemiesRemaining++;
        OnEnemyCountChanged?.Invoke(EnemiesRemaining);
    }

    /// <summary>
    /// Call this when an enemy dies or reaches the player.
    /// </summary>
    public void UnregisterEnemy()
    {
        EnemiesRemaining = Mathf.Max(0, EnemiesRemaining - 1);
        OnEnemyCountChanged?.Invoke(EnemiesRemaining);
        EnemySpawner.Instance.UpdateEnemyCount();
        if (EnemiesRemaining == 0)
            OnAllEnemiesDefeated?.Invoke();
    }


    public void Reset()
    {
        EnemiesRemaining = 0;
        OnEnemyCountChanged?.Invoke(EnemiesRemaining);
    }
}