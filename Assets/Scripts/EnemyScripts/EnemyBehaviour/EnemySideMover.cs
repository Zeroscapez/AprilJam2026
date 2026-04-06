using UnityEngine;

public class EnemySideMover : EnemyMovement
{
    public float TravelDistance = 5f;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    protected override void Move()
    {
        float x = Mathf.Sin(Time.time * Speed) * TravelDistance;
        transform.position = new Vector3(_startPosition.x + x, transform.position.y, transform.position.z);
    }
}
