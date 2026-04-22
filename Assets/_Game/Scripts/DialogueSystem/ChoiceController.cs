using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasGroup))]
public class ChoiceController : MonoBehaviour
{
  [Header("Inputs")]
  public InputActionReference confirmChoiceAction;
  public InputActionReference selectionMoveAction;
  [Header("Choice Elements")]
  public List<ChoiceText> choiceTextElements;

  private CanvasGroup _canvasGroup;
  private List<string> _currentChoices;
  private int _currentSelectedIndex = 0;
  private bool _isChoiceModeActive = false;

  void Awake()
  {
    _canvasGroup = GetComponent<CanvasGroup>();
    _canvasGroup.alpha = 0;
    confirmChoiceAction.action.Enable();
    selectionMoveAction.action.Enable();
  }

  private void Update()
  {
    if (!_isChoiceModeActive) return;

    HandleNavigationInput();
    HandleConfirmationInput();
  }

  public void SetupAndShowChoices(List<string> choices)
  {
    _currentChoices = choices;
    for (int i = 0; i < choiceTextElements.Count; i++)
    {
      string text = i < choices.Count ? choices[i] : "";
      bool isTextElementActive = i < choices.Count && text.Length > 0;
      choiceTextElements[i].Setup(text, i, isTextElementActive);
    }

    _canvasGroup.alpha = 1;
    _isChoiceModeActive = true;
    _currentSelectedIndex = 0;
    UpdateChoiceHighlighting();
  }

  public void HideChoices()
  {
    _canvasGroup.alpha = 0;
    _isChoiceModeActive = false;
  }

  private void HandleNavigationInput()
  {
    if (!selectionMoveAction.action.WasPressedThisFrame()) return;
    bool indexChanged = false;
    Vector2 inputDirection = selectionMoveAction.action.ReadValue<Vector2>();

    if (inputDirection == Vector2.up)
    {
      _currentSelectedIndex--;
      if (_currentSelectedIndex < 0)
        _currentSelectedIndex = _currentChoices.Count - 1; // Wrap around

      indexChanged = true;
    }
    else if (inputDirection == Vector2.down)
    {
      _currentSelectedIndex++;
      if (_currentSelectedIndex >= _currentChoices.Count)
        _currentSelectedIndex = 0; // Wrap around

      indexChanged = true;
    }

    if (indexChanged)
    {
      UpdateChoiceHighlighting();
    }
  }
  private void HandleConfirmationInput()
  {
    if (confirmChoiceAction.action.WasPressedThisFrame())
    {
      DialogueManager.Instance.SelectChoice(_currentSelectedIndex);
    }
  }
  private void UpdateChoiceHighlighting()
  {
    for (int i = 0; i < choiceTextElements.Count; i++)
    {
      bool isSelected = i == _currentSelectedIndex;
      choiceTextElements[i].SetHighlight(isSelected);
    }
  }
}
