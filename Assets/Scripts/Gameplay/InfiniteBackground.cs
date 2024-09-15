using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform cameraTransform;
    private float backgroundHeight; 

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        backgroundHeight = spriteRenderer.bounds.size.y;
    }

    private void Update()
    {
        if (transform.position.y + backgroundHeight < cameraTransform.position.y)
        {
            Vector3 newPos = transform.position;
            newPos.y += 2 * backgroundHeight;
            transform.position = newPos;
        }
        else if (transform.position.y - backgroundHeight  > cameraTransform.position.y)
        {
            Vector3 newPos = transform.position;
            newPos.y -= 2 * backgroundHeight;
            transform.position = newPos;
        }
    }
}
