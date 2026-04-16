using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private TMP_Text timerText;          // Always shown
    [SerializeField] private GameObject startUI;         // Shown at beginning (before first start)
    [SerializeField] private Button startGameButton;      // Start Game button
    [SerializeField] private GameObject endedUI;         // Shown when timer reaches 0
    [SerializeField] private Button restartGameButton;  // Restart button

    public GameState State { get; private set; }

    private float remainingSeconds;
    private bool startedOnce; // Distinguishes "ended because not started yet" vs "ended after timer runout"

    private void Awake()
    {
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
                startedOnce = true; // now we consider it a real "ended"
                ApplyUI();
            }

            UpdateTimerUI();
        }
        else
        {
            // Timer is always visible; when ended it stays showing 0 (or whatever it last was)
            UpdateTimerUI();
        }
    }

    // Public so Unity UI Button `OnClick` can be wired in the Inspector too.
    public void StartGame()
    {
        remainingSeconds = gameDurationSeconds;
        State = GameState.Playing;
        startedOnce = true;

        EnsureTimerFontAsset();
        ApplyUI();
        UpdateTimerUI();
    }

    // Public so Unity UI Button `OnClick` can be wired in the Inspector too.
    public void RestartGame()
    {
        remainingSeconds = gameDurationSeconds;
        State = GameState.Playing;

        EnsureTimerFontAsset();
        ApplyUI();
        UpdateTimerUI();
    }

    private void ApplyUI()
    {
        bool showStart = (State == GameState.Ended) && !startedOnce;
        bool showEnded = (State == GameState.Ended) && startedOnce;

        if (startUI != null) startUI.SetActive(showStart);
        if (endedUI != null) endedUI.SetActive(showEnded);

        // (No special UI changes needed when Playing; timer stays visible)
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        EnsureTimerFontAsset();

        int totalSeconds = Mathf.Max(0, Mathf.CeilToInt(remainingSeconds));
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void EnsureTimerFontAsset()
    {
        if (timerText == null) return;

        // TextMeshPro needs an assigned font asset to generate its mesh.
        if (timerText.font != null) return;

        var defaultFont = TMP_Settings.defaultFontAsset;
        if (defaultFont != null)
        {
            timerText.font = defaultFont;
            return;
        }

    }
}