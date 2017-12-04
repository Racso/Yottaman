using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    public int EnemiesLeft;
    private int TotalEnemies;

    public int DamageToProperty;

    public Enemy PrefabSuelo;
    public Enemy PrefabJetpack;

    private Coroutine TutorialRoutine;
    private Coroutine SpawnerRoutine;
    private bool SpawningEnemies = false;
    private bool InGameOver = false;

    private List<Vector2> TimeForCriminals = new List<Vector2> { new Vector2(1f, 5f), new Vector2(1f, 3f), new Vector2(1f, 2f) };

    public GameObject FirePrefab;

    public void PropertyDamaged(Vector3 position)
    {
        var newFire = Instantiate(FirePrefab);
        newFire.transform.position = position;
        DamageToProperty += 1;
        if (DamageToProperty >= 20)
        {
            GameOverByFire(position);
        }
    }


    void Start ()
    {
        TotalEnemies = EnemiesLeft;
        AudioManager.Instance.PlayMusic();
        TutorialRoutine = StartCoroutine(Tutorial());
        SpawnerRoutine = StartCoroutine(Routine_SpawnCriminals());
    }
	
	void Update ()
    {
        if (InGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && TutorialRoutine != null)
        {
            StopCoroutine(TutorialRoutine);
            TutorialRoutine = null;
            Hero.Instance.GetComponent<Hero>().enabled = true;
            SpawningEnemies = true;
        }
		if (EnemiesLeft < 1)
        {
            if (FindObjectOfType<Enemy>() == null)
            {
                if (InGameOver) { return; }
                InGameOver = true;
                StartCoroutine(Win());
            }
        }
	}

    IEnumerator Routine_SpawnCriminals()
    {
        while (EnemiesLeft>0)
        {
            yield return new WaitForSeconds(Random.Range(TimeForCriminals[Hero.Instance.Level].x, TimeForCriminals[Hero.Instance.Level].y));
            if (!SpawningEnemies) { continue; }

            var criminalType = Random.Range(0, 2);

            if (criminalType == 0)
            {
                SpawnFloor();
            }
            else
            {
                SpawnJetpack();
            }

            EnemiesLeft -= 1;
            if (EnemiesLeft == NextLevelUp())
            {
                StartCoroutine(LevelUp());
            }
        }
    }


    public EnemyFloor SpawnFloor()
    {
        var newCriminal = Instantiate(PrefabSuelo) as EnemyFloor;
        newCriminal.Points.Add(Scenario.Instance.BottomLeft);
        newCriminal.Points.Add(Scenario.Instance.BottomRight);
        int startingSide = Random.Range(0, 2);
        newCriminal.transform.position = newCriminal.Points[startingSide].position;
        return newCriminal;
    }

    public EnemyFloor SpawnJetpack()
    {
        var newCriminal = Instantiate(PrefabJetpack) as EnemyFloor;
        newCriminal.Points.Add(Scenario.Instance.BottomLeft);
        newCriminal.Points.Add(Scenario.Instance.TopRight);
        int startingSide = Random.Range(0, 2);
        newCriminal.transform.position = startingSide == 0 ?
            Scenario.Instance.BottomLeft.position.RandomBetween(Scenario.Instance.TopLeft.position) :
            Scenario.Instance.BottomRight.position.RandomBetween(Scenario.Instance.TopRight.position);
        return newCriminal;
    }


    private int NextLevelUp()
    {
        if (Hero.Instance.Level == 0) { return 85; }
        if (Hero.Instance.Level == 1) { return 50; }
        return -999999;
    }


    private IEnumerator Tutorial()
    {
        yield return null;
        Hero.Instance.GetComponent<Hero>().enabled = false;
        var tutorialCriminal = SpawnFloor();
        var newPos = tutorialCriminal.transform.position;
        newPos.x = 0;
        tutorialCriminal.transform.position = newPos;
        tutorialCriminal.Points.Clear();

        UIManager.Instance.Say("( Press ESC at any time to SKIP the tutorial )",3.5f);
        yield return new WaitForSeconds(4);
        UIManager.Instance.Say("Hello! I'm YOTTAMAN!",3.5f);
        yield return new WaitForSeconds(3);
        UIManager.Instance.Say("My power is awesome, but I get even MORE POWER with time!", 5);
        yield return new WaitForSeconds(5.5f);
        UIManager.Instance.Say("Oh, sorry about that, I didn't see you there!",3f);
        yield return new WaitForSeconds(3.5f);
        UIManager.Instance.Say("Let's say hi to our new friend, shall we?",2);
        yield return new WaitForSeconds(2.5f);
        Hero.Instance.GetComponent<Hero>().enabled = true;
        UIManager.Instance.Say("MOVE: WASD / ARROWS\nTARGET: MOUSE\nSHOOT: CLICK", 100);
        yield return new WaitUntil(() => tutorialCriminal == null);
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.Say("He'll be fine. Almost. Maybe.",3f);
        yield return new WaitForSeconds(3.5f);
        UIManager.Instance.Say("Anyway: Here comes the bride! I mean, the crime!", 4f);
        SpawningEnemies = true;
        yield return new WaitForSeconds(4.5f);
        UIManager.Instance.Say("...");
        yield return new WaitForSeconds(1);
        UIManager.Instance.Say("... Hahaha? No? OK then.");
    }

    private IEnumerator LevelUp()
    {
        yield return null;
        Time.timeScale = 0;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.LevelUp);

        if (Hero.Instance.Level == 0)
        {
            UIManager.Instance.Say("HAHA, YEAH! MORE POWER!", 3f);
            yield return new WaitForSecondsRealtime(3.5f);
            UIManager.Instance.Say("More power is always better, isn't it?", 3f);
            yield return new WaitForSecondsRealtime(2);
        }
        else
        {
            UIManager.Instance.Say("MORE POWER!... yay?", 3f);
            yield return new WaitForSecondsRealtime(3.5f);
            UIManager.Instance.Say("I guess I CAN be careful...", 3f);
            yield return new WaitForSecondsRealtime(2);
        }
        Hero.Instance.Level += 1;
        Time.timeScale = 1;
    }

    public void GameOver(Vector3 positionOfGameOver)
    {
        if (InGameOver) { return; }
        InGameOver = true;
        StartCoroutine(RoutineGameOver(positionOfGameOver));
    }


    public void GameOverByFire(Vector3 positionOfGameOver)
    {
        if (InGameOver) { return; }
        InGameOver = true;
        StartCoroutine(RoutineGameOverFire(positionOfGameOver));
    }



    private IEnumerator Win()
    {
        CameraManager.Instance.IsFollowing = false;
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            Destroy(bullet.gameObject);
        }
        StopCoroutine(SpawnerRoutine);
        Destroy(Hero.Instance.gameObject);
        StopAllCoroutines();
        UIManager.Instance.ShowGameOverImageWin();
        yield return null;
    }

    IEnumerator RoutineGameOver(Vector3 position)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        UIManager.Instance.SayPreGameOver();
        yield return new WaitForSecondsRealtime(1);
        CameraManager.Instance.IsFollowing = false;
        CameraManager.Instance.transform.position = new Vector3(position.x, position.y, CameraManager.Instance.transform.position.z);
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 0.1f;
        CameraManager.Instance.Shake(4);
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        UIManager.Instance.ShowGameOverImage();
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            Destroy(bullet.gameObject);
        }
        StopCoroutine(SpawnerRoutine);
        Destroy(Hero.Instance.gameObject);
        yield return new WaitForSecondsRealtime(2);
        UIManager.Instance.SayGameOver();
        StopAllCoroutines();
    }

    IEnumerator RoutineGameOverFire(Vector3 position)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        UIManager.Instance.SayPreGameOver();
        yield return new WaitForSecondsRealtime(1);
        CameraManager.Instance.IsFollowing = false;
        CameraManager.Instance.transform.position = new Vector3(position.x, position.y, CameraManager.Instance.transform.position.z);
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 0.3f;
        CameraManager.Instance.Shake(4);
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        UIManager.Instance.ShowGameOverImageFire();
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
        foreach (var bullet in FindObjectsOfType<Bullet>())
        {
            Destroy(bullet.gameObject);
        }
        StopCoroutine(SpawnerRoutine);
        Destroy(Hero.Instance.gameObject);
        yield return new WaitForSecondsRealtime(2);
        UIManager.Instance.SayGameOver();
        StopAllCoroutines();
    }


}
