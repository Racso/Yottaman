using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioBounds : Singleton<ScenarioBounds>
{

    public List<Transform> Corners;

    public float Left
    {
        get
        {
            return Mathf.Min(Corners[0].position.x, Corners[1].position.x);
        }
    }

    public float Right
    {
        get
        {
            return Mathf.Max(Corners[0].position.x, Corners[1].position.x);
        }
    }

    public float Top
    {
        get
        {
            return Mathf.Max(Corners[0].position.y, Corners[1].position.y);
        }
    }

    public float Bottom
    {
        get
        {
            return Mathf.Min(Corners[0].position.y, Corners[1].position.y);
        }
    }

    public Transform TopLeft
    {
        get
        {
            return Corners[0];
        }
    }

    public Transform TopRight
    {
        get
        {
            return Corners[1];
        }
    }

    public Transform BottomLeft
    {
        get
        {
            return Corners[2];
        }
    }

    public Transform BottomRight
    {
        get
        {
            return Corners[3];
        }
    }

}