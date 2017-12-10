using UnityEngine;
using System.Collections;

public abstract class HeroSkill : MonoBehaviour
{

    protected Hero hero;

    protected void Start()
    {
        hero = GetComponent<Hero>();
    }

    public static Vector3 PointerPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
