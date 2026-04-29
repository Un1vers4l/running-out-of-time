using UnityEngine;

[CreateAssetMenu(menuName = "Sriptable Objects/NPC Data")]
public class NPC_Data : ScriptableObject
{
  public string displayName;
  public Color robeColor;
  public TextAsset dialogueJSON;
}