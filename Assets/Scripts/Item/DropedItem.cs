using UnityEngine;
using UnityEngine.UI;

public class DropedItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private UI_World ui_world;
    [SerializeField] private Image ui_itemImage;

    public void Initialize()
    {
        ui_itemImage.sprite = item.GetSprite();
        ui_world.Initialize();
    }

    public void GiveUp()
    {
        InventorySystem.current.AddItem(item);
        Destroy(gameObject);
    }
}
