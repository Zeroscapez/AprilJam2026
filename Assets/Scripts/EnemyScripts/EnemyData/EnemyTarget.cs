using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class EnemyTarget : MonoBehaviour, IShootable
{


    public EnemyData enemyData;
    public int CurrentEnemyHealth;
    private string enemyName;
    private int maxEnemyHealth;
    [SerializeField] private int pointValue;

    [Header("VFX / Feedback")]
    public Color FlashColor = Color.red;
    public float FlashDuration = 0.1f;
    private Material _material;
    private Color _originalColor;

    public float DeathDelay = 0.5f; // Time to wait before destroying the enemy after death

    [Header("Sprites")]
    public Sprite AliveSprite;
    public Sprite AttackSprite;
    public Sprite DeathSprite;

    public SpriteRenderer _renderer { get; private set; }
    private EnemyMovement EnemyMovement;


    void Awake()
    {

        _material = GetComponent<Renderer>().material;
        EnemyMovement = GetComponent<EnemyMovement>();
        _renderer = GetComponent<SpriteRenderer>();

        if (enemyData == null)
        {
            Debug.LogWarning("EnemyData not assigned on " + gameObject.name);
            Debug.LogWarning("Assigning default values to prevent errors.");

            enemyName = "Default Enemy";
            maxEnemyHealth = 2;
            CurrentEnemyHealth = maxEnemyHealth;
            return;
        }
        else
        {
            enemyName = enemyData.EnemyName;
            maxEnemyHealth = enemyData.MaxHealth;
            CurrentEnemyHealth = maxEnemyHealth;
            AliveSprite = enemyData.AliveSprite;
            AttackSprite = enemyData.AttackSprite;
            DeathSprite = enemyData.DeathSprite;
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (_material)
        {
            _originalColor = _material.color;
        }






        SetAlive();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnHit()
    {
        Debug.Log("Enemy hit! = " + enemyName + " Health: " + CurrentEnemyHealth + "/" + maxEnemyHealth);
        CurrentEnemyHealth--;

        if (CurrentEnemyHealth <= 0)
        {
            Die();
        }
        else
        {
            StopAllCoroutines(); // Prevents overlapping flashes if hit rapidly
            StartCoroutine(Flash());
        }
    }

    public void Die()
    {
        EnemyMovement._isActing = true; // Prevents any further actions
        EnemyMovement.enabled = false; // Stop movement immediately
        ScoreManager.Instance.AddScore(pointValue);
        if (GameManager.Instance.GAME == GameState.Debug)
        {
            Debug.Log(enemyName + " has been defeated!");
            return;
        }

        SetDeath();

        StartCoroutine(DestroyAfterDelay()); // Delay to allow death animation/sprite to show

    }

    IEnumerator Flash()
    {
        _material.color = FlashColor;
        yield return new WaitForSeconds(FlashDuration);
        _material.color = _originalColor;
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(DeathDelay);
        this.gameObject.SetActive(false);
    }

    public void SetAlive() => _renderer.sprite = AliveSprite;
    public void SetAttack() => _renderer.sprite = AttackSprite;
    public void SetDeath() => _renderer.sprite = DeathSprite;
}
