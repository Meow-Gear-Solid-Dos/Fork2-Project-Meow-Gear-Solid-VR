using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    public void StartGame ()
    {
        Debug.Log("Loading Next Level");
        SceneManager.LoadScene(0);
        GameOverScreen.SetActive(false);

    }
    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
    
}