using TMPro;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;
    private int collectedStars = 0;
    int requiredStars = 3;
    [SerializeField] private TextMeshProUGUI starCounter;

    private void Awake()
    {
        Instance = this;
        collectedStars = PlayerPrefs.GetInt("Stars", 0);
        starCounter.text = collectedStars.ToString();
    }

    public void CollectStar(GameObject star)
    {
        collectedStars++;
        starCounter.text = collectedStars.ToString();
        PlayerPrefs.SetInt("Stars", collectedStars);
        PlayerPrefs.Save();
        Destroy(star);
        Debug.Log("Star Collected! Total Stars: " + collectedStars);
    }
}
