using UnityEngine;
using System.Collections;

public class ScenarioHealth : Health
{

    public override void Hit(int damage, Bullet bullet)
    {
        if (damage == 3)
        {
            GameManager.Instance.GameOver(bullet.transform.position);
        }
    }

}
