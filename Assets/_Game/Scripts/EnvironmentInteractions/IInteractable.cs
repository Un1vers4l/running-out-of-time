using System;
using UnityEngine;
public interface IInteractable
{
  void Interact(GameObject interactionSource);
  void SetHighlightInUI(bool isHighlighted);
}