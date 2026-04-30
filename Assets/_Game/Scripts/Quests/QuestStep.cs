using System;

[Serializable]
public class QuestStep
{
  public IQuestStepAction[] actions;

  public QuestStep(params IQuestStepAction[] actions) => this.actions = actions;

  public void ExecuteActions()
  {
    foreach (var action in actions) action.Execute();
  }

}