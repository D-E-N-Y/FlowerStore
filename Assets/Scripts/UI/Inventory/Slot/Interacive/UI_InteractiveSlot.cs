using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_InteractiveSlot : MonoBehaviour, IDropHandler
{
    public Action getItem;

    [SerializeField] protected int slot;

    [System.Serializable]
    private struct SItemInSlot
    {
        public Sprite sprite;
        public Color have, notHave;
    }
    [SerializeField] private SItemInSlot itemInSlot;

    [SerializeField] private Image ui_itemImage;
    protected Item item;
    protected abstract Type ItemType();

    public virtual void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Item _item = dropped.GetComponent<UI_Item>().item;

        if (_item.GetType() != ItemType()) return;

        if (item != null)
        {
            HasItem();
        }
        item = _item;

        InventorySystem.current.RemoveItem(item);
        ui_itemImage.sprite = item.GetSprite();
        ui_itemImage.color = itemInSlot.have;

        getItem?.Invoke();
    }

    public virtual void RemoveItem()
    {
        ui_itemImage.sprite = itemInSlot.sprite;
        ui_itemImage.color = itemInSlot.notHave;
        item = null;
        
        getItem?.Invoke();
    }

    public virtual void DropItem()
    {
        if (item == null) return;

        InventorySystem.current.AddItem(item);
    }

    protected virtual void ReplaceItem()
    {
        InventorySystem.current.AddItem(item);
    }

    public bool HasItem() => item != null;
}