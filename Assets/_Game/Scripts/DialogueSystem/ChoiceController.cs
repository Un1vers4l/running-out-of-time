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
    for (int i = 0; i < choiceTextElements.Count; i++)
    {
      string text = i < choices.Count ? choices[i] : "";
      bool isActive = i < choices.Count;
      choiceTextElements[i].Setup(text, i, isActive);
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
    bool indexChanged = false;
    if (selectionMoveAction.action.ReadValue<Vector2>() == Vector2.up)
    {
      _currentSelectedIndex--;
      if (_currentSelectedIndex < 0)
        _currentSelectedIndex = choiceTextElements.Count - 1; // Wrap around

      indexChanged = true;
    }
    else if (selectionMoveAction.action.ReadValue<Vector2>() == Vector2.down)
    {
      _currentSelectedIndex++;
      if (_currentSelectedIndex >= choiceTextElements.Count)
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

// {
//   public List<ChoiceText> DialogueChoiceTexts;

//   public 

//   public void Setup(int index, string text)
//   {
//     choiceIndex = index;
//     GetComponent<TextMeshProUGUI>().text = text;
//   }

//   public void OnPointerClick(PointerEventData eventData)
//   {
//     Debug.Log("Select choice" + choiceIndex);
//     DialogueManager.Instance.SelectChoice(choiceIndex);
//   }

//   public SelectChoice()
//   {

//   }
// }