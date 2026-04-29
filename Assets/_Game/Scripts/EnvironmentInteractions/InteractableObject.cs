using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractableObject : InteractableBase
{
  [SerializeField] private ItemData itemData;

  private SpriteRenderer _spriteRenderer;
  private Animator _animator;

  void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    if (!itemData) return;

    if (itemData.spriteToRender)
    {
      _spriteRenderer.sprite = itemData.spriteToRender;
    }

    if (itemData.animator)
    {
      // TODO: check if this actually works like this
      _animator = itemData.animator;
    }
  }

  protected override void ExecuteInteraction(GameObject instigator)
  {
    if (!itemData) return;

    DialogueManager.Instance.StartDialogue("", itemData.dialogueJSON);
  }

  protected override void PlayInteractionSound()
  {
    // Play sound
  }
}