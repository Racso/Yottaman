using UnityEngine;
using System.Collections;

public class HealthScenario : Health
{

    public override void Hit(int damage, LinearProjectile bullet)
    {
        if (damage > 1)
        {
            GameManager.Instance.HandlerPropertyDamaged(bullet.transform.position);
            UIManager.Instance.SaySorryThings();
        }
        if (damage >= 4)
        {
            GameManager.Instance.GameOver(bullet.transform.position);
        }
    }

}
