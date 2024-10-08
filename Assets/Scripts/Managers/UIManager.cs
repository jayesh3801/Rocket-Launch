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
    public GameObject pauseButton;
    public GameObject pausePanel;
    public GameObject shieldUI;

    private void Awake()
    {
        Instance = this;
        levelText.text = SceneManager.GetActiveScene().name.ToString();
    }

    private void OnEnable()
    {
        PowerUpManager.OnShieldDeactivated += OnShieldDeactivated;
    }

    private void OnDisable()
    {
        PowerUpManager.OnShieldDeactivated -= OnShieldDeactivated;
    }

    public void OnShieldPickedUp()
    {
        shieldUI.SetActive(true);
    }

    public void OnShieldDeactivated()
    {
        shieldUI.SetActive(false);
    }

    public void OnShieldButtonClicked()
    {
        PowerUpManager.Instance.StartShieldPowerUp();
    }

    public void LevelCompleted()
    {
        StartCoroutine(EnableUIwithDelay(0));
    }

    public void LevelFailed()
    {
        StartCoroutine(EnableUIwithDelay(1));
    }

    public void OnNextLevelClicked()
    {
        GameManager.Instance.LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnRetryLevelClicked()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
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

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }
}
