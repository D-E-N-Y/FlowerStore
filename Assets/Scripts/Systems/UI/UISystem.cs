using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    public static UISystem current;

    private Dictionary<string, UI_Panel> openPanels;
    private UIProvider provider;

    public bool HasOpenPanels() => openPanels.Count() > 0;

    public void Initialize()
    {
        current = this;
        openPanels = new Dictionary<string, UI_Panel>();
    }

    public void Initialize(UIProvider provider)
    { 
        this.provider = provider;
        openPanels.Clear();
    }

    private UI_Panel CurrentPanel => openPanels.Count > 0 ? openPanels.Last().Value : null;

    public void OpenLastMenu()
    {
        provider.GetLastMenu().Show();
        openPanels[provider.GetLastMenu().gameObject.name] = provider.GetLastMenu();
    }

    public void OpenPanelByName(string name)
    {
        var newPanel = provider.GetPanelByName(name);
        if (newPanel == null)
        {
            Debug.LogWarning($"UIPanel '{name}' not found.");
            return;
        }

        openPanels[name] = newPanel;
        newPanel.Show();
    }

    public void ClosePanelByName(string name)
    {
        if(openPanels.Count <= 0) return;
        
        openPanels[name].Hide();
        openPanels.Remove(name);
    }

    public void TogglePanelByName(string name)
    {
        if(openPanels.ContainsKey(name))
        {
            ClosePanelByName(name);
        }
        else
        {
            OpenPanelByName(name);
        }
    }

    public void CloseCurrentPanel()
    {
        if(openPanels.Count <= 0) return;

        string lastKey = openPanels.Keys.Last();
        if(openPanels[lastKey].isCanClose)
        {
            openPanels[lastKey].Hide();
            openPanels.Remove(lastKey);
        }
    }

    public void CloseAllPanels()
    {
        if(openPanels.Count <= 0) return;
        
        foreach(var panel in openPanels)
        {
            panel.Value.Hide();
        }

        openPanels.Clear();
    }
}