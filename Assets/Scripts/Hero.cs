using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Singleton<Hero> {

    public float FlightSpeed = 600;
    public int Level = 0;

    private new Collider2D collider;
    private Animator animator;

	void Start ()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
	}
	
	void Update ()
    {
        UpdatePositionWithAnimation();
        transform.FlipXToLookTo(HeroSkill.PointerPosition());
        ClampPositionToScenario();
	}

    private void UpdatePositionWithAnimation()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * FlightSpeed * Time.deltaTime;
        transform.Translate(movement);

        bool AmIMoving = !movement.IsCloseEnoughTo(Vector3.zero);
        animator.SetBool("flying", AmIMoving);
    }

    private void ClampPositionToScenario()
    {
        float clampedX = Mathf.Clamp(transform.position.x, ScenarioBounds.Instance.Left, ScenarioBounds.Instance.Right);
        float clampedY = Mathf.Clamp(transform.position.y, ScenarioBounds.Instance.Bottom, ScenarioBounds.Instance.Top);
        float clampedZ = transform.position.z;

        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }

    public void UpdateSkillAnimation(string skillName, bool animation)
    {
        animator.SetBool(skillName, animation);
    }

}
