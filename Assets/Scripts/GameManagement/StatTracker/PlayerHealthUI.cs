using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth PlayerHealth;
    public GameObject HeartPrefab; // Heart Prefab with Image component

    public Transform HeartContainer; // Container to hold heart icons

    [Header("Sprites")]
    public Sprite FullHeartSprite; //Full Heart
    public Sprite EmptyHeartSprite; //Empty Heart

    private List<Image> heartImages = new List<Image>();

    public void Awake()
    {
        //PlayerHealth = PlayerHealth.Instance;
    }

    void Start()
    {
        if (PlayerHealth == null)
        {
            Debug.LogWarning("[PlayerHealthUI] No PlayerHealth assigned.");
            PlayerHealth = PlayerHealth.Instance;
            return;
        }

        PlayerHealth.OnHealthChanged += UpdateHearts;

        SpawnHearts(PlayerHealth.MaxHealth);
        UpdateHearts(PlayerHealth.MaxHealth);
    }

    void OnDisable()
    {
        if (PlayerHealth != null)
            PlayerHealth.OnHealthChanged -= UpdateHearts;
    }

    void SpawnHearts(int count)
    {
        foreach (Image heart in heartImages)
        {
            Destroy(heart.gameObject);
        }

        heartImages.Clear();

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(HeartPrefab, HeartContainer);
            Image img = obj.GetComponent<Image>();
            img.sprite = FullHeartSprite; // Add this line
            heartImages.Add(img);
        }
    }

    void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            bool isFull = i < currentHealth;

            if (EmptyHeartSprite != null)
            {
                heartImages[i].sprite = isFull ? FullHeartSprite : EmptyHeartSprite;

            }
            else
                heartImages[i].gameObject.SetActive(isFull); // Just hide if no empty sprite
        }
    }
}