﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianHealth : Health {

    Animator _anim;

	void Start () {
        _anim = GetComponentInChildren<Animator>();	
	}
	
    public override void Hit(int damage)
    {
        _anim.SetBool("dying", true);
        var enemy = GetComponent<Enemy>();
        enemy.StopAllCoroutines();
        enemy.enabled = false;
        enemy.PointerWhenAttacking.gameObject.SetActive(false);
        StartCoroutine(Coroutine_Die());
    }

    IEnumerator Coroutine_Die()
    {
        float a = 1;
        while (a>0) {
            var sprites = GetComponentsInChildren<SpriteRenderer>();
            a -= Time.deltaTime;
            Color col = new Color(1, 1, 1, a);
            foreach (var sprite in sprites)
            {
                sprite.color = col;
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}
