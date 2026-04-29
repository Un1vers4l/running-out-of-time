using UnityEngine;

public enum GameState
{
  NotStarted,
  Playing,
  Paused,
  Ended
}

public class GameManager : MonoBehaviour
{
  [Header("Timer")]
  public float TotalGameTimeInSeconds = 15 * 60; // minutes * seconds

  public static GameManager Instance { get; private set; }
  public GameState State { get; private set; }
  [System.NonSerialized] public GameSwitchController SwitchController;
  [System.NonSerialized] public float RemainingGameTime;

  void Awake()
  {
    if (Instance == null) Instance = this; else Destroy(gameObject);

    SwitchController = gameObject.AddComponent<GameSwitchController>();
    State = GameState.NotStarted;
    StartGame();
  }

  void Update()
  {
    if (State == GameState.Playing)
    {
      RemainingGameTime -= Time.deltaTime;

      if (RemainingGameTime <= 0f)
      {
        RemainingGameTime = 0f;
        State = GameState.Ended;
      }
    }
  }

  public void StartGame()
  {
    RemainingGameTime = TotalGameTimeInSeconds;
    State = GameState.Playing;
  }
}