using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shelve : UI_InteractablePanel
{
    public override Type PanelType => typeof(Shelve);
    private Shelve shelve;

    [SerializeField] private GameObject ui_buildPanel;
    [SerializeField] private GameObject ui_notBuildPanel;

    [SerializeField] protected TextMeshProUGUI ui_nameBuild;

    [SerializeField] private List<SUICost> ui_flowerCost;
    [SerializeField] private List<SUICost> ui_upgradeCost;
    [SerializeField] private List<SUICost> ui_buildCost;
    [SerializeField] private Slider ui_lvlSlider;

    [SerializeField] private Button ui_build, ui_upgrade;

    public override void Initialize(Actor _actor)
    {
        base.Initialize(_actor);

        if (shelve) UnsubscriptionActions();
        shelve = (Shelve)_actor;

        shelve.updateData += UpdateData;
        ResourceSystem.current.updateData += UpdateButtons;

        UpdateData();
    }

    private void UpdateData()
    {
        ui_buildPanel.SetActive(shelve.isBuild);
        ui_notBuildPanel.SetActive(!shelve.isBuild);

        ui_nameBuild.text = shelve.GetName();

        SetUICost(shelve.GetBuildCost(), ui_buildCost);
        SetUICost(shelve.GetFlowerCost(), ui_flowerCost);
        SetUICost(shelve.GetUpgradeCost(), ui_upgradeCost);

        ui_lvlSlider.value = (float)(shelve.currentLevel - (shelve.maxLevel - 15)) / (float)(shelve.maxLevel - (shelve.maxLevel - 15));

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (shelve.isBuild)
        {
            ui_upgrade.onClick.RemoveAllListeners();
            ui_upgrade.interactable = false;

            if (!ResourceSystem.current.CheckForBuy(shelve.GetUpgradeCost())) return;

            ui_upgrade.onClick.AddListener(() => Upgrade());
            ui_upgrade.interactable = true;
        }
        else
        {
            ui_build.onClick.RemoveAllListeners();
            ui_build.interactable = false;

            if (!ResourceSystem.current.CheckForBuy(shelve.GetBuildCost())) return;

            ui_build.onClick.AddListener(() => Build());
            ui_build.interactable = true;
        }
    }

    private void SetUICost(List<SCost> costs, List<SUICost> ui_costs)
    {
        foreach (SCost cost in costs)
        {
            foreach (SUICost ui_cost in ui_costs)
            {
                if (cost.resource == ui_cost.resource)
                {
                    ui_cost.ui_amount.text = cost.amount.ToString();
                    break;
                }
            }
        }
    }

    private void Build()
    {
        if (!ResourceSystem.current.CheckForBuy(shelve.GetBuildCost())) return;

        ResourceSystem.current.RemoveResources(shelve.GetBuildCost());
        shelve.Build();
    }

    private void Upgrade()
    {
        if (!ResourceSystem.current.CheckForBuy(shelve.GetUpgradeCost())) return;

        ResourceSystem.current.RemoveResources(shelve.GetUpgradeCost());
        shelve.Upgrade();
    }

    protected override void UnsubscriptionActions()
    {
        base.UnsubscriptionActions();

        shelve.updateData -= UpdateData;
        ResourceSystem.current.updateData -= UpdateButtons;
    }
}