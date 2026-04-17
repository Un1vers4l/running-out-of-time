using System.Collections;
using TMPro;
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

    [Header("Typewriter")]
    [SerializeField] private float charactersPerSecond = 40f;

    private CanvasGroup _canvasGroup;
    private Story _currentInkStory;
    private bool _isDialogPlaying;
    public bool IsDialoguePlaying => _isDialogPlaying;
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
        if (!_isDialogPlaying) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
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
        ShowDialogPanel();
        SpeakerNameText.SetText(speakerName);
        ContinueDialogue();
    }

    private void ContinueDialogue()
    {
        if (!_currentInkStory.canContinue)
        {
            HideDialogPanel();
            return;
        }

        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        _typingCoroutine = StartCoroutine(TypeText(_currentInkStory.Continue()));
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
        _isDialogPlaying = true;
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        DialogueText.text = "";
        SpeakerNameText.text = "";
    }

    private void HideDialogPanel()
    {
        _isDialogPlaying = false;
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        DialogueText.text = "";
        SpeakerNameText.text = "";
    }
}
