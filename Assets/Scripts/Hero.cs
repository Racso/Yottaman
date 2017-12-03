using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Singleton<Hero> {

    private Collider2D _collider;
    public Animator _anim;

    public float FlightSpeed;
    public LayerMask SolidLayers;
    public int Level = 0;

	void Start () {
        _collider = GetComponent<Collider2D>();
        _anim = GetComponentInChildren<Animator>();
	}
	
	void Update () {
        var movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * FlightSpeed * Time.deltaTime;

        //Evitar incrustarse en sólidos.
        var hit = Physics2D.BoxCast(_collider.bounds.center + (Vector3)movement.normalized * 0.1f, _collider.bounds.size, 0, movement.normalized, movement.magnitude, SolidLayers);
        if (hit)
        {
            var movimientoOK = hit.distance;
            movement = movement.normalized * movimientoOK;
        }

        _anim.SetBool("flying", !movement.IsCloseEnoughTo(Vector3.zero));
        transform.Translate(movement);

        transform.localScale = transform.LocalScaleLookingTowards(HeroSkill.PointerPosition());

	}

}
