using UnityEngine;

public class EnemyStraightMover : EnemyMovement
{
    protected override void Move()
    {
        float distance = Vector3.Distance(
            transform.position,
            playerTransform.position
        );

        float step = Speed * Time.deltaTime;

        // Clamp movement so we don't overshoot the stop distance
        step = Mathf.Min(step, distance - StopDistance);

        if (step > 0f)
        {
            transform.Translate(Vector3.back * step);
        }
    }
}
