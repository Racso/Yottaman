using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HeroSkillLaser : HeroSkill
{

    public Transform LasersInitialPosition;
    public float cooldownWhenPressed = 0.5f;
    public List<LinearProjectile> BulletPrefabsPerLevel;

    private string LaserButton = "Fire1";
    private string SkillAnimationName = "laser";
    private bool isInCooldown = false;

    void Update()
    {
        if (!hero.enabled) { return; }

        QuickResetCooldownIfPossible();
        ShootIfPossible();

        hero.UpdateSkillAnimation(SkillAnimationName, InputIsTryingToShoot());
    }

    private void QuickResetCooldownIfPossible()
    {
        if (Input.GetButtonUp(LaserButton))
        {
            isInCooldown = false;
            StopAllCoroutines();
        }
    }

    private void ShootIfPossible()
    {
        if (!ShouldShootRightNow()) { return; }

        var newLaser = Instantiate(BulletPrefabsPerLevel[hero.Level]);
        newLaser.InitInPositionWithTarget(LasersInitialPosition.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        AudioManager.Instance.PlaySFX(AudioManager.Instance.Laser);

        isInCooldown = true;
        StartCoroutine(Routine_ResetCooldownAfterTime());
    }

    private bool ShouldShootRightNow()
    {
        return InputIsTryingToShoot() && !isInCooldown;
    }

    private bool InputIsTryingToShoot()
    {
        return Input.GetButton(LaserButton);
    }

    private IEnumerator Routine_ResetCooldownAfterTime()
    {
        yield return new WaitForSeconds(cooldownWhenPressed);
        isInCooldown = false;
    }

}
