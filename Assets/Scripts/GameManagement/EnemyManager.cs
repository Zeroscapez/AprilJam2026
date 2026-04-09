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
        StartCoroutine(GameStartSequence());
    }

    public void StartGame()
    {
        EnemySpawner.Instance.StartTimeline();
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

        if (EnemiesRemaining == 0)
            OnAllEnemiesDefeated?.Invoke();
    }

    IEnumerator GameStartSequence()
    {
        // You can add any pre-game animations or effects here
        yield return new WaitForSeconds(5f); // Example delay before starting the game

        StartGame();
    }

    public void Reset()
    {
        EnemiesRemaining = 0;
        OnEnemyCountChanged?.Invoke(EnemiesRemaining);
    }
}