using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioBounds : Singleton<ScenarioBounds>
{
    public List<Transform> TwoCorners;

    private Vector3[] corners;

    private void Start()
    {
        corners = new Vector3[4];
        corners[Corners.TOP_LEFT] = CreateVectorFromCornersByFunctions(Mathf.Min, Mathf.Max);
        corners[Corners.TOP_RIGHT] = CreateVectorFromCornersByFunctions(Mathf.Max, Mathf.Max);
        corners[Corners.BOTTOM_LEFT] = CreateVectorFromCornersByFunctions(Mathf.Min, Mathf.Min);
        corners[Corners.BOTTOM_RIGHT] = CreateVectorFromCornersByFunctions(Mathf.Max, Mathf.Min);
    }

    private Vector3 CreateVectorFromCornersByFunctions(Func<float, float, float> functionForX, Func<float, float, float> functionForY)
    {
        float x = functionForX(TwoCorners[0].position.x, TwoCorners[1].position.x);
        float y = functionForY(TwoCorners[0].position.y, TwoCorners[1].position.y);
        float z = TwoCorners[0].position.z;
        return new Vector3(x, y, z);
    }

    public Vector3 TopLeft
    {
        get
        {
            return corners[Corners.TOP_LEFT];
        }
    }

    public Vector3 TopRight
    {
        get
        {
            return corners[Corners.TOP_RIGHT];
        }
    }

    public Vector3 BottomLeft
    {
        get
        {
            return corners[Corners.BOTTOM_LEFT];
        }
    }

    public Vector3 BottomRight
    {
        get
        {
            return corners[Corners.BOTTOM_RIGHT];
        }
    }

    public float Bottom
    {
        get
        {
            return BottomLeft.y;
        }
    }

    public float Left
    {
        get
        {
            return BottomLeft.x;
        }
    }

    public float Top
    {
        get
        {
            return TopRight.y;
        }
    }

    public float Right
    {
        get
        {
            return TopRight.x;
        }
    }

    private static class Corners
    {
        public const int TOP_LEFT = 0;
        public const int TOP_RIGHT = 1;
        public const int BOTTOM_RIGHT = 2;
        public const int BOTTOM_LEFT = 3;
    }
}