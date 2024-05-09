using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public ScreenFader fader;
    public GameObject player;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("XR System");
        fader = GameObject.FindGameObjectWithTag("XR System").GetComponentInChildren<ScreenFader>();
        DontDestroyOnLoad(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Checks to see if object colliding has player tag
        if (!other.CompareTag("Player"))
        {
            return;
        }
        float timer = 2;
        EventBus.Instance.LevelLoadStart();
        StartCoroutine(Delay(timer, level));

    }
    private IEnumerator Delay(float duration, int level)
    {
        fader.FadeToBlack(duration);
        yield return new WaitForSeconds(duration);
        Debug.Log("Entering next level");
        fader.FadeFromBlack(duration);
        EventBus.Instance.LevelLoadEnd();
        if(level == 2)
        {
            Destroy(player);
        }
        SceneManager.LoadScene(level);
    }
}
