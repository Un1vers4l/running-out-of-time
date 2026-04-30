public class AddItemToInventory : IQuestStepAction
{
  private string _itemName;

  public AddItemToInventory(string itemName)
  {
    _itemName = itemName;
  }

  public void Execute()
  {
    //
  }
}