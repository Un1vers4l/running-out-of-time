using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEnvironmentInteraction : MonoBehaviour
{
  public InputActionReference interactAction;
  private List<IInteractable> _nearbyInteractables = new();

  void OnEnable()
  {
    interactAction.action.Enable();
  }

  private void OnDisable()
  {
    interactAction.action.Disable();
  }

  void Update()
  {
    if (_nearbyInteractables.Count == 0) return;

    if (interactAction.action.WasPressedThisFrame())
    {
      {
        _nearbyInteractables[0].Interact(gameObject);
      }
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.TryGetComponent(out IInteractable interactable)) return;

    if (!_nearbyInteractables.Contains(interactable))
    {
      _nearbyInteractables.Add(interactable);
      Debug.Log("Length " + _nearbyInteractables.Count);
    }

  }
  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.TryGetComponent(out IInteractable interactable))
    {
      _nearbyInteractables.Remove(interactable);
      Debug.Log("Length " + _nearbyInteractables.Count);
    }
  }

}