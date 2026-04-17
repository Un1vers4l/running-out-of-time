using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class HotbarVisibility : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        bool shouldHide =
            (DialogueManager.Instance != null && DialogueManager.Instance.IsDialoguePlaying) ||
            (GameTimerStateMachine.Instance != null && GameTimerStateMachine.Instance.IsStartScreenShowing);

        _canvasGroup.alpha = shouldHide ? 0f : 1f;
        _canvasGroup.blocksRaycasts = !shouldHide;
    }
}
