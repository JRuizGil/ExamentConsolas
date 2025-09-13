using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -7);
    public float step = 0.5f;

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        Vector3 diff = desiredPosition - transform.position;
        if (diff.magnitude > step)
        {
            transform.position += diff.normalized * step;
        }
        else
        {
            transform.position = desiredPosition;
        }
    }
}