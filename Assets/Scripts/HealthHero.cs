using UnityEngine;
using System.Collections;

public class HealthHero : Health
{
    public LinearProjectile CounterBulletSmall;
    public LinearProjectile CounterBulletBig;

    public override void Hit(int damage, LinearProjectile bullet)
    {
        DestroyObject(bullet.gameObject);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Shield);
        if (Hero.Instance.Level == 1)
        {
            var counterBullet = Instantiate(CounterBulletSmall);
            counterBullet.SetBullet(bullet.transform.position, bullet.transform.position - (Vector3)bullet.GetComponent<Rigidbody2D>().velocity);
        }
        else if (Hero.Instance.Level > 1)
        {
            var counterBullet = Instantiate(CounterBulletBig);
            counterBullet.SetBullet(bullet.transform.position, bullet.transform.position - (Vector3)bullet.GetComponent<Rigidbody2D>().velocity);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
