using UnityEngine;
using System.Collections;

public class HeroHealth : Health
{
    public override void Hit(int damage)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.Shield);
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
