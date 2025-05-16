using System;
using UnityEngine.EventSystems;

public class UI_EquipSlot : UI_InteractiveSlot
{
    protected override Type ItemType() => typeof(Flower);

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        EquipSystem.current.AddItem(slot, item);
    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        EquipSystem.current.RemoveItem(slot, item);
        item = null;
    }

    protected override void HasItem()
    {
        base.HasItem();
        EquipSystem.current.RemoveItem(slot, item);
    }
}