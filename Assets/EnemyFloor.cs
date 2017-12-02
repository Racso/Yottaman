using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloor : Enemy {

    public float RunningSpeed;

    public LayerMask LayerVictims;

    private float AttackRange = 200;
    private float AttackCooldown = 5;

    public List<Transform> Points;
    public Vector3 NextPoint;

    public Vector2 TimeBetweenPositionChanges;

	void Start () {
        StartCoroutine(Routine_ChangeLocation());
        StartCoroutine(Routine_Attack());
    }
	
	void Update ()
    {
        if (IsMoving())
        {
            var direction = NextPoint - transform.position;
            transform.Translate(direction.normalized * RunningSpeed * Time.deltaTime);
        }
    }

    private IEnumerator Routine_ChangeLocation()
    {
        while (true)
        {
            NextPoint = Points[0].position.RandomBetween(Points[1].position);
            yield return new WaitUntil(() => IsMoving()==false);
            yield return new WaitForSeconds(Random.Range(TimeBetweenPositionChanges.x, TimeBetweenPositionChanges.y));
        }
    }

    private IEnumerator Routine_Attack()
    {
        var timeBetweenChecks = 0.5f;
        var boxSize = new Vector2(AttackRange * 2, 50);
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenChecks);
            if (IsMoving()) { continue; }

            var hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, 0, LayerVictims);
            if (!hit) { continue; }
            Health victimHealth = hit.collider.GetComponent<Health>();
            victimHealth.Hit(10);
            yield return new WaitForSeconds(AttackCooldown - timeBetweenChecks);
        }
    }

    private bool IsMoving()
    {
        return !transform.position.IsCloseEnoughTo(NextPoint);
    }

}
