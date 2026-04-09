using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject startBanner;
    public GameObject readyBanner;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startBanner.gameObject.SetActive(false);
        readyBanner.gameObject.SetActive(true);
    }

    public void ShowBanner()
    {
        startBanner.gameObject.SetActive(true);
        readyBanner.gameObject.SetActive(false);
    }

    public void HideBanner()
    {
        startBanner.gameObject.SetActive(false);
        readyBanner.gameObject.SetActive(false);
    }


}
