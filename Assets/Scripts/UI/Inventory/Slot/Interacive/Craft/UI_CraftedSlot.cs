using UnityEngine;
using UnityEngine.UI;

public class UI_CraftedSlot : MonoBehaviour
{
    [SerializeField] private Image ui_itemImage;
    public Item item { get; private set; }

    public void Initialize(Item item)
    {
        if (item) GetItem();

        this.item = item;

        ui_itemImage.sprite = item.GetSprite();
        ui_itemImage.gameObject.SetActive(true);
    }

    public void GetItem()
    {
        if (item == null) return;

        InventorySystem.current.AddItem(item);
        ui_itemImage.gameObject.SetActive(false);
        item = null;
    }
}