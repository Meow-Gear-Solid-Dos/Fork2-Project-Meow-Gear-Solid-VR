using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyBossHealth : MonoBehaviour, IHealth
{
    [SerializeField] private GameObject bloodSplat;
    [SerializeField] private GameObject bossModel;
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private Renderer boss;
    private GameObject splatEffect;
    public Transform bossHead;
    public ScreenFader fader;
    public bool isDead;
    
    public float maxHealth = 1000f;
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
        healthBar = GameObject.FindWithTag("BossHealthbar").GetComponent<HealthBar>();
        boss = GameObject.FindWithTag("Boss").GetComponent<Renderer>();
        boss = GetComponentInChildren<SkinnedMeshRenderer>();
        boss.enabled = true;
        isInvulnerable = false;
        isDead = false;
        healthBar.SetHealth(maxHealth);

    }

    public void TakeDamage(float damageAmount)
    {
        // Debug.Log("TakeDamage(): " + isInvulnerable);
        bossAnimator.SetBool("IsHit", false);
        if(isInvulnerable == false)
        {
            StartCoroutine("GetInvulnerable");
            currentHealth -= damageAmount;
            splatEffect = Instantiate(bloodSplat, bossHead, false);
            StartCoroutine(BloodTimer(splatEffect));
            healthBar.SetHealth(currentHealth);

            if(currentHealth <= 0)
            {
                onDeath();
            }
        }
        if(isInvulnerable == true)
        {
            StartCoroutine("GetInvulnerable");
            bossAnimator.SetBool("IsHit", true);
        }
    }

    IEnumerator GetInvulnerable()
    {
        isInvulnerable = true;
        StartCoroutine("FlashColor");
        yield return new WaitForSeconds(2f);
        StopCoroutine("FlashColor");
        isInvulnerable = false;
        bossAnimator.SetBool("IsHit", false);
    }

    IEnumerator FlashColor()
    {
        Color invisible;
        invisible = boss.material.color;
        int x = 0;
        while(x <= 10)
        {
            invisible.a = .25f;
            boss.material.color = invisible;
            yield return new WaitForSeconds(.25f);
            invisible.a = 1f;
            boss.material.color = invisible;
            yield return new WaitForSeconds(.25f);
            x++;
        }
        bossAnimator.SetBool("IsHit", false);
    }

    public void onDeath()
    {
        StartCoroutine(DeathTimer(bossModel));
    }

    IEnumerator DeathTimer(GameObject boss)
    {
        EventBus.Instance.LevelLoadStart();
        isDead = true;
        bossAnimator.SetBool("IsDead", isDead );
        //bossAnimator.SetBool("IsDead", isDead);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("FlashColor");
        yield return new WaitForSeconds(1.5f);
        fader.FadeToBlack(2f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }
    IEnumerator BloodTimer(GameObject splatEffect)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(splatEffect);
    }
}
