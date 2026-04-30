using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
  private Dictionary<Quests, Dictionary<string, QuestStep>> _quests;

  void Awake()
  {
    InitializeQuests();
  }

  public void ExecuteQuestStep(string questId, string questStepId)
  {
    // TODO: Ensure questId is in Quests

    // if (_quests.TryGetValue(questId, out var questSteps) && questSteps.TryGetValue(questStepId, out var questStep))
    // {
    //   questStep.ExecuteActions();
    // }
    // else
    // {
    //   Debug.LogError($"QuestController.ExecuteQuestStep: Could not find questID '{questId}' or questStepId '{questStepId}'");
    // }
  }

  private void InitializeQuests()
  {
    _quests = new Dictionary<Quests, Dictionary<string, QuestStep>>();
    foreach (Quests quest in Enum.GetValues(typeof(Quests)))
    {
      _quests[quest] = GetStepsForQuest(quest);
    }
  }

  private Dictionary<string, QuestStep> GetStepsForQuest(Quests quest)
  {
    switch (quest)
    {
      case Quests.initial_interrogation:
        return DefineInitialInterrogationSteps();
      case Quests.cat_cultist:
        return DefineCatCultistSteps();
      case Quests.party_cultist:
        return DefinePartyCultistSteps();
      case Quests.shop_assistant:
        return DefineShopAssistantSteps();
      case Quests.chimney:
        return DefineChimneySteps();
      default:
        return new Dictionary<string, QuestStep>();
    }
  }

  private Dictionary<string, QuestStep> DefineInitialInterrogationSteps()
  {
    return new Dictionary<string, QuestStep>()
    {
      {
        "releaseCat", new QuestStep(
          new SetSwitchState(GameSwitches.ReleasedCat, true)
        )
      }
    };
  }

  private Dictionary<string, QuestStep> DefineCatCultistSteps()
  {
    return new Dictionary<string, QuestStep>()
    {
      {
        "releaseCat", new QuestStep(
          new SetSwitchState(GameSwitches.ReleasedCat, true)
        )
      }
    };
  }

  private Dictionary<string, QuestStep> DefinePartyCultistSteps()
  {
    return new Dictionary<string, QuestStep>()
    {
      {
        "releaseCat", new QuestStep(
          new SetSwitchState(GameSwitches.ReleasedCat, true)
        )
      }
    };
  }

  private Dictionary<string, QuestStep> DefineShopAssistantSteps()
  {
    return new Dictionary<string, QuestStep>()
    {
      {
        "releaseCat", new QuestStep(
          new SetSwitchState(GameSwitches.ReleasedCat, true)
        )
      }
    };
  }

  private Dictionary<string, QuestStep> DefineChimneySteps()
  {
    return new Dictionary<string, QuestStep>()
    {
      {
        "releaseCat", new QuestStep(
          new SetSwitchState(GameSwitches.ReleasedCat, true)
        )
      }
    };
  }
}