using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance { get; private set; }
    private int collectedStars = 0;
    int requiredStars = 3;

    private void Awake()
    {
        Instance = this;
    }

    public void CollectStar(GameObject star)
    {
        collectedStars++;
        Destroy(star);
        Debug.Log("Star Collected! Total Stars: " + collectedStars);
    }

    public int GetCollectedStars()
    {
        return collectedStars;
    }

    public void ResetCollectedStars()
    {
        collectedStars = 0;
    }

    public bool HasEnoughStars()
    {
        return collectedStars >= requiredStars;
    }
}
