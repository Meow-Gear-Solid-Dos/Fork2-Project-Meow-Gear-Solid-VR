using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private GameObject bloodSplat;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject handL;
    [SerializeField] private GameObject handR; 
    [SerializeField] private Renderer handLRenderer;
    [SerializeField] private Renderer handRRenderer;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject PlayerDyingModel;
    private GameObject splatEffect;
    public VideoFader videoFader;
    public ScreenFader screenFader;
    public Transform playerHead;
    public bool isDead;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip gameOverTheme;
    public bool gameOverMusicPlaying;


    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvulnerable;
    
    public HealthBar healthBar;
    

    public float MaxHealth{
        get { return MaxHealth; }
    }
    public float CurrentHealth{
        get { return CurrentHealth; }
        
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.FindWithTag("Healthbar").GetComponent<HealthBar>();
        isInvulnerable = false;
        isDead = false;
        healthBar.SetHealth(currentHealth);
        GameOverScreen.SetActive(false);

    }
    public void TakeDamage(float damageAmount)
    {
        // Debug.Log("TakeDamage(): " + isInvulnerable);
        if(isInvulnerable == false)
        {
            StartCoroutine("GetInvulnerable");
            currentHealth -= damageAmount;
            splatEffect = Instantiate(bloodSplat, playerHead, false);
            StartCoroutine(BloodTimer(splatEffect));
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0)
            {
                onDeath();
            }
        }
    }

    public void HealHealth(float healAmount){
        if(currentHealth + healAmount > maxHealth){
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
        else{
            currentHealth += healAmount;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void onDeath()
    {
        StartCoroutine("DeathTimer");
        Debug.Log("GAME OVER");
    }

    void Update()
    {//tests our damage function. Must remove later
		if (Input.GetKeyDown(KeyCode.G))
		{
			TakeDamage(50);
		}
        if (Input.GetKeyDown(KeyCode.H))
		{
			HealHealth(20);
		}
        if(handLRenderer == null)
        {
            handLRenderer = GameObject.FindWithTag("HandL").GetComponent<Renderer>();
        }
        if(handRRenderer == null)
        {
            handRRenderer = GameObject.FindWithTag("HandR").GetComponent<Renderer>();
        }
    }
    IEnumerator GetInvulnerable()
    {
        isInvulnerable = true;
        StartCoroutine("FlashColor");
        yield return new WaitForSeconds(2f);
        StopCoroutine("FlashColor");
        isInvulnerable = false;
    }
    IEnumerator FlashColor()
    {
        Color invisibleL;
        Color invisibleR;
        invisibleL = handLRenderer.material.color;
        invisibleR = handRRenderer.material.color;
        int x = 0;
        while(x <= 10)
        {
            invisibleL.a = .25f;
            invisibleR.a = .25f;
            handLRenderer.material.color = invisibleL;
            handRRenderer.material.color = invisibleR;
            yield return new WaitForSeconds(.25f);
            invisibleL.a = 1f;
            invisibleR.a = 1f;
            handLRenderer.material.color = invisibleL;
            handRRenderer.material.color = invisibleR;
            yield return new WaitForSeconds(.25f);
            x++;
        }
    }
    IEnumerator DeathTimer()
    {
        EventBus.Instance.LevelLoadStart();
        StartCoroutine(FadeOut(musicSource, 1));
        screenFader.FadeToBlack(1.5f);
        isDead = true;
        yield return new WaitForSeconds(1.5f);
        GameOverScreen.SetActive(true);
        screenFader.FadeFromBlack(1.5f);
        StartCoroutine("FlashColor");
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        GameOverScreen.SetActive(true);
    }
    IEnumerator BloodTimer(GameObject splatEffect)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(splatEffect);
    }
    IEnumerator PlayGameOverTheme()
    {
                StartCoroutine(FadeOut(musicSource, 1));
                gameOverMusicPlaying = true;
                musicSource.Stop();
                Debug.Log("Old Music Stopped");
                musicSource.clip = gameOverTheme;
                Debug.Log("Music Switched");
                musicSource.Play();
                Debug.Log("Music Started");
                yield return new WaitForSeconds (musicSource.clip.length);
    }
 
    IEnumerator FadeOut(AudioSource audioSource, float FadeTime) 
    {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop();
        audioSource.volume = startVolume;
        StopCoroutine ("PlayGameOverTheme");
        gameOverMusicPlaying = false;
    }
    
}
