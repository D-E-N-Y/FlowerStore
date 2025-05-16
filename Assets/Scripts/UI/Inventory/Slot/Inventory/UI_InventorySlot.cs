using UnityEngine;

public class UI_InventorySlot : MonoBehaviour
{
    [SerializeField] private UI_Item ui_item;

    public void Initialize(Item item)
    {
        ui_item.Initialize(item);
    }
}