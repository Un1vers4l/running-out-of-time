using UnityEngine;

public class KultistNPC : InteractableBase
{
  // [SerializeField] private DialogueData dialogueData;

  protected override void ExecuteInteraction(GameObject interactionSource)
  {
    // something something DialogController.startDialogue()
    Debug.Log("Interacted with Cultist");
  }

  protected override void PlayInteractionSound()
  {
    // Play sound
  }
}
