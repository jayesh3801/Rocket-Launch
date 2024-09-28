using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;        // Reference to the Text object for the instruction
    public Image handImage;          // Reference to the hand image in the UI
    public Vector2 startPosition;    // Start position for the hand (for Drag tutorial)
    public Vector2 endPosition;      // End position for the hand (for Drag tutorial)
    public float rotationAngle = 45f; // Rotation angle for the hand (for Rotate tutorial)
    public static TutorialController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Start the "Drag to Launch" tutorial
        ShowDragToLaunchTutorial();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            handImage.gameObject.SetActive(false);
            tutorialText.gameObject.SetActive(false);
            handImage.rectTransform.DOKill();
        }
    }

    private void ShowDragToLaunchTutorial()
    {
        // Set the instruction text for Drag tutorial
        tutorialText.text = "Drag to Launch";

        // Reset hand image position
        handImage.rectTransform.anchoredPosition = startPosition;

        // Animate the hand movement using DOTween
        AnimateHandDrag();
    }

    private void AnimateHandDrag()
    {
        // Animate hand movement from startPosition to endPosition in a loop
        handImage.rectTransform.DOAnchorPos(endPosition, 1f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);  // Loop infinitely with Yoyo (back and forth)
    }

    public void ShowTapToRotateTutorial()
    {
        // Set the instruction text for Tap tutorial
        tutorialText.text = "Tap to Rotate";
        tutorialText.gameObject.SetActive(true);
        handImage.gameObject.SetActive(true);
        // Reset hand image position to center (or any position you prefer for tap tutorial)
        handImage.rectTransform.anchoredPosition = startPosition + new Vector2(0,-100); // You can change position if needed

        // Animate the hand rotating around its Z-axis
        AnimateHandRotate();
    }

    private void AnimateHandRotate()
    {
        // Reset rotation before applying the animation
        handImage.rectTransform.rotation = Quaternion.Euler(0, 0, 0);

        // Animate the hand rotating between 0 and rotationAngle degrees in a loop
        handImage.rectTransform.DORotate(new Vector3(0, 0, rotationAngle), 0.5f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);  // Loop infinitely with Yoyo (rotate back and forth)
    }
}
