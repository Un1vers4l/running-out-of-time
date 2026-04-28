using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]

[RequireComponent(typeof(Animator))]
public class KultistNPC : InteractableBase
{
  [SerializeField] private NPC_Data npcData;

  private SpriteRenderer _spriteRenderer;
  private Animator _animator;
  private readonly string _lyingDownAnimationTriggerName = "isLyingDown";

  private bool _isLyingDown = false;

  void Awake()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _animator = GetComponent<Animator>();

    _spriteRenderer.color = npcData.robeColor;
  }

  protected override void ExecuteInteraction(GameObject interactionSource)
  {
    if (!npcData) return;

    DialogueManager.Instance.StartDialogue(npcData.displayName, npcData.dialogJSON);
  }

  private void SetIsLyingDown(bool isLyingDown)
  {
    _isLyingDown = isLyingDown;
    _animator.SetBool(_lyingDownAnimationTriggerName, isLyingDown);
  }

  protected override void PlayInteractionSound()
  {
    // Play sound
  }
}
