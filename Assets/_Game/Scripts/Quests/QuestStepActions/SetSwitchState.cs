public class SetSwitchState : IQuestStepAction
{
  private GameSwitches _gameSwitch;
  private bool _state;

  public SetSwitchState(GameSwitches gameSwitch, bool state)
  {
    _gameSwitch = gameSwitch;
    _state = state;
  }

  public void Execute()
  {
    //
  }
}