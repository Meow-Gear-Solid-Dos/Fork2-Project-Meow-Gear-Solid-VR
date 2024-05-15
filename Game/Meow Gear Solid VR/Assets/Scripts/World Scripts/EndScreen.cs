using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class EndScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text alertStats;
    [SerializeField] private TMP_Text timeStats;
    [SerializeField] private TMP_Text killsStats;
    [SerializeField] private GameObject statsScreen;
    [SerializeField] private GameObject thanksScreen;
    public ScreenFader fader;
    public bool endScreen;
    // Start is called before the first frame update
    void Start()
    {
        alertStats.SetText(EventBus.Instance.numTimesAlertPhaseEntered.ToString());
        timeStats.SetText(Time.time.ToString());
        killsStats.SetText(EventBus.Instance.numKilledEnemies.ToString());
        thanksScreen.SetActive(false);
        statsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void StartDelay()
    {
        StartCoroutine("Delay");
    }
    public IEnumerator Delay()
    {
        fader.FadeToBlack(1.5f);
        yield return new WaitForSeconds(2f);
        statsScreen.SetActive(false);
        thanksScreen.SetActive(true);
        fader.FadeFromBlack(1f);
        endScreen = true;
    }
    public IEnumerator DelayTwo()
    {
        fader.FadeToBlack(1.5f);
        yield return new WaitForSeconds(2f);
        statsScreen.SetActive(true);
        thanksScreen.SetActive(false);
        fader.FadeFromBlack(1f);
        endScreen = false;
    }
}
