using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

	private float _shakeTime = 0;
	private float _shakeMaxDistance = 15f;
	private int _shakeEveryFrames = 1;
	private Vector3 _cameraInitialPosition;
	private Vector3 _lastCameraPosition;
	private int _currentFrame = 0;

	public void Shake(float time)
	{
		_shakeTime = time;
		this.enabled = true;
	}

	private void OnEnable()
	{
		_cameraInitialPosition = transform.position;
	}

	private void OnDisable()
	{
		transform.position = _cameraInitialPosition;
	}
		
	public void ManualUpdate()
	{
		_shakeTime -= Time.deltaTime;
		if (_shakeTime <= 0)
		{
			this.enabled = false;
			return;
		}

		if (_lastCameraPosition != transform.position)
		{
			_cameraInitialPosition = transform.position;
		}

		_currentFrame = (_currentFrame + 1) % _shakeEveryFrames;

		if (_currentFrame > 0) {
			return;
		}

		transform.position = _cameraInitialPosition + (Vector3)Random.insideUnitCircle * _shakeMaxDistance;
		_lastCameraPosition = transform.position;

	}
}
