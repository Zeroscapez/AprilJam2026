using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject startBanner;
    public GameObject readyBanner;
    public GameObject timeBanner;


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
        timeBanner.gameObject.SetActive(false);
        startBanner.gameObject.SetActive(false);
        readyBanner.gameObject.SetActive(true);

        Debug.Log("UIManager initialized. Start Banner hidden, Ready Banner shown.");
    }

    public void ShowBanner()
    {
        startBanner.gameObject.SetActive(true);
        readyBanner.gameObject.SetActive(false);
        Debug.Log("Start Banner shown, Ready Banner hidden.");
    }

    public void HideBanner()
    {
        startBanner.gameObject.SetActive(false);
        readyBanner.gameObject.SetActive(false);
        Debug.Log("Banners hidden.");
    }

    public void ShowTimeBanner()
    {
        timeBanner.gameObject.SetActive(true);
    }


}
