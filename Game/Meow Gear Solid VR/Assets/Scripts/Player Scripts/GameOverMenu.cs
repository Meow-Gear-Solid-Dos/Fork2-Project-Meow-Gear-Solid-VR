using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject Everthing;
    public void StartGame ()
    {
        Debug.Log("Loading Next Level");
        SceneManager.LoadScene(0);
        GameOverScreen.SetActive(false);
        Destroy(Everthing);

    }
    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
    
}