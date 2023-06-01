

using UnityEngine;

public class ballfolower : MonoBehaviour
{ 
    public Transform target; // The transform of the object to follow (tennis racket)
    public Transform lookAtTarget; // The transform of the object to look at (ball)
    public Vector3 offset = new Vector3(0f, 2f, -10f); // The offset from the target's position
    public float smoothSpeed = 0.125f; // The smoothing factor for the camera movement

    private void LateUpdate()
    {
        if (target == null || lookAtTarget == null)
        {
            return; // Exit if the target or look-at target is not set
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(lookAtTarget);
    }
}
