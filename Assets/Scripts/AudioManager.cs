using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource button;
    public AudioSource pickUpCoins;
    public AudioSource heal;
    public AudioSource bulletHit;
    public AudioSource reload;
    public AudioSource playerDash;
    public AudioSource playerHurt;
    public AudioSource enemyHurt;
    public AudioSource enemyDeath;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void PlaySfx(AudioSource sfx)
    {
        float randPitch = Random.Range(0.8f, 1f);
        sfx.pitch = randPitch;
        sfx.Play();
    }
    
}
