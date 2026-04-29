using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private Image timerImage;
    [SerializeField] private GameObject startUI;
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject endedUI;
    [SerializeField] private Button restartGameButton;

    private void Start()
    {
        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGameButtonHandler);
        else
            Debug.LogWarning($"{nameof(MenuUIController)}: `startGameButton` not assigned on {name}.", this);

        if (restartGameButton != null)
            restartGameButton.onClick.AddListener(StartGameButtonHandler);
        else
            Debug.LogWarning($"{nameof(MenuUIController)}: `restartGameButton` not assigned on {name}.", this);

        SetEndScreenVisibilty(false);
        SetStartScreenVisibilty(false);

        // if (GameManager.Instance.State == GameState.NotStarted) SetStartScreenVisibilty(true);
    }

    private void Update()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Playing:
                UpdateTimerUI();
                break;
            case GameState.Ended:
                SetEndScreenVisibilty(true);
                break;
        }
    }

    public void StartGameButtonHandler()
    {
        GameManager.Instance.StartGame();
        SetStartScreenVisibilty(false);
        SetEndScreenVisibilty(false);
        UpdateTimerUI();
    }

    private void SetStartScreenVisibilty(bool isVisible)
    {
        startUI.SetActive(isVisible);
    }

    private void SetEndScreenVisibilty(bool isVisible)
    {
        endedUI.SetActive(isVisible);
    }

    private void UpdateTimerUI()
    {
        if (timerImage == null) return;

        timerImage.fillAmount = GameManager.Instance.RemainingGameTime / GameManager.Instance.TotalGameTimeInSeconds;
    }
}
