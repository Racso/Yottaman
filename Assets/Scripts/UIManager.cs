using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Text Level;
    public Text EnemiesLeft;
    public Text DamageToProperty;

    public Text TxtSay;

    public GameObject GameOverImage;
    public GameObject GameOverImageEarthOK;
    public GameObject GameOverImageEarthFire;

    private Coroutine _routine;

    void Start()
    {
        GameOverImage.SetActive(false);
    }


    void Update()
    {
        Level.text = "POWER LEVEL: " + PowerLevelString(Hero.Instance.Level);
        EnemiesLeft.text = "ENEMIES IN BASE: " + GameManager.Instance.EnemiesLeft.ToString();
        DamageToProperty.text = "WORLD DAMAGE LEFT: " + GameManager.Instance.DamageToProperty.ToString();
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
                return "Apocalyptic";
        }
    }

    public void ShowGameOverImage()
    {
        StartCoroutine(RoutineShowGameOver());
    }

    public void ShowGameOverImageWin()
    {
        GameOverImage.gameObject.SetActive(true);
        SayWon();
    }

    public void ShowGameOverImageFire()
    {
        GameOverImageEarthFire.SetActive(true);
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
            "Hey, dude, sorry about that!",
            "Woa, I'm sorry!",
            "Woa, I'm so sorry!",
            "That'll probably leave a mark...",
            "That probably stung...",
            "Hey dude, I... Well, he isn't listening.",
            "Hey dude, I... Well, he isn't looking.",
            "Oh, bollocks!",
            "Oh, for my own sake!",
            "I hope you weren't using your face...",
            "Were you planning to have kids?",
            "He'll recover. I'm sure.",
            "Well, at least he wasn't the Pope.",
            "Well, at least he wasn't the President.",
            "Well, at least he wasn't me.",
            "He had choosen... poorly.",
            "Hum, I felt that.",
            "Look at the brighside... Hello?",
            "That was probably a bit too much.",
            "I think he said something about an itch.",
            "I think he said something about a glitch.",
            "I think he said something about a witch.",
            "I think he said something about a locker.",
            "I think he said something about a peach.",
            "I think he said something about a duck.",
            "I think he said something about a truck.",
            "I think he said something about luck.",
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
            "It's OK. The forecast predicted rain. I think.",
            "It's just some fire.",
            "It's a small fire. Probably won't get big.",
            "It was getting cold anyway. Kind of.",
            "I've chosen... poorly.",
        };
        Say(strings[Random.Range(0, strings.Length)]);
    }

    public void SayGameOver()
    {
        var strings = new string[] {
            "I should've accepted that Jenga night.",
            "Well, that's it for today, I guess.",
            "Aw man, I wanted to watch the World Cup!",
            "The brightside: No more crime. Ever.",
            "So... Time to look for a new job, I guess.",
            "Okay... sh*t.",
        };
        Say(strings[Random.Range(0, strings.Length)], 100);
    }

    public void SayWon()
    {
        var strings = new string[] {
            "HAHA! Told you: I'm awesome!",
            "How about THAT? Earth is safe!",
            "Best. Hero. Ever.",
            "I should be called Awesomeman!",
        };
        Say(strings[Random.Range(0, strings.Length)], 100);
    }

    public void SayPreGameOver()
    {
        var strings = new string[] {
            "OH, NO, NO, NO...!",
            "THAT DOESN'T LOOK GOOD AT ALL.",
            "OH, CRAP.",
            "OH, NOT AGAIN!",
            "NOOOOOOOO!!!",
        };
        Say(strings[Random.Range(0, strings.Length)]);
    }


}