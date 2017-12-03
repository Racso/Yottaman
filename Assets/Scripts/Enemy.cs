using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float RunningSpeed;
    public Bullet BulletPrefab;

    public Vector3 NextPoint;
    public Pointer PointerWhenAttacking;

    private GameObject AttackTarget;

    public bool IsDying;

    Animator _anim;

    private bool _attacking = false;

    public virtual void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _anim.SetBool("walking", false);

        if (IsMoving())
        {
            var direction = NextPoint - transform.position;
            var distanceToMove = RunningSpeed * Time.deltaTime;
            if (distanceToMove > direction.magnitude)
            {
                transform.position = NextPoint;
            }
            else
            {
                transform.Translate(direction.normalized * RunningSpeed * Time.deltaTime);
            }

            var newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * Mathf.Sign(direction.x);
            transform.localScale = newScale;

            _anim.SetBool("walking", true);
        }
        else if (IsAttacking() && AttackTarget != null)
        {
            transform.localScale = transform.LocalScaleLookingTowards(AttackTarget.transform.position);
        }
    }

    public virtual void Attack(GameObject target)
    {
        if (IsAttacking()) { return; }
        StartCoroutine(Routine_Attack(target));
    }

    public IEnumerator Routine_Attack(GameObject target)
    {
        _attacking = true;
        AttackTarget = target;
        _anim.SetBool("attacking", true);
        PointerWhenAttacking.SetTarget(target);

        yield return new WaitForSeconds(0.5f);

        var newBullet = Instantiate(BulletPrefab);
        PointerWhenAttacking.Paused = true;
        newBullet.SetBullet(PointerWhenAttacking.HotPoint.transform.position, target);

        yield return new WaitForSeconds(0.2f);

        _anim.SetBool("attacking", false);
        PointerWhenAttacking.SetTarget(null);

        _attacking = false;
        AttackTarget = null;
    }

    public bool IsAttacking()
    {
        return _attacking;
    }

    public bool IsMoving()
    {
        return !transform.position.IsCloseEnoughTo(NextPoint);
    }

}
