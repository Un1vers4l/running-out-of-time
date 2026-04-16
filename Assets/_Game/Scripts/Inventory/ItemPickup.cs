using UnityEngine;

public class ItemPickup : InteractableBase
{
    public ItemData itemData;

    protected override void ExecuteInteraction(GameObject interactionSource)
    {
        Inventory inventory = interactionSource.GetComponent<Inventory>();
        if (inventory == null) return;

        if (inventory.AddItem(itemData))
            Destroy(gameObject);
    }

    protected override void PlayInteractionSound() { }
}
