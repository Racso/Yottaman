using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Transform _target;
	private Vector3 _offset;

	public void Follow(Transform target)
	{
		_target = target;
		_offset = _target.position - transform.position;
	}
	
	public void ManualUpdate()
	{
		transform.position = _target.position - _offset;
	}
}
