using System;
using System.Collections;
using UnityEngine;

public class EnemyTarget : MonoBehaviour, IShootable
{

    public EnemyData enemyData;
    public int CurrentEnemyHealth;
    private string enemyName;
    private int maxEnemyHealth;

    [Header("VFX / Feedback")]
    public Color FlashColor = Color.red;
    public float FlashDuration = 0.1f;
    private Material _material;
    private Color _originalColor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        if (_material)
        {
            _originalColor = _material.color;
        }


        if (enemyData == null)
        {
            Debug.LogError("EnemyData not assigned on " + gameObject.name);
            Debug.LogError("Assigning default values to prevent errors.");

            enemyName = "Default Enemy";
            maxEnemyHealth = 5;
            CurrentEnemyHealth = maxEnemyHealth;
            return;
        }
        enemyName = enemyData.EnemyName;
        maxEnemyHealth = enemyData.MaxHealth;
        CurrentEnemyHealth = maxEnemyHealth;
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
        ScoreManager.Instance.AddScore(pointValue);
        if (GameManager.Instance.GAME == GameState.Debug)
        {
            Debug.Log(enemyName + " has been defeated!");
            return;
        }
        this.gameObject.SetActive(false);
    }

    IEnumerator Flash()
    {
        _material.color = FlashColor;
        yield return new WaitForSeconds(FlashDuration);
        _material.color = _originalColor;
    }
}
