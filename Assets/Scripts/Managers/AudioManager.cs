using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip win;
    public AudioClip powerupCollect;
    public AudioClip crash;
    public AudioClip starCollect;
    public AudioClip fuelAlert;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayWinSFX()
    {
        audioSource.PlayOneShot(win, 0.5f);
    }

    public void PlayCrashSFX()
    {
        audioSource.PlayOneShot(crash, 0.5f);
    }

    public void PlayStarCollectSFX()
    {
        audioSource.PlayOneShot(starCollect, 0.5f);
    }

    public void PlayFuelAlertSFX()
    {
        audioSource.PlayOneShot(fuelAlert, 0.5f);
    }

    public void PlayPowerUpCollectSFX()
    {
        audioSource.PlayOneShot(powerupCollect, 0.5f);
    }
}
