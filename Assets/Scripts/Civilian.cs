using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {

    public Vector3 FinishPoint;

    public float MinRunningSpeed = 8f;
    public float MaxRunningSpeed = 20f;

    private float RunningSpeed;

    private float zero = 0.1f;

	void Start () {
        RunningSpeed = Random.Range(MinRunningSpeed, MaxRunningSpeed);
	}
	
	void Update () {
        var direction = FinishPoint - transform.position;
        if (direction.magnitude <= zero)
        {
            Destroy(gameObject);
        }
        transform.Translate(direction.normalized * RunningSpeed * Time.deltaTime);
	}
}
