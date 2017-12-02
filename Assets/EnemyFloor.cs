using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloor : Enemy {

    public float RunningSpeed;

    public List<Transform> Points;
    public Vector3 NextPoint;

    public Vector2 TimeBetweenPositionChanges;

	void Start () {
        StartCoroutine(Routine_ChangeLocation());
	}
	
	void Update ()
    {
        var direction = NextPoint - transform.position;
        transform.Translate(direction.normalized * RunningSpeed * Time.deltaTime);
    }

    private IEnumerator Routine_ChangeLocation()
    {
        while (true)
        {
            NextPoint = Points[0].position.RandomBetween(Points[1].position);
            yield return new WaitUntil(() => transform.position.IsCloseEnoughTo(NextPoint));
            yield return new WaitForSeconds(Random.Range(TimeBetweenPositionChanges.x, TimeBetweenPositionChanges.y));
        }
    }

}
