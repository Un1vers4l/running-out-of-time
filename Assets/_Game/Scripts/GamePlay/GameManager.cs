using UnityEngine;
using UnityEngine.UI;

public class GameTimerStateMachine : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Ended
    }

    [Header("Timer")]
    [SerializeField] private float gameDurationSeconds = 30f;

    [Header("UI (assign in Inspector)")]
    [SerializeField] private Image timerImage;
    [SerializeField] private GameObject startUI;
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject endedUI;
    [SerializeField] private Button restartGameButton;

    public static GameTimerStateMachine Instance { get; private set; }

    public GameState State { get; private set; }

    private float remainingSeconds;
    private bool startedOnce;

    private void Awake()
    {
        Instance = this;
        remainingSeconds = gameDurationSeconds;
    }

    private void Start()
    {
        State = GameState.Ended;
        startedOnce = false;

        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);
        else
            Debug.LogWarning($"{nameof(GameTimerStateMachine)}: `startGameButton` not assigned on {name}.", this);

        if (restartGameButton != null)
            restartGameButton.onClick.AddListener(RestartGame);
        else
            Debug.LogWarning($"{nameof(GameTimerStateMachine)}: `restartGameButton` not assigned on {name}.", this);

        ApplyUI();
        UpdateTimerUI();
    }

    private void Update()
    {
        if (State == GameState.Playing)
        {
            remainingSeconds -= Time.deltaTime;

            if (remainingSeconds <= 0f)
            {
                remainingSeconds = 0f;
                State = GameState.Ended;
                startedOnce = true;
                ApplyUI();
            }

            UpdateTimerUI();
        }
        else
        {
            UpdateTimerUI();
        }
    }

    public void StartGame()
    {
        remainingSeconds = gameDurationSeconds;
        State = GameState.Playing;
        startedOnce = true;
        ApplyUI();
        UpdateTimerUI();
    }

    public void RestartGame()
    {
        remainingSeconds = gameDurationSeconds;
        State = GameState.Playing;
        ApplyUI();
        UpdateTimerUI();
    }

    private void ApplyUI()
    {
        bool showStart = (State == GameState.Ended) && !startedOnce;
        bool showEnded = (State == GameState.Ended) && startedOnce;

        if (startUI != null) startUI.SetActive(showStart);
        if (endedUI != null) endedUI.SetActive(showEnded);
    }

    private void UpdateTimerUI()
    {
        if (timerImage == null) return;

        timerImage.fillAmount = remainingSeconds / gameDurationSeconds;
    }
}
