using System;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class InkController
{
  public Story CurrentStory;

  // private Dictionary<string, Action<string>> _commandRegistry = new Dictionary<string, Action<string>>();
  private Dictionary<string, Action<string>> _commandRegistry;
  private string _currentDialoguePartner;

  private readonly string INK_FUNCTION_BIND_NAME = "ExecuteFunction";
  private readonly string INK_TAG_PLAYER_SPEAKS = "Player";


  public InkController()
  {
    _commandRegistry = new Dictionary<string, Action<string>>()
    {
        { "AddInventoryItem", (payload) => AddItemMock(payload) }
    };
  }

  public void InitNewStory(string dialoguePartner, TextAsset storyJSON)
  {
    CurrentStory = new Story(storyJSON.text);
    CurrentStory.BindExternalFunction(INK_FUNCTION_BIND_NAME, (Action<string, string>)ExecuteFunction);

    _currentDialoguePartner = dialoguePartner;
  }

  public NextDialogueLineData ContinueStory()
  {
    if (!CurrentStory.canContinue)
    {
      DismissCurrentStory();
      return null;
    }

    string nextLine = CurrentStory.Continue();
    string currentSpeaker = CurrentStory.currentTags.Contains(INK_TAG_PLAYER_SPEAKS) ? INK_TAG_PLAYER_SPEAKS : _currentDialoguePartner;

    return new NextDialogueLineData(nextLine, currentSpeaker);
  }

  private void ExecuteFunction(string commandName, string payload)
  {
    if (_commandRegistry.TryGetValue(commandName, out Action<string> action))
    {
      action.Invoke(payload);
    }
    else
    {
      Debug.LogWarning($"InkController.ExecuteFunction: Command '{commandName}' not found in registry.");
    }
  }

  private void DismissCurrentStory()
  {
    CurrentStory.UnbindExternalFunction(INK_FUNCTION_BIND_NAME);
    CurrentStory = null;
    _currentDialoguePartner = "";
  }


  private void AddItemMock(string payload)
  {
    Debug.Log("You received an item! It is: " + payload);
  }
}