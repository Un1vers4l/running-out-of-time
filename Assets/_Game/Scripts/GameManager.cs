using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }
  [System.NonSerialized] public GameSwitchController SwitchController;

  void Awake()
  {
    if (Instance == null) Instance = this; else Destroy(gameObject);

    SwitchController = gameObject.AddComponent<GameSwitchController>();
  }
}