using UnityEngine;
using System.Collections;

public class RotateTowardsTarget : MonoBehaviour
{

    public Transform HotPoint;
    private GameObject _target;
    private Collider2D _targetCollider;

    public bool Paused;

    public void SetTarget(GameObject Target)
    {
        _target = Target;
        if (Target != null) { _targetCollider = Target.GetComponent<Collider2D>(); }
        gameObject.SetActive(Target != null);
        Paused = false;
    }

    void Update()
    {
        if (_target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        if (Paused) { return; }
        var otherPosition = _target.TargetCenter(); //_targetCollider != null ? _targetCollider.bounds.center : _target.transform.position;
        gameObject.transform.right = (otherPosition - transform.position).normalized * Mathf.Sign(otherPosition.x - transform.position.x);
    }
}
