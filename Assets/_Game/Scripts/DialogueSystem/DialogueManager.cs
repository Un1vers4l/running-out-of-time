using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Components")]
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    private CanvasGroup _canvasGroup;
    private Story _currentInkStory;
    private bool _isDialogPlaying;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        ;

        _canvasGroup = GetComponent<CanvasGroup>();
        HideDialogPanel();
    }

    void Update()
    {
        if (!_isDialogPlaying) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ContinueDialogue();
        }
    }

    public void StartDialogue(string speakerName, TextAsset inkDialogJSON)
    {
        _currentInkStory = new Story(inkDialogJSON.text);
        ShowDialogPanel();
        SpeakerNameText.SetText(speakerName);

        ContinueDialogue();
    }

    private void ContinueDialogue()
    {
        if (!_currentInkStory.canContinue)
        {
            HideDialogPanel();
        }

        DialogueText.SetText(_currentInkStory.Continue());
    }

    private void ShowDialogPanel()
    {
        _isDialogPlaying = true;
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        if (DialogueText.text.Length > 0) DialogueText.text = "";
        if (SpeakerNameText.text.Length > 0) SpeakerNameText.text = "";
    }

    private void HideDialogPanel()
    {
        _isDialogPlaying = false;
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        if (DialogueText.text.Length > 0) DialogueText.text = "";
        if (SpeakerNameText.text.Length > 0) SpeakerNameText.text = "";
    }
}