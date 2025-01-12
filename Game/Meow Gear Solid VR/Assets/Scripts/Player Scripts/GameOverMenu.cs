using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject GameOverScreen;
    public PlayerHealth playerHealth;
    public void StartGame ()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        Debug.Log("Loading Next Level");
        SceneManager.LoadScene(0);
        EventBus.Instance.inAlertPhase = false;
        EventBus.Instance.playerisSeen = false;
        EventBus.Instance.numKilledEnemies = 0;
        EventBus.Instance.numTimesAlertPhaseEntered = 0;
        EventBus.Instance.timeElapsed = 0;
        GameOverScreen.SetActive(false);

    }
    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
    public void StartAgain()
    {
        StopCoroutine("DeathTimer");
        playerHealth.isDead = false;
        playerHealth.currentHealth = 100f;
        playerHealth.healthBar.SetHealth(playerHealth.currentHealth);
        playerHealth.StartCoroutine("PlayNormalTheme");
        GameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }
    
}