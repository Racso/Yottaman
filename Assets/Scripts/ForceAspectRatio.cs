using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour {

    public bool IntegerWidth = true;

    public float targetWidth = 16f;
    public float targetHeight = 9f;

    public Color backgroundColor;
    private const string backgroundCamera = "backgroundCamera";

    // Use this for initialization
    void Start()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = targetWidth / targetHeight;

        // determine the game window's current aspect ratio
        float windowaspect = Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        var verticalUnits = camera.orthographicSize * 2;
        var horizontalUnits = verticalUnits * windowaspect;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }

        if (GameObject.Find(backgroundCamera) ==null) CreateEmptyCamera();
    }


    private void CreateEmptyCamera()
    {
        var gameObject = new GameObject(backgroundCamera);
        var cam = gameObject.AddComponent<Camera>();
        var originalDepth = Camera.main.depth;
        cam.depth = originalDepth - 1;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = backgroundColor;
        cam.cullingMask = 0;
    }
}
