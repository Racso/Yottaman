using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianHealth : Health {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Hit(int damage)
    {   
        Destroy(gameObject);
    }
}
