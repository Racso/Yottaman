using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HeroLaser : HeroSkill
{

    public Transform LaserPosition;

    private float _cooldown = 1f;
    private bool _isInCooldown = false;

    public List<Bullet> BulletPrefabsPerLevel;

    void Update()
    {
        if (Input.GetButton("Fire1") && !_isInCooldown)
        {
            _isInCooldown = true;
            StartCoroutine(Routine_Cooldown());

            var newLaser = Instantiate(BulletPrefabsPerLevel[_hero.Level]);
            newLaser.SetBullet(LaserPosition.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Laser);
        }

        _hero._anim.SetBool("laser", Input.GetButton("Fire1"));
    }

    private IEnumerator Routine_Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _isInCooldown = false;
    }
}
