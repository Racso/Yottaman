using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T: Component
{
	private static T _instance = default(T);

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this as T;
		}
		else
		{
			Destroy (this);
		}
	}

	public static T Instance
	{
		get
		{
			return _instance;
		}
	}
}
