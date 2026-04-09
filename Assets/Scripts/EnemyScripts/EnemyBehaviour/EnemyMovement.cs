using System.Collections;
using UnityEngine;

/// <summary>
/// Base class for all enemy movement types.
/// Handles lifetime, attacking the player, and despawning.
/// </summary>
public abstract class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 2f;
    public float StopDistance = 1f;
    public bool PauseEnabled = false;
    public float PauseDuration = 1f;
    public float PauseInterval = 3f;

    [Header("Lifetime")]
    public float LifeTime = 10f;            // Seconds before the enemy despawns without counting as a kill
    public float AttackAfter = 5f;          // Seconds alive before the enemy attacks the player

    [Header("Attack")]
    public int AttackDamage = 1;            // Damage dealt to the player on attack
    public float AttackDuration = 0.5f;        // Duration of the attack animation/state

    private float _elapsed;
    private bool _hasAttacked;
    private float _pauseTimer;
    private bool _isPaused;

    public bool _isActing;

    public EnemyTarget _target;
    public Transform playerTransform;

    void Awake()
    {
        playerTransform = Camera.main.transform;
        _target = GetComponent<EnemyTarget>();
    }
    void Start()
    {

        if (_target == null)
        {
            Debug.LogError($"[{name}] No EnemyTarget component found! This enemy won't work correctly.");
            return;
        }

        if (_target.enemyData == null)
        {
            Debug.LogWarning($"[{name}] EnemyTarget has no EnemyData assigned! Assigning default values to prevent errors.");
            return;
        }

        _target.enemyData.LifeTime = LifeTime;
        _target.enemyData.AttackAfter = AttackAfter;
        _target.enemyData.AttackDamage = AttackDamage;
    }
    void Update()
    {
        _elapsed += Time.deltaTime;

        // Attack the player once after AttackAfter seconds
        if (!_hasAttacked && _elapsed >= AttackAfter)
        {
            _hasAttacked = true;
            AttackPlayer();
        }

        // Despawn without scoring after LifeTime seconds
        if (_elapsed >= LifeTime)
        {
            Despawn();
            return;
        }

        HandlePause();
    }

    void HandlePause()
    {

        if (_isActing) return;


        if (PauseEnabled)
        {
            _pauseTimer += Time.deltaTime;

            if (_isPaused)
            {
                if (_pauseTimer >= PauseDuration)
                {
                    _isPaused = false;
                    _pauseTimer = 0f;
                }
                return;
            }
            else if (_pauseTimer >= PauseInterval)
            {
                _isPaused = true;
                _pauseTimer = 0f;
                return;
            }
        }

        if (Vector3.Distance(transform.position, playerTransform.position) > StopDistance)
        {
            Move();

        }

    }

    void AttackPlayer()
    {

        PlayerHealth.Instance?.TakeDamage(AttackDamage);
        StartCoroutine(AttackSequence());

    }

    IEnumerator AttackSequence()
    {
        _isActing = true;
        _target.SetAttack();
        yield return new WaitForSeconds(AttackDuration);
        _target.SetAlive();
        _isActing = false;
    }

    void Despawn()
    {
        // Unregister without awarding score
        EnemyManager.Instance?.UnregisterEnemy();
        Destroy(gameObject);
    }

    protected abstract void Move();
}