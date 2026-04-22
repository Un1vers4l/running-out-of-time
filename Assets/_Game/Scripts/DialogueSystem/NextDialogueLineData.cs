using System.Collections.Generic;
public record NextDialogueLineData
{
  public NextDialogueLineData(string text, string speaker, List<string> choices)
  {
    Text = text;
    Speaker = speaker;
    Choices = choices;
  }
  public string Text { get; }
  public string Speaker { get; }
  public List<string> Choices { get; }
}
