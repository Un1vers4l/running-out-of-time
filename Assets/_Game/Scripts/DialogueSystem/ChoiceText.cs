using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]

public class ChoiceText : MonoBehaviour
{
  // public Color highlightColor = Color.RGBToHSV(255f, 104f, 4f);
  public Color highlightColor = Color.yellow;
  private int _choiceIndex;
  private TextMeshProUGUI _textElement;
  private Color _originalTextColor;

  void Awake()
  {
    _textElement = GetComponent<TextMeshProUGUI>();
    _originalTextColor = _textElement.color;
    gameObject.SetActive(false);
  }

  public void Setup(string text, int index, bool isActive)
  {
    _textElement.text = text;
    _choiceIndex = index;
    gameObject.SetActive(isActive);
  }

  public void Reset()
  {
    _textElement.text = "";
    gameObject.SetActive(false);
  }

  public void SetHighlight(bool isSelected)
  {
    _textElement.color = isSelected ? highlightColor : _originalTextColor;
  }
}