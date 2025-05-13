using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    public static ResourceSystem current;
    public Action updateData;
    
    [Serializable]
    private struct SResource
    {
        public EResourceType type;
        public int amount;
    }    
    [SerializeField] private List<SResource> serializedResources;
    private Dictionary<EResourceType, int> resources;

    public void Initialize()
    {
        resources = new Dictionary<EResourceType, int>();
        
        foreach(SResource current in serializedResources)
        {
            resources[current.type] = current.amount;
        }

        current = this;
    }

    public int GetByType(EResourceType type) => resources.TryGetValue(type, out int value) ? value : 0;
    public bool CheckForBuy(EResourceType type, int value) => resources[type] >= value;

    public int GetMoney() => resources[EResourceType.Money];
    public int GetFlowers() => resources[EResourceType.Flowers];
    
    public void AddResource(EResourceType type, int value)
    {
        value = Mathf.Max(value, 0);
        resources[type] += value;

        updateData?.Invoke();
    }
    
    public void RemoveResource(EResourceType type, int value)
    {
        value = Mathf.Max(value, 0);
        int result = resources[type] - value;
        resources[type] = Mathf.Max(result, 0);

        updateData?.Invoke();
    }
}
