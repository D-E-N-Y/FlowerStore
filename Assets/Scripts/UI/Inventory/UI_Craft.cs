using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Craft : MonoBehaviour
{
    [SerializeField] private List<UI_CraftSlot> ui_craftSlots;
    [SerializeField] private UI_CraftedSlot ui_craftedSlot;

    [SerializeField] private Button ui_craftButton;

    private void OnEnable()
    {
        ui_craftSlots.ForEach(x => x.getItem += AvaibleCraft);
        AvaibleCraft();
    }

    private void OnDisable()
    {
        ui_craftSlots.ForEach(x => x.getItem -= AvaibleCraft);
    }

    private void AvaibleCraft()
    {
        int hasItems = ui_craftSlots.Where(x => x.HasItem()).ToList().Count();
        ui_craftButton.interactable = hasItems == ui_craftSlots.Count;
    }

    public void Craft()
    {
        ui_craftSlots.ForEach(x => x.RemoveItem());

        Item _item = CraftSystem.current.Craft();
        ui_craftedSlot.Initialize(_item);
    }
}
