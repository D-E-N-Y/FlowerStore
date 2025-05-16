using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem : MonoBehaviour 
{
    public static EquipSystem current;
    public Action updateData;

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

    public void Equip()
    {
        // equip
    }
}