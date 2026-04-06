using UnityEngine;

[System.Serializable]
public class SpawnEvent
{
    public float Time; // Time in seconds when the enemy should spawn
    public GameObject EnemyPrefab;
    public Vector3 SpawnPosition;
}