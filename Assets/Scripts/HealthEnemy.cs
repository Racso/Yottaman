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
        if (_enemy.enabled == false) { return; }

        _enemy.enabled = false;
        _enemy.StopAllCoroutines();
        _enemy.PointerWhenAttacking.gameObject.SetActive(false);
        var collider = GetComponent<Collider2D>();
        if (collider != null) { collider.enabled = false; }

        _anim.SetBool("dying", true);

        if (damage == 1)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Die1);
            Destroy(bullet.gameObject);
        }
        else if (damage == 2 || damage == 4)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Die2);
            UIManager.Instance.SaySorryEnemy();
            Destroy(bullet.gameObject);
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Die2);
            UIManager.Instance.SaySorryEnemy();
        }

        GameManager.Instance.HandlerEnemyDied(_enemy);

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
