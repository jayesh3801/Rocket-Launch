using UnityEngine;
using DG.Tweening;
using System.Collections;

public class RocketController : MonoBehaviour
{
    [SerializeField] private float maxDragDistance = 5f;  // Maximum distance the player can drag
    [SerializeField] private float launchForceMultiplier = 10f;  // Multiplier to convert drag distance to force
    [SerializeField] private float minScaleY = 0.5f;  // Minimum scale on the Y-axis before shaking starts
    [SerializeField] private float maxVelocity = 20f;  // Maximum velocity the rocket can reach
    [SerializeField] private float moveForce = 5f;  // Force to apply when moving in the direction of the tap

    private Vector2 dragStartPos;
    private Vector2 dragEndPos;
    private bool isDragging = false;
    private bool canDrag = true;
    private bool isLaunched = false;
    private Rigidbody2D rb;
    private Vector3 originalScale;

    [SerializeField] private GameObject speedLineVFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (canDrag && Input.GetMouseButtonDown(0))
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LaunchRocket();
            ResetScale();
            StartCoroutine(ShowSpeedLines());
            isDragging = false;
            canDrag = false;
            isLaunched = true;
        }

        if (isDragging)
        {
            Vector2 currentDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateScale(currentDragPos);
        }

        // Handle tap input for rotation after launch
        if (isLaunched && Input.GetMouseButtonDown(0))
        {
            Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tapPosition.z = 0; // Ensure z position is zero for 2D

            if (tapPosition.x < transform.position.x)
            {
                RotateAndMoveRocket(45f); 
            }
            else
            {
                RotateAndMoveRocket(-45f); 
            }
        }

        // Cap the rocket's velocity to maxVelocity
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    void LaunchRocket()
    {
        Vector2 dragVector = dragEndPos - dragStartPos;

        // Limit the drag distance to maxDragDistance
        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        // Calculate the launch force based on the drag distance
        float launchForce = dragVector.magnitude * launchForceMultiplier;

        // Apply the force to the rocket's Rigidbody2D
        rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
    }

    void UpdateScale(Vector2 currentDragPos)
    {
        Vector2 dragVector = currentDragPos - dragStartPos;

        // Limit the drag distance to maxDragDistance
        if (dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }

        // Calculate the new scale on the Y-axis
        float scaleY = Mathf.Lerp(originalScale.y, minScaleY, dragVector.magnitude / maxDragDistance);

        if (scaleY > minScaleY)
        {
            transform.DOScaleY(scaleY, 0.1f);
        }
    }

    void ResetScale()
    {
        // Reset the scale back to the original
        transform.localScale = originalScale;
    }

    private IEnumerator ShowSpeedLines()
    {
        speedLineVFX.SetActive(true);
        yield return new WaitForSeconds(1);
        speedLineVFX.SetActive(false);
    }

    private void RotateAndMoveRocket(float angle)
    {
        float currentZRotation = transform.eulerAngles.z;
        if (currentZRotation > 180)
        {
            currentZRotation -= 360;
        }

        float targetZRotation = currentZRotation + angle;

        // Clamp the target rotation to -45 and 45 degrees
        targetZRotation = Mathf.Clamp(targetZRotation, -45f, 45f);

        // If target rotation is same as current rotation, return without rotating
        if (Mathf.Approximately(targetZRotation, currentZRotation))
        {
            return;
        }

        // Rotate the rocket
        transform.DORotate(Vector3.forward * targetZRotation, 0.5f, RotateMode.Fast).OnComplete(() =>
        {
            // Calculate the direction to move based on the new rotation
            Vector2 moveDirection = transform.up; // The up vector after rotation is the new forward direction

            // Apply force in the direction of the new rotation
            rb.AddForce(moveDirection * moveForce, ForceMode2D.Impulse);
        });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Goal"))
        {
            rb.velocity = Vector2.zero;
            UIManager.Instance.LevelCompleted();
            Debug.Log("YOU WON");
        }
        if (collision.collider.CompareTag("Obstacle"))
        {
            rb.velocity = Vector2.zero;
            UIManager.Instance.LevelFailed();
            Destroy(gameObject);
            Debug.Log("YOU LOOSE");
        }
    }
}
