using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target for the camera to follow
    public float smoothSpeed = 0.125f; // The speed of the smooth transition
    public Vector3 offset; // The offset of the camera from the target

    void FixedUpdate()
    {
        if (target == null) return;

        // Desired position of the camera
        Vector3 desiredPosition = target.position + offset;
        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
