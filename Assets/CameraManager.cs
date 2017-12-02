using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager> {

	public GameObject character;

	private CameraFollow follow;
	private CameraShaker shaker;

	void Start()
	{
		follow = GetComponent<CameraFollow> ();
		follow.Follow (character.transform);
		shaker = GetComponent<CameraShaker> ();
	}

	void LateUpdate()
	{
		follow.ManualUpdate ();
		shaker.ManualUpdate ();
	}

	public void Shake(float time)
	{
		shaker.Shake (time);
	}

}
