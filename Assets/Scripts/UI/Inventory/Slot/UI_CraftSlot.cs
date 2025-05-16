using System;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_InteractiveSlot 
{
    protected override Type ItemType() => typeof(Petal);

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        CraftSystem.current.AddItem(slot, item);
    }

    public override void RemoveItem()
    {
        base.RemoveItem();
        CraftSystem.current.RemoveItem(slot, item);
        item = null;
    }

    protected override void HasItem()
    {
        base.HasItem();
        CraftSystem.current.RemoveItem(slot, item);
    }
}