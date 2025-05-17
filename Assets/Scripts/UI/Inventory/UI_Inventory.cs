using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Inventory : UI_Panel
{
    [SerializeField] private UI_InventorySlot ui_inventorySlotPrefab;
    [SerializeField] private GameObject itemsContainer;
    private List<UI_InventorySlot> ui_inventorySlots;

    private void OnEnable()
    {
        InventorySystem.current.updateData += UpdateData;
        UpdateData();
    }

    private void OnDisable()
    {
        InventorySystem.current.updateData -= UpdateData;
    }

    void UpdateData()
    {
        List<Item> items = InventorySystem.current.items;
        ui_inventorySlots = itemsContainer.GetComponentsInChildren<UI_InventorySlot>(true).ToList();

        int dif = items.Count - ui_inventorySlots.Count;
        if (dif > 0)
        {
            for (int i = 0; i < dif; i++)
            {
                UI_InventorySlot ui_slot = Instantiate(ui_inventorySlotPrefab, itemsContainer.transform);
                ui_inventorySlots.Add(ui_slot);
            }
        }
        ui_inventorySlots.ForEach(x => x.gameObject.SetActive(false));
        
        for (int i = 0; i < items.Count; i++)
        {
            ui_inventorySlots[i].Initialize(items[i]);
            ui_inventorySlots[i].gameObject.SetActive(true);
        }
    }
}
