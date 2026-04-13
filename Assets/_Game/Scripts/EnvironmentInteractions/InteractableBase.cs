using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
  public void Interact(GameObject interactionSource)
  {
    ExecuteInteraction(interactionSource);
    PlayInteractionSound();
  }

  private static readonly Color _highlightColor = new Color32(173, 216, 200, 255);
  private readonly System.Collections.Generic.Dictionary<SpriteRenderer, Color> _defaultSpriteColors
    = new System.Collections.Generic.Dictionary<SpriteRenderer, Color>();

  // TODO: Maybe do this differently; quick llm solution until we know for sure which sprite we use
  public virtual void SetHighlightInUI(bool isHighlighted)
  {
    var spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);

    foreach (var spriteRenderer in spriteRenderers)
    {
      if (spriteRenderer == null) continue;

      if (!_defaultSpriteColors.ContainsKey(spriteRenderer))
      {
        _defaultSpriteColors[spriteRenderer] = spriteRenderer.color;
      }

      spriteRenderer.color = isHighlighted
        ? _highlightColor
        : _defaultSpriteColors[spriteRenderer];
    }
  }

  protected abstract void ExecuteInteraction(GameObject interactionSource);
  protected abstract void PlayInteractionSound();
}