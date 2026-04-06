using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    [Header("Base Settings")]
    public float Speed = 2f;
    public float StopDistance = 1f;
    public bool PauseEnabled = false;
    public float PauseDuration = 1f;
    public float PauseInterval = 3f;

    private float _pauseTimer;
    private bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        if (PauseEnabled)
        {
            _pauseTimer += Time.deltaTime;

            if (isPaused)
            {
                if (_pauseTimer >= PauseDuration)
                {
                    isPaused = false;
                    _pauseTimer = 0f;
                }
                return; // Skip movement while paused
            }
            else if (_pauseTimer >= PauseInterval)
            {
                isPaused = true;
                _pauseTimer = 0f;
                return;
            }
        }

        if (Vector3.Distance(transform.position, Vector3.zero) > StopDistance)
            Move();
    }

    protected abstract void Move();
}
