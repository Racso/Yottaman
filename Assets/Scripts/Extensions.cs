using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    public static float Zero = 0.1f;

    public static bool IsCloseEnoughTo(this Vector3 position, Vector3 other)
    {
        return (position - other).magnitude <= Zero;
    }

    public static Vector3 RandomPointBetween(Vector3 pointA, Vector3 pointB)
    {
        return new Vector3(Random.Range(pointA.x, pointB.x), Random.Range(pointA.y, pointB.y), Random.Range(pointA.z, pointB.z));
    }

    public static Vector3 TargetCenter(this GameObject go)
    {
        var col = go.GetComponent<Collider2D>();
        if (col == null) { return go.transform.position; }
        return col.bounds.center;
    }

    public static bool ContainsObject(this LayerMask mask, GameObject go)
    {
        return (mask.value & 1 << go.layer) != 0;
    }

    public static Vector3 LocalScaleLookingTowards(this Transform transform, Vector3 to)
    {
        float lookTo = Mathf.Sign(to.x - transform.position.x);
        if (lookTo == 0) { lookTo = transform.localScale.x; }
        return new Vector3(lookTo, transform.localScale.y, transform.localScale.z);
    }

    public static void FlipToLookTo(this Transform gameobject, Vector3 to)
    {
        gameobject.transform.localScale = gameobject.transform.LocalScaleLookingTowards(to);
    }
}
