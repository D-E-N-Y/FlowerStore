using UnityEngine;

public class UI_InventorySlot : MonoBehaviour
{
    [SerializeField] private UI_Item ui_item;
    [SerializeField] private TooltipTrigger tooltip;

    public void Initialize(Item item)
    {
        ui_item.Initialize(item);
        tooltip.SetText(item.GetName(), item.GetDescription());
    }
}