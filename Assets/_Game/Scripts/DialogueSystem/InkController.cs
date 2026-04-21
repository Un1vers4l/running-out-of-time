using System;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkController
{
  public Story CurrentStory;
  public event Action<string> OnAddItem;
  public event Action<string, bool> OnUpdateGameSwitch;
  public event Func<string, bool> RequestGameSwitchState;

  private readonly Dictionary<string, Action<string>> _actionRegistry;
  private readonly Dictionary<string, Func<string, object>> _queryRegistry;
  private string _playerSpeakerDisplayName = "Player";
  private string _defaultSpeakerDisplayName;
  private string _currentSpeakerTag;

  private readonly string INK_ACTION_BIND_NAME = "ExecuteAction";
  private readonly string INK_QUERY_BIND_NAME = "ExecuteQuery";
  private readonly string INK_TAG_PLAYER_SPEAKER = "Speaker_Player";
  private readonly string INK_TAG_DEFAULT_SPEAKER = "Speaker_Default";

  public InkController()
  {
    _actionRegistry = new Dictionary<string, Action<string>>()
    {
      { "AddInventoryItem", (payload) => OnAddItem?.Invoke(payload) },
      { "SetGameSwitchTrue", (payload) => OnUpdateGameSwitch?.Invoke(payload, true) },
    };

    _queryRegistry = new Dictionary<string, Func<string, object>>()
    {
      { "GetGameSwitchState", (payload) => RequestGameSwitchState?.Invoke(payload) },
    };

    _currentSpeakerTag = INK_TAG_DEFAULT_SPEAKER;
  }

  public void InitNewStory(string dialoguePartner, TextAsset storyJSON)
  {
    CurrentStory = new Story(storyJSON.text);
    CurrentStory.BindExternalFunction(INK_ACTION_BIND_NAME, (Action<string, string>)ExecuteActionHandler);
    CurrentStory.BindExternalFunction(INK_QUERY_BIND_NAME, (string commandName, string payload) => { return ExecuteQueryHandler(commandName, payload); });

    _defaultSpeakerDisplayName = dialoguePartner;
  }

  public NextDialogueLineData ContinueStory()
  {
    if (!CurrentStory.canContinue)
    {
      DismissCurrentStory();
      return null;
    }

    string nextLine = CurrentStory.Continue();
    string speakerTag = CurrentStory.currentTags.Find(tag => tag == INK_TAG_PLAYER_SPEAKER || tag == INK_TAG_DEFAULT_SPEAKER);
    if (speakerTag != null)
    {
      _currentSpeakerTag = speakerTag;
    }

    string currentSpeaker = _currentSpeakerTag == INK_TAG_PLAYER_SPEAKER ? _playerSpeakerDisplayName : _defaultSpeakerDisplayName;

    return new NextDialogueLineData(nextLine, currentSpeaker);
  }

  private void ExecuteActionHandler(string commandName, string payload)
  {
    if (_actionRegistry.TryGetValue(commandName, out Action<string> action))
    {
      action.Invoke(payload);
    }
    else
    {
      Debug.LogWarning($"InkController.ExecuteActionHandler: Command '{commandName}' not found in registry.");
    }
  }

  private object ExecuteQueryHandler(string commandName, string payload)
  {
    if (_queryRegistry.TryGetValue(commandName, out Func<string, object> query))
    {
      return query.Invoke(payload);
    }
    else
    {
      Debug.LogWarning($"InkController.ExecuteQueryHandler: Command '{commandName}' not found in registry.");
    }

    return null;
  }

  private void DismissCurrentStory()
  {
    CurrentStory.UnbindExternalFunction(INK_ACTION_BIND_NAME);
    CurrentStory.UnbindExternalFunction(INK_QUERY_BIND_NAME);
    CurrentStory = null;
    _defaultSpeakerDisplayName = "";
    _currentSpeakerTag = "";
  }
}