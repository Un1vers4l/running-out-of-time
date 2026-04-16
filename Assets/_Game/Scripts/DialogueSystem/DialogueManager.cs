using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;
using System;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Components")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    private Story _currentInkStory;
    private bool _isDialogPlaying;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        ;

        _isDialogPlaying = false;
        DialoguePanel.SetActive(false);
    }

    private void Update()
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
        SetDialogPlayingState(true);
        SpeakerNameText.SetText(speakerName);

        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        if (!_currentInkStory.canContinue)
        {
            SetDialogPlayingState(false);
        }

        DialogueText.SetText(_currentInkStory.Continue());
    }

    private void SetDialogPlayingState(bool isPlaying)
    {
        _isDialogPlaying = isPlaying;
        DialoguePanel.SetActive(isPlaying);

        if (!isPlaying && DialogueText.text.Length > 0)
        {
            DialogueText.SetText("");
            SpeakerNameText.SetText("");
        }
    }
}