using UnityEngine;
using UnityEngine.InputSystem;
public class TriggerDialogueExample : MonoBehaviour
{
    public GameObject dialogueTarget;

    public RuntimeDialogueGraph RuntimeGraph;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame){
            DialogueManager dialogueManager = dialogueTarget.GetComponent<DialogueManager>();
            dialogueManager.StartDialogue(RuntimeGraph);
        }
    }
}
