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
        transform.position = startingPoint;
        _rb = GetComponent<Rigidbody2D>();
        var direction = target.TargetCenter() - startingPoint;
        _rb.velocity = direction.normalized * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("balazo");
        Health victimHealth = collision.GetComponent<Health>();
        victimHealth.Hit(10);
    }

    void Update()
    {
        if (transform.position.magnitude > MaxDistanceBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }
}
