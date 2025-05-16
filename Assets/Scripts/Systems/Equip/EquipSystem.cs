using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem : MonoBehaviour 
{
    public static EquipSystem current;
    public Action<EEffectType, float> applyEffect, cancelEffect;
    public List<SUISlot> items { get; private set; }

    private void OnDisable()
    {
        applyEffect -= GameBalance.current.ApplyEffect;
        cancelEffect -= GameBalance.current.CancelEffect;
    }   

    public void Initialize()
    {
        applyEffect += GameBalance.current.ApplyEffect;
        cancelEffect += GameBalance.current.CancelEffect;

        items = new List<SUISlot>();
        current = this;
    }

    public void AddItem(int slot, Item item)
    {
        // if already has some effect then effect weakens by hals
        float modificator = 1f;
        Flower flower = (Flower)item;
        foreach (SUISlot _slot in items)
        {
            Flower equipFlower = (Flower)_slot.item;
            if (equipFlower.GetEffect() == flower.GetEffect())
            {
                modificator = 0.5f;
                break;
            }
        }
        applyEffect?.Invoke(flower.GetEffect(), flower.GetEffectValue() * modificator);

        items.Add(new SUISlot(slot, item));
    }

    public void RemoveItem(int slot, Item item)
    {
        // if already has some effect then effect weakens by hals
        float modificator = 1f;
        Flower flower = (Flower)item;
        foreach (SUISlot _slot in items)
        {
            Flower equipFlower = (Flower)_slot.item;
            if (equipFlower.GetEffect() == flower.GetEffect() && _slot.slot != slot)
            {
                modificator = 0.5f;
                break;
            }
        }
        cancelEffect?.Invoke(flower.GetEffect(), flower.GetEffectValue() * modificator);

        items.RemoveAll(x => x.slot == slot && x.item == item);
    }
}