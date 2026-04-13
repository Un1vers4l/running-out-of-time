using UnityEditor;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
  public void Interact(GameObject interactionSource)
  {
    ExecuteInteraction(interactionSource);
    PlayInteractionSound();
  }

  public virtual void Highlight()
  {
    // default highlight logic
    Debug.Log("Highlight object: " + gameObject.name);
  }

  protected abstract void ExecuteInteraction(GameObject interactionSource);
  protected abstract void PlayInteractionSound();
}