using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [Header("Timeline")]
    public StageTimeline Timeline;

    [Header("Organisation")]
    public Transform EnemyContainer;    // Drag an empty GameObject here to keep the hierarchy clean
    private List<SpawnEvent> _remaining = new List<SpawnEvent>();
    private float _elapsed;
    private bool _running;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {

    }

    public void StartTimeline()
    {
        if (Timeline == null)
        {
            Debug.LogWarning("[EnemySpawner] No StageTimeline assigned.");
            return;
        }

        _remaining = new List<SpawnEvent>(Timeline.SpawnEvents);
        _remaining.Sort((a, b) => a.Time.CompareTo(b.Time));

        _elapsed = 0f;
        _running = true;
    }

    public void StopTimeline()
    {
        _running = false;
    }

    void Update()
    {
        if (!_running || _remaining.Count == 0) return;

        _elapsed += Time.deltaTime;

        while (_remaining.Count > 0 && _elapsed >= _remaining[0].Time)
        {
            SpawnEnemy(_remaining[0]);
            _remaining.RemoveAt(0);
        }
    }

    void SpawnEnemy(SpawnEvent spawnEvent)
    {
        if (spawnEvent.EnemyPrefab == null)
        {
            Debug.LogWarning("[EnemySpawner] SpawnEvent has no prefab assigned.");
            return;
        }

        GameObject enemy = Instantiate(spawnEvent.EnemyPrefab, spawnEvent.SpawnPosition, Quaternion.identity);

        if (EnemyContainer != null)
            enemy.transform.SetParent(EnemyContainer);
        EnemyManager.Instance?.RegisterEnemy();

        //  Debug.Log($"[EnemySpawner] Spawned {spawnEvent.EnemyPrefab.name} at t={_elapsed:F1}s");
    }

    // Draws spawn positions in the Scene view so you don't need empty GameObjects
    void OnDrawGizmos()
    {
        if (Timeline == null) return;

        foreach (SpawnEvent e in Timeline.SpawnEvents)
        {
            if (e.EnemyPrefab == null) continue;

            // Sphere at the spawn position
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(e.SpawnPosition, 0.5f);

            // Label with prefab name and time (visible in Scene view)
#if UNITY_EDITOR
            UnityEditor.Handles.Label(e.SpawnPosition + Vector3.up, $"{e.EnemyPrefab.name}\nt={e.Time}s");
#endif
        }
    }
}