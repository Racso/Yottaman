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

    public static Vector3 RandomBetween(this Vector3 a, Vector3 b)
    {
        return new Vector3(Random.Range(a.x, b.x), Random.Range(a.y, b.y));
    }
}
