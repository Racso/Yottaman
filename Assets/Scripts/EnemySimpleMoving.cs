using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleMoving : Enemy {

    public float MovingSpeed = 800;
    public Vector2 TimeBetweenPositionChanges;
    public float AttackRange = 600;
    public float AttackCooldown = 5;
    public LayerMask LayerVictims;
    public LinearProjectile ShootingBulletPrefab;

    private RotateTowardsTarget PointerWhenAttacking;
    private Vector3 MovementTargetPosition;
    private GameObject AttackTarget;

    public List<Transform> CornersOfMovingArea = new List<Transform>();


	public override void Start () {
        base.Start();

        PointerWhenAttacking = GetComponentInChildren<RotateTowardsTarget>();

        StartCoroutine(Routine_ChangeLocation());
        StartCoroutine(Routine_ChangeAttackTarget());
    }

    private IEnumerator Routine_ChangeLocation()
    {
        if (CornersOfMovingArea.Count < 2) { yield break; }

        SetNewTargetLocation();

        while (true)
        {
            yield return new WaitForSeconds(RandomSecondsBetweenChangingLocations());
            SetNewTargetLocation();
            yield return new WaitUntil(() => CloseEnoughToTarget());
        }
    }

    private void SetNewTargetLocation()
    {
        MovementTargetPosition = Extensions.RandomPointBetween(CornersOfMovingArea[0].position, CornersOfMovingArea[1].position);
    }

    private float RandomSecondsBetweenChangingLocations()
    {
        return Random.Range(TimeBetweenPositionChanges.x, TimeBetweenPositionChanges.y);
    }

    private bool CloseEnoughToTarget()
    {
        return transform.position.IsCloseEnoughTo(MovementTargetPosition);
    }

    private IEnumerator Routine_ChangeAttackTarget()
    {
        var waitTimeBetweenTryingToAttack = 0.5f;
        var boxSize = new Vector2(AttackRange * 2, AttackRange * 2);

        StopAttackingAnimation();

        while (true)
        {
            yield return new WaitForSeconds(waitTimeBetweenTryingToAttack);
            if (!ReadyToAttack()) { continue; }

            var hitSomething = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, 0, LayerVictims);
            if (!hitSomething) { continue; }

            StartAttackingTarget(hitSomething.collider.gameObject);

            yield return new WaitForSeconds(AttackCooldown - waitTimeBetweenTryingToAttack);
        }
    }

    private bool ReadyToAttack()
    {
        return !Moving && !Attacking;
    }

    private bool Moving
    {
        get
        {
            return animator.GetBool("walking");
        }
        set
        {
            animator.SetBool("walking", value);
        }
    }

    private bool Attacking
    {
        get
        {
            return animator.GetBool("attacking");
        }
        set
        {
            animator.SetBool("attacking", value);
        }
    }

    public virtual void StartAttackingTarget(GameObject target)
    {
        StartCoroutine(Routine_Attack(target));
    }

    public IEnumerator Routine_Attack(GameObject target)
    {
        StartAttackingAnimationOnTarget(target);

        yield return new WaitForSeconds(0.5f);

        var newBullet = Instantiate(ShootingBulletPrefab);
        newBullet.InitInPositionWithTarget(PointerWhenAttacking.HotPoint.transform.position, target);
        PointerWhenAttacking.Paused = true;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Shoot);

        yield return new WaitForSeconds(0.2f);

        StopAttackingAnimation();
    }

    private void StartAttackingAnimationOnTarget(GameObject target)
    {
        Attacking = true;
        AttackTarget = target;
        PointerWhenAttacking.SetTarget(target);
    }

    private void StopAttackingAnimation()
    {
        Attacking = false;
        AttackTarget = null;
        PointerWhenAttacking.SetTarget(null);
    }

    void Update()
    {
        Moving = false;

        if (!CloseEnoughToTarget())
        {
            Moving = true;
            MoveTorwardsTarget();
        }

        if (Attacking && AttackTarget != null)
        {
            transform.FlipToLookTo(AttackTarget.transform.position);
        }
    }

    private void MoveTorwardsTarget()
    {
        transform.FlipToLookTo(MovementTargetPosition);

        var vectorDistanceToTarget = MovementTargetPosition - transform.position;
        var distanceToMoveThisFrame = MovingSpeed * Time.deltaTime;
        if (distanceToMoveThisFrame > vectorDistanceToTarget.magnitude)
        {
            transform.position = MovementTargetPosition;
        }
        else
        {
            var vectorToMoveThisFrame = vectorDistanceToTarget.normalized * MovingSpeed * Time.deltaTime;
            transform.Translate(vectorToMoveThisFrame);
        }
    }

    public override void HitHandler(int damage, LinearProjectile bullet)
    {
        if (!enabled) { return; }

        enabled = false;
        StopAllCoroutines();
        PointerWhenAttacking.gameObject.SetActive(false);

        var collider = GetComponent<Collider2D>();
        if (collider != null) { collider.enabled = false; }

        if (BulletMakesOffensiveDamage(bullet))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Die2);
            UIManager.Instance.SaySorryEnemy();
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Die1);
        }
        if (BulletIsDestroyedAfterHit(bullet))
        {
            Destroy(bullet.gameObject);
        }

        animator.SetBool("dying", true);
        GameManager.Instance.HandlerEnemyDied(this);

        StartCoroutine(Coroutine_DieWithFading());
    }

    private bool BulletIsDestroyedAfterHit(LinearProjectile bullet)
    {
        return bullet.Damage == 1 || bullet.Damage == 2 || bullet.Damage == 4;
    }

    private bool BulletMakesOffensiveDamage(LinearProjectile bullet)
    {
        return bullet.Damage >= 2;
    }

    IEnumerator Coroutine_DieWithFading()
    {
        var sprites = GetComponentsInChildren<SpriteRenderer>();

        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            Color col = new Color(1, 1, 1, alpha);
            foreach (var sprite in sprites)
            {
                sprite.color = col;
            }
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    public override bool IsAttacking()
    {
        return Attacking;
    }

    public override bool IsMoving()
    {
        return Moving;
    }


}
