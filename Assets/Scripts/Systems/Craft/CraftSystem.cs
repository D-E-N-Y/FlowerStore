using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public static CraftSystem current;
    public Action updateData;

    [SerializeField] private List<Flower> craftableItems;
    public List<SUISlot> items { get; private set; }

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

    public Flower Craft()
    {
        if (items.Count == 5) return null;

        Flower _flower = craftableItems[UnityEngine.Random.Range(0, craftableItems.Count)];
        return _flower;
    }
}