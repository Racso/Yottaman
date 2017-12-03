using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public Enemy PrefabCriminal;
    public List<Transform> SpawnPoints;

    private Vector2 TimeForCivilians = new Vector2(0.2f, 3f);
    private Vector2 TimeForCriminals = new Vector2(3f, 5f);

    void Start ()
    {
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

            var newCriminal = Instantiate(PrefabCriminal) as EnemyFloor;
            int startingPoint = Random.Range(0, SpawnPoints.Count);
            newCriminal.transform.position = SpawnPoints[startingPoint].position;
            newCriminal.Points = SpawnPoints;
        }
    }


}
