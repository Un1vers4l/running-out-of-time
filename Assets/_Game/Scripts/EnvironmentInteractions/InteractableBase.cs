using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
  public void Interact(GameObject interactionSource)
  {
    PlayInteractionSound();
    ExecuteInteraction(interactionSource);
  }

  protected virtual void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player")) { Debug.Log("Player entered!"); }
  }

  protected virtual void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player")) { Debug.Log("Player left.."); }
  }

  protected abstract void ExecuteInteraction(GameObject interactionSource);
  protected abstract void PlayInteractionSound();
}