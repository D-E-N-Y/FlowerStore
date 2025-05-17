using System.Collections.Generic;
using UnityEngine;

public class UIProvider : MonoBehaviour 
{
    [SerializeField] private List<UI_Panel> ui_prefabs;
    [SerializeField] private UI_Panel ui_lastMenu;
    public Dictionary<string, UI_Panel> panels;

    public void Initialize()
    {
        panels = new Dictionary<string, UI_Panel>();
        
        foreach(UI_Panel ui_panel in ui_prefabs)
        {
            panels.Add(ui_panel.name, ui_panel);
        }

        UISystem.current.Initialize(this);
    }

    public UI_Panel GetPanelByName(string name)
    {
        return panels.ContainsKey(name) ? panels[name] : null;
    }

    public UI_Panel GetLastMenu() => ui_lastMenu;
}