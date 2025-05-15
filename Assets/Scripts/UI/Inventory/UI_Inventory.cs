using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private UI_Item ui_itemPrefab;
    [SerializeField] private GameObject itemsContainer;
    private List<UI_Item> ui_items;

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
        ui_items = itemsContainer.GetComponentsInChildren<UI_Item>(true).ToList();

        int dif = items.Count - ui_items.Count;
        if (dif > 0)
        {
            for (int i = 0; i < dif; i++)
            {
                UI_Item _ui_item = Instantiate(ui_itemPrefab, itemsContainer.transform);
                ui_items.Add(_ui_item);
            }
        }
        ui_items.ForEach(x => x.gameObject.SetActive(false));
        
        for (int i = 0; i < items.Count; i++)
        {
            ui_items[i].Initialize(items[i]);
            ui_items[i].gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
