using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public Enemy PrefabSuelo;
    public Enemy PrefabJetpack;

    private Vector2 TimeForCriminals = new Vector2(0.5f, 1f);

    void Start ()
    {
        AudioManager.Instance.PlayMusic();

        StartCoroutine(Routine_SpawnCriminals());
    }
	
	void Update ()
    {
		
	}

    IEnumerator Routine_SpawnCriminals()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(TimeForCriminals.x, TimeForCriminals.y));

            var criminalType = 1; // Random.Range(0, 2);

            if (criminalType == 0)
            {
                SpawnFloor();
            }
            else
            {
                SpawnJetpack();
            }

        }
    }


    public void SpawnFloor()
    {
        var newCriminal = Instantiate(PrefabSuelo) as EnemyFloor;
        newCriminal.Points.Add(Scenario.Instance.BottomLeft);
        newCriminal.Points.Add(Scenario.Instance.BottomRight);
        int startingSide = Random.Range(0, 2);
        newCriminal.transform.position = newCriminal.Points[startingSide].position;
    }

    public void SpawnJetpack()
    {
        var newCriminal = Instantiate(PrefabJetpack) as EnemyFloor;
        newCriminal.Points.Add(Scenario.Instance.BottomLeft);
        newCriminal.Points.Add(Scenario.Instance.TopRight);
        int startingSide = Random.Range(0, 2);
        newCriminal.transform.position = startingSide == 0 ?
            Scenario.Instance.BottomLeft.position.RandomBetween(Scenario.Instance.TopLeft.position) :
            Scenario.Instance.BottomRight.position.RandomBetween(Scenario.Instance.TopRight.position);
    }

}
