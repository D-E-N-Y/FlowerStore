using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem current;
    public Action updateData;

    public List<Item> items { get; private set; }

    public void Initialize()
    {
        items = new List<Item>();
        current = this;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        updateData?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        updateData?.Invoke();
    }
}