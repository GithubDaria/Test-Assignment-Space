using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip starClip;
    [SerializeField] private AudioSource starCollected;
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void PlayStarCollectedSound()
    {
        starCollected.PlayOneShot(starClip);
    }
}
