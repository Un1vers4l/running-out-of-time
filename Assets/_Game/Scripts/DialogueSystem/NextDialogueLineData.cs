public record NextDialogueLineData
{
  public NextDialogueLineData(string text, string speaker)
  {
    Text = text;
    Speaker = speaker;
  }
  public string Text { get; }
  public string Speaker { get; }
}
