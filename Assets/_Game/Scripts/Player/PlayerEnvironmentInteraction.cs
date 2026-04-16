using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEnvironmentInteraction : MonoBehaviour
{
  public InputActionReference interactAction;
  private List<IInteractable> _nearbyInteractables = new();
  private IInteractable _currentInteractionTarget;

  private bool _canPlayerInteract = true;

  private void OnEnable()
  {
    DialogueManager.OnDialogueStarted += DisableInteraction;
    DialogueManager.OnDialogueEnded += EnableInteraction;

    EnableInteraction();
  }

  private void OnDisable()
  {
    DialogueManager.OnDialogueStarted -= DisableInteraction;
    DialogueManager.OnDialogueEnded -= EnableInteraction;

    DisableInteraction();
  }


  void Update()
  {
    if (!_canPlayerInteract) return;
    if (_nearbyInteractables.Count == 0) return;

    IInteractable closest = GetClosestInteractable();
    if (_currentInteractionTarget != closest)
    {
      UpdateCurrentIntaractionTarget(closest);
    }

    if (interactAction.action.WasPressedThisFrame())
    {
      _currentInteractionTarget.Interact(gameObject);
    }
  }


  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.TryGetComponent(out IInteractable interactable)) return;

    if (!_nearbyInteractables.Contains(interactable))
    {
      _nearbyInteractables.Add(interactable);
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.TryGetComponent(out IInteractable interactable))
    {
      _nearbyInteractables.Remove(interactable);
    }

    if (_nearbyInteractables.Count == 0)
    {
      _currentInteractionTarget?.SetHighlightInUI(false);
      _currentInteractionTarget = null;
    }
  }

  private IInteractable GetClosestInteractable()
  {
    if (_nearbyInteractables.Count == 0) return null;

    IInteractable closest = null;
    float shortestDistanceSqr = float.MaxValue;
    Vector3 playerPosition = transform.position;

    for (int i = _nearbyInteractables.Count - 1; i >= 0; i--)
    {
      IInteractable interactable = _nearbyInteractables[i];

      Vector3 interactablePosition = ((MonoBehaviour)interactable).transform.position;
      float distanceToPlayerSqr = (playerPosition - interactablePosition).sqrMagnitude;
      if (distanceToPlayerSqr < shortestDistanceSqr)
      {
        shortestDistanceSqr = distanceToPlayerSqr;
        closest = interactable;
      }
    }
    return closest;
  }

  private void UpdateCurrentIntaractionTarget(IInteractable newTarget)
  {
    _currentInteractionTarget?.SetHighlightInUI(false);
    newTarget.SetHighlightInUI(true);
    _currentInteractionTarget = newTarget;
  }

  private void EnableInteraction()
  {
    interactAction.action.Enable();
    _canPlayerInteract = true;
  }

  private void DisableInteraction()
  {
    interactAction.action.Disable();
    _canPlayerInteract = false;
  }
}