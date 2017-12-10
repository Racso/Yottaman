using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

    [System.Serializable]
    public class ParallaxLayer
    {
        public GameObject GameObject;
        [Range(-100,100)]
        public int MovementX;
        [Range(-100, 100)]
        public int MovementY;
    }

    public GameObject Camera;
    public List<ParallaxLayer> Layers;

	void Start () {
		
	}
	
	void LateUpdate ()
    {
        Vector3 camDiff = Camera.transform.position-transform.position;
        foreach (var layer in Layers)
        {
            layer.GameObject.transform.position = transform.position - new Vector3(camDiff.x * layer.MovementX/100f, camDiff.y * layer.MovementY/100f, 0);
        }
	}
}
