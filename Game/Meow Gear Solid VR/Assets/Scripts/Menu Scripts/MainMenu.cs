using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public ScreenFader fader;
    public GameObject startMenu;
    void Start()
    {
        Time.timeScale = 0;
        startMenu.SetActive(true);
        EventBus.Instance.LevelLoadStart();
    }
    public void StartGame ()
    {
        Debug.Log("Starting Game");
        Time.timeScale = 1;
        EventBus.Instance.GameStart();
        StartCoroutine("Delay");
    }
    public void QuitGame ()
    {
        Debug.Log("Program Terminated");
        Application.Quit();
    }
    private IEnumerator Delay()
    {
        float duration = 2f;
        fader.FadeToBlack(duration);
        yield return new WaitForSeconds(duration);
        startMenu.SetActive(false);
        Debug.Log("Entering next level");
        fader.FadeFromBlack(duration);
        EventBus.Instance.LevelLoadEnd();
    }
    
}
