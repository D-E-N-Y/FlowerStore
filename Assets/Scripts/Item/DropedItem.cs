using UnityEngine;

public class DropedItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private UI_World ui_world;

    public void Initialize()
    {
        ui_world.Initialize();
    }

    public void GiveUp()
    {
        InventorySystem.current.AddItem(item);
        Destroy(gameObject);
    }
}
