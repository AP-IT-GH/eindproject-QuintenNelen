using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public Transform cameraPivot; // Reference to the empty GameObject acting as the camera's parent
    public Vector3 offset; // Offset from the camera pivot

    private void LateUpdate()
    {
        // Set the camera's position relative to the camera pivot
        transform.position = cameraPivot.position + offset;
        transform.rotation = cameraPivot.rotation;
    }
}
