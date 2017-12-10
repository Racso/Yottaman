using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearProjectile : MonoBehaviour
{

    public int Damage;
    public LayerMask ImpactTargets;
    public float Speed;

    private float MaxDistanceBeforeDestroy = 5000f;
    Rigidbody2D rigidBody;
    
    public void InitInPositionWithTarget(Vector3 startingPoint, GameObject target)
    {
        InitInPositionWithTarget(startingPoint, target.TargetCenter());
    }

    public void InitInPositionWithTarget(Vector3 startingPoint, Vector3 target)
    {
        transform.position = startingPoint;
        SetVelocityTorwardsTarget(target);
    }

    private void SetVelocityTorwardsTarget(Vector3 target)
    {
        rigidBody = GetComponent<Rigidbody2D>();
        var movementDirection = target - transform.position;
        rigidBody.velocity = movementDirection.normalized * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (!ObjectIsAValidTarget(collidedObject)) { return; }

        HitCollidedObject(collidedObject);
    }

    private bool ObjectIsAValidTarget(GameObject gameObject)
    {
        return ImpactTargets.ContainsObject(gameObject);
    }

    private void HitCollidedObject(GameObject collidedObject)
    {
        Health victimHealth = collidedObject.GetComponent<Health>();
        if (victimHealth == null)
        {
            Debug.LogWarning("A target was hit but didn't have a Health component.");
        }
        else
        {
            victimHealth.Hit(Damage, this);
        }
    }

    void Update()
    {
        DestroyIfOutOfBounds();
    }

    private void DestroyIfOutOfBounds()
    {
        if (transform.position.magnitude > MaxDistanceBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }

}
