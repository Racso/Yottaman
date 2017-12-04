using UnityEngine;
using System.Collections;

public class ScenarioHealth : Health
{

    public override void Hit(int damage, Bullet bullet)
    {
        if (damage > 1)
        {
            GameManager.Instance.PropertyDamaged(bullet.transform.position);
        }
        if (damage >= 4)
        {
            GameManager.Instance.GameOver(bullet.transform.position);
        }
    }

}
