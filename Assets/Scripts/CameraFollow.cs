using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Transform _target;
	private Vector3 _offset;

	public void FollowWithCurrentOffset(Transform target)
	{
		_target = target;
		_offset = _target.position - transform.position;
	}

    public void FollowAndCenter(Transform target)
    {
        _target = target;
        _offset = new Vector3(0, 0 , target.transform.position.z-transform.position.z);
    }
	
	public void ManualUpdate()
	{
		transform.position = _target.position - _offset;
	}
}
