using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public static CraftSystem current;
    public Action updateData;

    [SerializeField] private List<Flower> craftableItems;
    private List<SUISlot> items;

    public void Initialize()
    {
        items = new List<SUISlot>();
        current = this;
    }

    public void AddItem(int slot, Item item)
    {
        items.Add(new SUISlot(slot, item));
        updateData?.Invoke();
    }

    public void RemoveItem(int slot, Item item)
    {
        items.RemoveAll(x => x.slot == slot && x.item == item);
        updateData?.Invoke();
    }

    public void Craft()
    {
        if (items.Count == 5)
        {
            // craft
        }
    }
}