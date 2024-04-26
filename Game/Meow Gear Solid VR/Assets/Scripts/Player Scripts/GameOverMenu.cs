using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    public PlayerHealth playerHealth;
    public void StartGame ()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        Debug.Log("Loading Next Level");
        SceneManager.LoadScene(0);
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
        GameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }
    
}