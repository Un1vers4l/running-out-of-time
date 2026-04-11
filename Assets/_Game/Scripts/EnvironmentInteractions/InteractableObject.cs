using UnityEngine;

public class InteractableObject : InteractableBase
{
  protected override void ExecuteInteraction(GameObject instigator)
  {
    // Do whatever the object wants (tbd how these are seperated by type)
  }

  protected override void PlayInteractionSound()
  {
    // Play sound
  }
}