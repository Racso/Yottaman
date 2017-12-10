using UnityEngine;
using System.Collections;

public class HealthHero : Health
{

    public LinearProjectile CounterBulletSmall;
    public LinearProjectile CounterBulletBig;

    private Hero hero;

    private void Start()
    {
        hero = GetComponent<Hero>();
    }

    public override void Hit(int damage, LinearProjectile bullet)
    {
        DestroyObject(bullet.gameObject);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Shield);

        if (hero.Level == 0) { return; }

        Vector3 incomingBulletPreviousPosition = bullet.transform.position - (Vector3)bullet.GetComponent<Rigidbody2D>().velocity;
        LinearProjectile counterBullet = hero.Level == 1 ? Instantiate(CounterBulletSmall) : Instantiate(CounterBulletBig);
        counterBullet.InitInPositionWithTarget(bullet.transform.position, incomingBulletPreviousPosition);
    }

}
