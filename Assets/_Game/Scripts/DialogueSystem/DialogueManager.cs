using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CanvasGroup))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI Components")]
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;
    public ChoiceController ChoiceController;

    [Header("Typewriter")]
    [SerializeField] private float charactersPerSecond = 40f;
    public InputActionReference dialogueAdvanceAction;
    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    public InkController InkController = new();

    private CanvasGroup _canvasGroup;

    private bool _isDialoguePlaying = false;
    public bool IsDialoguePlaying => _isDialoguePlaying;
    private bool _isTyping;
    private Coroutine _typingCoroutine;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        _canvasGroup = GetComponent<CanvasGroup>();
        HideDialogPanel();
    }

    void Update()
    {
        if (!_isDialoguePlaying) return;

        if (dialogueAdvanceAction.action.WasPressedThisFrame())
        {
            if (_isTyping)
                SkipTyping();
            else
                ContinueDialogue();
        }
    }

    public void StartDialogue(string speakerName, TextAsset inkDialogJSON)
    {
        InkController.InitNewStory(speakerName, inkDialogJSON);
        ShowDialogPanel();
        SpeakerNameText.SetText(speakerName);
        ContinueDialogue();
    }

    private void ContinueDialogue()
    {
        NextDialogueLineData nextDialogueLineData = InkController.ContinueStory();

        if (nextDialogueLineData == null)
        {
            HideDialogPanel();
            return;
        }

        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);

        if (nextDialogueLineData.Choices.Count > 0)
        {
            DialogueText.gameObject.SetActive(false);
            ChoiceController.SetupAndShowChoices(nextDialogueLineData.Choices);
        }
        else
        {
            DialogueText.gameObject.SetActive(true);
            ChoiceController.HideChoices();
            _typingCoroutine = StartCoroutine(TypeText(nextDialogueLineData.Text));
        }

        SpeakerNameText.SetText(nextDialogueLineData.Speaker);
    }

    public void SelectChoice(int choiceIndex)
    {
        InkController.CurrentStory.ChooseChoiceIndex(choiceIndex);
    }

    private IEnumerator TypeText(string text)
    {
        _isTyping = true;
        DialogueText.SetText(text);
        DialogueText.maxVisibleCharacters = 0;

        // ForceMeshUpdate so textInfo.characterCount is accurate immediately
        DialogueText.ForceMeshUpdate();
        int totalChars = DialogueText.textInfo.characterCount;

        float delay = 1f / charactersPerSecond;

        for (int i = 0; i <= totalChars; i++)
        {
            DialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(delay);
        }

        _isTyping = false;
    }

    private void SkipTyping()
    {
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        DialogueText.maxVisibleCharacters = int.MaxValue;
        _isTyping = false;

    }

    private void ShowDialogPanel()
    {
        _isDialoguePlaying = true;
        dialogueAdvanceAction.action.Enable();
        OnDialogueStarted?.Invoke();
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        DialogueText.text = "";
        SpeakerNameText.text = "";
    }

    private void HideDialogPanel()
    {
        _isDialoguePlaying = false;
        dialogueAdvanceAction.action.Disable();
        OnDialogueEnded?.Invoke();
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        DialogueText.text = "";
        SpeakerNameText.text = "";
    }


}
