using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager> {

	public GameObject character;

	public CameraFollow follow;
	private CameraShaker shaker;

    public bool IsFollowing = true;

	void Start()
	{
		follow = GetComponent<CameraFollow> ();
		follow.FollowWithCurrentOffset (character.transform);
		shaker = GetComponent<CameraShaker> ();
	}

	void LateUpdate()
	{
        if (IsFollowing) { follow.ManualUpdate(); }
		shaker.ManualUpdate ();
	}

	public void Shake(float time)
	{
		shaker.Shake (time);
	}


}
