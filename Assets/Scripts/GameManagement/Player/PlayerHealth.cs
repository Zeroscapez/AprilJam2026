using System;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
    [Header("Health")]
    public int MaxHealth = 3;

    public int CurrentHealth { get; private set; }

    public event Action<int> OnHealthChanged;
    public event Action OnPlayerDied;


    void Awake()
    {
        Instance = this;
        CurrentHealth = MaxHealth;
    }
    void Start()
    {

    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

        OnHealthChanged?.Invoke(CurrentHealth);
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance.DamageTaken);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., show game over screen, restart level, etc.)
        OnPlayerDied?.Invoke();
        Debug.Log("Player has died!");
    }
}