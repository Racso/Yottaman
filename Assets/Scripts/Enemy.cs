using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected Animator animator;

    public virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public abstract bool IsAttacking();
    public abstract bool IsMoving();

    public abstract void HitHandler(int damage, LinearProjectile bullet);

}
