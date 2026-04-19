using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();
    public int maxSlots = 20;

    public event Action OnInventoryChanged;

    void Start()
    {
        DialogueManager.Instance.InkController.OnAddItem += HandleAddItemFromInk;
    }

    void OnDisable()
    {
        DialogueManager.Instance.InkController.OnAddItem -= HandleAddItemFromInk;
    }

    public bool AddItem(ItemData data)
    {
        if (items.Count >= maxSlots)
            return false;

        items.Add(data);
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(ItemData data)
    {
        if (items.Remove(data))
            OnInventoryChanged?.Invoke();
    }

    private void HandleAddItemFromInk(string itemName)
    {
        // TODO
        // probably get all items and find the right one!? idk
        Debug.Log("Add item from ink: " + itemName);
    }
}
