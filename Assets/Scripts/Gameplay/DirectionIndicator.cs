using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    private Transform nearestFuel;
    private Transform nearestStar;

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
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = obj.transform;
            }
        }

        return nearestObject;
    }

    private void UpdateIndicators()
    {
       
    }

}
