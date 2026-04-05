using UnityEngine;

public class EnemyStraightMover : EnemyMovement
{
    protected override void Move()
    {
        transform.Translate(Vector3.back * Speed * Time.deltaTime);
    }
}
