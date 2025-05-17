using System;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInteractablePanels : MonoBehaviour
{
    [SerializeField] private List<UI_InteractablePanel> ui_panels;
    private Dictionary<Type, UI_InteractablePanel> panelByType = new();
    private UI_InteractablePanel openPanel;

    public void Initialize()
    {
        foreach (UI_InteractablePanel panel in ui_panels)
        {
            if (panel.PanelType != null && !panelByType.ContainsKey(panel.PanelType))
            {
                panelByType.Add(panel.PanelType, panel);
            }
        }
        
        InteractionSystem.current.Select += OpenPanel;
        InteractionSystem.current.UnSelect += ClosePanel;
    }

    private void OnDisable() 
    {
        InteractionSystem.current.Select -= OpenPanel;
        InteractionSystem.current.UnSelect -= ClosePanel;
    }

    private void OpenPanel(Actor actor)
    {
        Type _type = actor.GetType();

        foreach(var kvp in panelByType)
        {
            var panelType = kvp.Key;
            if (panelType.IsAssignableFrom(_type))
            {
                _type = panelType;

                panelByType[_type].Show();
                panelByType[_type].Initialize(actor);
                openPanel = panelByType[_type];

                return;
            }
        }

        Debug.Log($"Not found panel for {_type}");
    }

    private void ClosePanel()
    {
        if(openPanel)
        {
            openPanel.Hide();
            openPanel = null;
        }
    }
}