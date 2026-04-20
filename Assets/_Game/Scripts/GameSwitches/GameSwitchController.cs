using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSwitchController : MonoBehaviour
{
  private readonly Dictionary<GameSwitches, bool> _switches;

  void Start()
  {
    DialogueManager.Instance.InkController.OnUpdateGameSwitch += HandleSetSwitchStateFromInk;
    DialogueManager.Instance.InkController.RequestGameSwitchState += HandleGetSwitchStateFromInk;
  }

  void OnDisable()
  {
    DialogueManager.Instance.InkController.OnUpdateGameSwitch -= HandleSetSwitchStateFromInk;
    DialogueManager.Instance.InkController.RequestGameSwitchState -= HandleGetSwitchStateFromInk;
  }


  public GameSwitchController()
  {
    _switches = new Dictionary<GameSwitches, bool>();
    foreach (GameSwitches value in Enum.GetValues(typeof(GameSwitches)))
    {
      _switches[value] = false;
    }
  }

  public bool GetSwitchState(GameSwitches key)
  {
    return _switches[key];
  }

  public void SetSwitchState(GameSwitches key, bool state)
  {
    _switches[key] = state;
  }

  private bool HandleGetSwitchStateFromInk(string key)
  {
    if (!Enum.TryParse(key, out GameSwitches switchKey))
    {
      throw new ArgumentException($"GameSwitchController.HandleGetSwitchStateFromInk: Unknown switch name '{key}'");
    }
    Debug.Log($"Get State for key '{key}': ${GetSwitchState(switchKey)}");

    return GetSwitchState(switchKey);
  }

  private void HandleSetSwitchStateFromInk(string key, bool state)
  {
    if (!Enum.TryParse(key, out GameSwitches switchKey))
    {
      Debug.LogWarning($"GameSwitchController.HandleSetSwitchStateFromInk: Unknown switch name {key}");
      return;
    }
    Debug.Log($"Set State for key '{key}' to '${state}");

    SetSwitchState(switchKey, state);
  }
}