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

    public override void DropItem()
    {
        base.DropItem();
        RemoveItem();
    }

    protected override void ReplaceItem()
    {
        base.HasItem();
        CraftSystem.current.RemoveItem(slot, item);
    }

    public override void RemoveItem()
    {
        if (item == null) return;
       
        CraftSystem.current.RemoveItem(slot, item);
        base.RemoveItem();
    }
}