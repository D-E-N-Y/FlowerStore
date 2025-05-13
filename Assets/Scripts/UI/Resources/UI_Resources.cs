using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Resources : MonoBehaviour
{
    [Serializable]
    private struct SResource
    {
        public EResourceType type;
        public TextMeshProUGUI ui_amount;
    }    
    [SerializeField] private List<SResource> ui_serializedResources;

    public void Initialize()
    {
        UpdateData();

        ResourceSystem.current.updateData += UpdateData;
    }

    private void OnDisable()
    {
        ResourceSystem.current.updateData -= UpdateData;
    }

    private void UpdateData()
    {
        foreach(SResource current in ui_serializedResources)
        {
            current.ui_amount.text = ResourceSystem.current.GetByType(current.type).ToString();
        }
    }
}
