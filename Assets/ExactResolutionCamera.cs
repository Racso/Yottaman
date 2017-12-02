using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racso.Cameras
{
    public class ExactResolutionCamera : MonoBehaviour
    {
        public int X = 800;
        public int Y = 600;
        public int PixelsPerUnit = 100;

        public Color BackgroundFill = Color.black;

        private Camera cam;

        private void Start()
        {
            cam = GetComponent<Camera>();
            CreateEmptyCamera();
        }

        void Update()
        {
            int screenPxPerDesiredPx = Screen.height / Y;
            int realHeight = Y * screenPxPerDesiredPx;
            int realWidth = X * screenPxPerDesiredPx;

            float unitsY = (float)Y / PixelsPerUnit;
            cam.orthographicSize = unitsY / 2f;

            float viewportY = (float)realHeight / Screen.height;
            float viewPortX = (float)realWidth / Screen.width;
            //viewPortX = viewPortX * viewportY;

            var newRect = cam.rect;
            newRect.width = viewPortX;
            newRect.height = viewportY;
            newRect.center = new Vector2(0.5f, 0.5f);
            cam.rect = newRect;

        }

        private void CreateEmptyCamera()
        {
            var gameObject = new GameObject("backgroundCamera");
            var cam = gameObject.AddComponent<Camera>();
            var originalDepth = Camera.main.depth;
            cam.depth = originalDepth - 1;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = BackgroundFill;
            cam.cullingMask = 0;
        }
    }
}