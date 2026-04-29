using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform slotContainer;
    public GameObject slotPrefab;

    private void OnEnable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged += Refresh;

        Refresh();
    }

    private void OnDisable()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= Refresh;
    }

    private void Refresh()
    {
        foreach (Transform child in slotContainer)
            Destroy(child.gameObject);

        foreach (ItemData item in inventory.items)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            slot.GetComponentsInChildren<Image>()[1].sprite = item.sprite;
        }
    }
}
