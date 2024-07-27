using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform cameraTransform; // The camera's transform
    private float backgroundHeight; // Height of the background sprite

    private void Start()
    {
        // If no camera is assigned, use the main camera
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Calculate the height of the background sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        backgroundHeight = spriteRenderer.bounds.size.y;
    }

    private void Update()
    {
        // Check if the background has moved out of the camera's view on the Y-axis
        if (transform.position.y + backgroundHeight < cameraTransform.position.y)
        {
            // Reposition the background to the top
            Vector3 newPos = transform.position;
            newPos.y += 2 * backgroundHeight;
            transform.position = newPos;
        }
        else if (transform.position.y - backgroundHeight > cameraTransform.position.y)
        {
            // Reposition the background to the bottom
            Vector3 newPos = transform.position;
            newPos.y -= 2 * backgroundHeight;
            transform.position = newPos;
        }
    }
}
