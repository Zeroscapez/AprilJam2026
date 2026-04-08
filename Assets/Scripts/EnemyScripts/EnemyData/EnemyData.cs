using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public int MaxHealth;
    public float LifeTime;
    public float AttackAfter;
    public int AttackDamage;

    [Header("Sprites")]
    public Sprite AliveSprite;
    public Sprite AttackSprite;
    public Sprite DeathSprite;
}

