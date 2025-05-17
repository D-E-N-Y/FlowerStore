using System;
using TMPro;
using UnityEngine;

public abstract class UI_InteractablePanel : UI_Panel 
{
    [SerializeField] protected TextMeshProUGUI ui_name;
    public abstract Type PanelType { get; }
    private Actor actor;

    public virtual void Initialize(Actor _actor)
    {
        actor = _actor;
        
        ui_name.text = actor.GetName();
    }

    public override void Hide()
    {
        UnsubscriptionActions();
        base.Hide();
    }

    private void OnDisable()
    {
        UnsubscriptionActions();
    }

    protected virtual void UnsubscriptionActions()
    {
        
    }
}