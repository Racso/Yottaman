using UnityEngine;
using System.Collections;

public class RotateTowardsTarget : MonoBehaviour
{
    public Transform HotPoint;
    private GameObject currentTarget;

    public void SetTargetAndShow(GameObject Target)
    {
        currentTarget = Target;
        gameObject.SetActive(true);
        enabled = true;
    }

    public void StopTargetingAndHide()
    {
        StopTargeting();
        gameObject.SetActive(false);
    }

    public void StopTargeting()
    {
        enabled = false;
    }

    void Update()
    {
        var targetCurrentPosition = currentTarget.TargetCenter();
        gameObject.transform.right = (targetCurrentPosition - transform.position).normalized * Mathf.Sign(targetCurrentPosition.x - transform.position.x);
    }
}
