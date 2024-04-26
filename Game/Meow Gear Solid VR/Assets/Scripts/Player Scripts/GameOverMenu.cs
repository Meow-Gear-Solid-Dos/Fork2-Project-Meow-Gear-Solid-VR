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
        Time.timeScale = 1;
        GameOverScreen.SetActive(false);
        playerHealth.currentHealth = 100f;
    }
    
}