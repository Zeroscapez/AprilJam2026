using UnityEngine;

public class EnemyZigzagMover : EnemyMovement
{
    public float Frequency = 1f; // Frequency of the zigzag movement
    public float Amplitude = 1f; // Amplitude of the zigzag movement
    protected override void Move()
    {
        float x = Mathf.Sin(Time.time * Frequency) * Amplitude;
        transform.position += new Vector3(x, 0, -Speed) * Time.deltaTime;
    }


}
