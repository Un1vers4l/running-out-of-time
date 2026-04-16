using UnityEngine;

public class KultistNPC : InteractableBase
{
  [SerializeField] private NPC_Data npcData;

  private SpriteRenderer _spriteRenderer;

  void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _spriteRenderer.color = npcData.robeColor;
  }

  protected override void ExecuteInteraction(GameObject interactionSource)
  {
    // something something DialogController.startDialogue()
    Debug.Log("Interacted with " + npcData?.displayName);
  }

  protected override void PlayInteractionSound()
  {
    // Play sound
  }
}
