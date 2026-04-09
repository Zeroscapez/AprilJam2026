using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource audioSource;
    public AudioClip ButtonHover;
    public AudioClip ButtonSelect;
    public AudioClip DamageTaken;
    public AudioClip Gunshot;


    public void Awake()
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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 0.5f);
    }


}
