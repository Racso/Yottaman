using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloor : Enemy {


    public LayerMask LayerVictims;

    private float AttackRange = 400;
    private float AttackCooldown = 5;

    public List<Transform> Points;


    public Vector2 TimeBetweenPositionChanges;

	public override void Start () {
        base.Start();
        StartCoroutine(Routine_ChangeLocation());
        StartCoroutine(Routine_Attack());
    }
	

    private IEnumerator Routine_ChangeLocation()
    {
        while (true)
        {
            if (Points.Count < 2) { yield break; }
            NextPoint = Points[0].position.RandomBetween(Points[1].position);
            yield return new WaitUntil(() => IsMoving()==false);
            yield return new WaitForSeconds(Random.Range(TimeBetweenPositionChanges.x, TimeBetweenPositionChanges.y));
        }
    }

    private IEnumerator Routine_Attack()
    {
        var timeBetweenChecks = 0.5f;
        var boxSize = new Vector2(AttackRange * 2, AttackRange * 2);

        while (true)
        {
            yield return new WaitForSeconds(timeBetweenChecks);
            if (IsMoving()) { continue; }

            var hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, 0, LayerVictims);
            if (!hit) { continue; }
            Attack(hit.collider.gameObject);
            yield return new WaitForSeconds(AttackCooldown - timeBetweenChecks);
        }
    }



}
