using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI Components")]
    public TextMeshProUGUI SpeakerNameText;
    public TextMeshProUGUI DialogueText;

    [Header("Typewriter")]
    [SerializeField] private float charactersPerSecond = 40f;
    public InputActionReference dialogueAdvanceAction;
    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;

    private CanvasGroup _canvasGroup;
    private Story _currentInkStory;
    private bool _isDialoguePlaying = false;
    public bool IsDialoguePlaying => _isDialoguePlaying;
    private bool _isTyping;
    private Coroutine _typingCoroutine;

    private Dictionary<string, Action<string>> _commandRegistry = new Dictionary<string, Action<string>>();
    private readonly string INK_FUNCTION_BIND_NAME = "ExecuteFunction";

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        _commandRegistry.Add("AddInventoryItem", (payload) => AddItemMock(payload));

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
        _currentInkStory = new Story(inkDialogJSON.text);
        _currentInkStory.BindExternalFunction(INK_FUNCTION_BIND_NAME, (Action<string, string>)ExecuteFunctionFromInk);
        ShowDialogPanel();
        SpeakerNameText.SetText(speakerName);
        ContinueDialogue();
    }

    private void ContinueDialogue()
    {
        if (!_currentInkStory.canContinue)
        {
            HideDialogPanel();
            _currentInkStory.UnbindExternalFunction(INK_FUNCTION_BIND_NAME);
            return;
        }


        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        _typingCoroutine = StartCoroutine(TypeText(_currentInkStory.Continue()));

        if (_currentInkStory.currentTags.Contains("Player"))
        {
            SpeakerNameText.SetText("Player");
        }
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
        OnDialogueStarted.Invoke();
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
        OnDialogueEnded.Invoke();
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        DialogueText.text = "";
        SpeakerNameText.text = "";
    }

    private void ExecuteFunctionFromInk(string commandName, string payload)
    {
        if (_commandRegistry.TryGetValue(commandName, out Action<string> action))
        {
            action.Invoke(payload);
        }
        else
        {
            Debug.LogWarning($"ExecuteFunctionFromInk: Command '{commandName}' not found in registry.");
        }
    }
    private void AddItemMock(string payload)
    {
        Debug.Log("You received an item! It is: " + payload);
    }
}
