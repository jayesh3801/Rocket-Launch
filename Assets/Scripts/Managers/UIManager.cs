using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI headingText;
    public TextMeshProUGUI levelText;
    public GameObject nextLevelbutton;
    public GameObject retryLevelbutton;

    private void Awake()
    {
        Instance = this;
       levelText.text =  SceneManager.GetActiveScene().name.ToString();
    }

    public void LevelCompleted()
    {
        StartCoroutine(EnableUIwithDelay(0));
    }

    public void LevelFailed()
    {
        StartCoroutine(EnableUIwithDelay(1));
    }

    public void OnNextLevelCliked()
    {
        GameManager.Instance.LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnRetryLevelCliked()
    {
        GameManager.Instance.LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator EnableUIwithDelay(int state)
    {
        yield return new WaitForSeconds(2);
        if (state == 0)
        {
            LevelCompletedLogic();
        }
        else if (state == 1)
        {
            LevelFailedLogic();
        }
    }

    private void LevelCompletedLogic()
    {
        headingText.gameObject.SetActive(true);
        headingText.text = "Level Completed!";
        nextLevelbutton.SetActive(true);
    }

    private void LevelFailedLogic()
    {
        headingText.gameObject.SetActive(true);
        headingText.text = "Level Failed";
        retryLevelbutton.SetActive(true);
    }
}
