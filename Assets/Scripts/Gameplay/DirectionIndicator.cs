using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fuelIndicatorSprite;
    [SerializeField] private SpriteRenderer starIndicatorSprite;
    [SerializeField] private float indicatorRange = 30f;
    [SerializeField] private Camera mainCamera;

    private Transform nearestFuel;
    private Transform nearestStar;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        FindNearestItems();
        UpdateIndicators();
    }

    private void FindNearestItems()
    {
        nearestFuel = FindNearestTaggedObject("Fuel");
        nearestStar = FindNearestTaggedObject("Star");
    }

    private Transform FindNearestTaggedObject(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        Transform nearestObject = null;
        float minDistance = float.MaxValue;

        foreach (GameObject obj in taggedObjects)
        {
            float distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < minDistance && distance <= indicatorRange)
            {
                minDistance = distance;
                nearestObject = obj.transform;
            }
        }

        return nearestObject;
    }

    private void UpdateIndicators()
    {
        UpdateIndicator(fuelIndicatorSprite, nearestFuel);
        UpdateIndicator(starIndicatorSprite, nearestStar);
    }

    private void UpdateIndicator(SpriteRenderer indicator, Transform target)
    {
        if (target != null)
        {
            // Convert the target position to screen space
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position);

            // Convert the screen position to viewport position
            Vector3 viewportPosition = mainCamera.ScreenToViewportPoint(screenPosition);

            // Clamp the viewport position to keep the indicator within the screen edges
            viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f);
            viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f);

            // Convert the clamped viewport position back to screen position
            Vector3 clampedScreenPosition = mainCamera.ViewportToScreenPoint(viewportPosition);

            // Convert the clamped screen position to world position
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(clampedScreenPosition.x, clampedScreenPosition.y, mainCamera.nearClipPlane + 1));

            // Set the indicator's position
            indicator.transform.position = new Vector3(worldPosition.x, worldPosition.y, indicator.transform.position.z);

            // Rotate the indicator to face the target
            Vector2 direction = (target.position - indicator.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            indicator.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Ensure the indicator is visible
            indicator.enabled = true;
        }
        else
        {
            // Hide the indicator if no target is found
            indicator.enabled = false;
        }
    }
}
