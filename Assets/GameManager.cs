using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Civilian PrefabCivilian;
    public List<Transform> CivilianSpawnPoints;

    private float MinTimeForCivilians = 0.1f;
    private float MaxTimeForCivilians = 3f;

	void Start () {
        StartCoroutine(Routine_SpawnCivilians());
	}
	
	void Update () {
		
	}

    IEnumerator Routine_SpawnCivilians()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinTimeForCivilians, MaxTimeForCivilians));

            var newCivilian = Instantiate(PrefabCivilian);
            int startingPoint = Random.Range(0, CivilianSpawnPoints.Count);
            int endingPoint = (startingPoint + 1 ) % CivilianSpawnPoints.Count;
            newCivilian.transform.position = CivilianSpawnPoints[startingPoint].position;
            newCivilian.FinishPoint = CivilianSpawnPoints[endingPoint].position;
        }
    }
}
