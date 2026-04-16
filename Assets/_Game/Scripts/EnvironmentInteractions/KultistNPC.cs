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
    if (!npcData) return;

    DialogueManager.Instance.StartDialogue(npcData.displayName, npcData.dialogJSON);
  }

  protected override void PlayInteractionSound()
  {
    // Play sound
  }
}
