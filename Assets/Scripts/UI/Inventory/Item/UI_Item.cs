using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    [SerializeField] private Image ui_image;
    public Item item { get; private set; }

    public void Initialize(Item item)
    {
        this.item = item;
        ui_image.sprite = item.GetSprite();
    }
}
