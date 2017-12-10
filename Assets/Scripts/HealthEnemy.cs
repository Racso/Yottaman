using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : Health {

    Animator _anim;
    Enemy _enemy;

	void Start () {
        _anim = GetComponentInChildren<Animator>();
        _enemy = GetComponent<Enemy>();
    }
	
    public override void Hit(int damage, LinearProjectile bullet)
    {
        _enemy.HitHandler(damage, bullet);
    }

}
