using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Text Level;
    public Text EnemiesLeft;

    public Text TxtSay;

    public GameObject GameOverImage;
    public GameObject GameOverImageEarthOK;

    private Coroutine _routine;

    void Start()
    {
        GameOverImage.SetActive(false);
    }


    void Update()
    {
        Level.text = "POWER LEVEL: " + PowerLevelString(Hero.Instance.Level);
        EnemiesLeft.text = "ENEMIES LEFT: " + GameManager.Instance.EnemiesLeft.ToString();
    }

    public void Say(string msg)
    {
        if (_routine != null) { StopCoroutine(_routine); }
        _routine = StartCoroutine(SayMessage(msg, 2+msg.Length / 35));
    }

    public void Say(string msg, float time)
    {
        if (_routine != null) { StopCoroutine(_routine); }
        _routine = StartCoroutine(SayMessage(msg, time));
    }

    private IEnumerator SayMessage(string msg, float time)
    {
        TxtSay.text = msg;
        yield return new WaitForSecondsRealtime(time);
        TxtSay.text = "";
    }

    private string PowerLevelString(int level)
    {
        switch (level)
        {
            case 0:
                return "Some";
            case 1:
                return "Dangerous";
            default:
                return "9001";
        }
    }

    public void ShowGameOverImage()
    {
        StartCoroutine(RoutineShowGameOver());
    }

    IEnumerator RoutineShowGameOver()
    {
        GameOverImage.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        GameOverImageEarthOK.SetActive(false);
    }

    public void SaySorryEnemy()
    {
        var strings = new string[] {
            "Woa, sorry about that!",
            "That'll probably leave a mark...",
            "Humm...",
            "Oh, bollocks!",
            "I hope you weren't using your face...",
            "Were you planning to have kids?",
        };
        Say(strings[Random.Range(0, strings.Length)]);
    }

    public void SaySorryThings()
    {
        var strings = new string[] {
            "I hope nobody lives in those buildings...",
            "I think I'll need to call the firemen...",
            "Humm...",
            "I'm quite sure that fire was already there.",
        };
        Say(strings[Random.Range(0, strings.Length)]);
    }

    public void SayGameOver()
    {
        var strings = new string[] {
            "I should've accepted that Jenga night.",
            "Well, that's it for today, I guess.",
            "There you go. No more crime. Ever.",
            "Okay... sh*t.",
        };
        Say(strings[Random.Range(0, strings.Length)], 100);
    }

    public void SayPreGameOver()
    {
        var strings = new string[] {
            "OH, NO...",
            "THAT DOESN'T LOOK GOOD AT ALL.",
            "OH, CRAP.",
        };
        Say(strings[Random.Range(0, strings.Length)]);
    }

}