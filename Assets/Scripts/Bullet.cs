using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask Targets;
    public float Speed;

    private float MaxDistanceBeforeDestroy = 5000f;
    Rigidbody2D _rb;

    public void SetBullet(Vector3 startingPoint, GameObject target)
    {
        SetBullet(startingPoint, target.TargetCenter());
    }

    public void SetBullet(Vector3 startingPoint, Vector3 target)
    {
        transform.position = startingPoint;
        _rb = GetComponent<Rigidbody2D>();
        var direction = target - startingPoint;
        _rb.velocity = direction.normalized * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Targets.ContainsObject(collision.gameObject)) { return; }
        Health victimHealth = collision.GetComponent<Health>();
        victimHealth.Hit(10);
        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.magnitude > MaxDistanceBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }
}
