using System;
using UnityEngine.EventSystems;

public class UI_EquipSlot : UI_InteractiveSlot
{
    public Action<EEffectType, float> setEffect;
    protected override Type ItemType() => typeof(Flower);

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        EquipSystem.current.AddItem(slot, item);
    }

    public override void DropItem()
    {
        base.DropItem();
        RemoveItem();
    }

    protected override void ReplaceItem()
    {
        base.HasItem();
        EquipSystem.current.RemoveItem(slot, item);
    }

    public override void RemoveItem()
    {
        if (item == null) return;

        EquipSystem.current.RemoveItem(slot, item);
        base.RemoveItem();
    }
}