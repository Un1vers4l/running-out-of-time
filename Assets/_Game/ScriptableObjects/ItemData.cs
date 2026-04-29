using UnityEngine;

[CreateAssetMenu(menuName = "Sriptable Objects/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public Animator animator;
    public TextAsset dialogueJSON;
}
