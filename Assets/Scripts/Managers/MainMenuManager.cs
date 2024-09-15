using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    Animator animator;
    public void OnInitialized()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Initialized", true);
    }

    public void OnPlayButtonClicked()
    {
        GameManager.Instance.LoadSceneByIndex(1);
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
