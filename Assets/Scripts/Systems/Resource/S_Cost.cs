using UnityEngine;

[System.Serializable]
public struct SCost
{
    public EResourceType resource;
    [Range(0, 9999)] public int amount;

    public SCost(EResourceType resource, int amount)
    {
        this.resource = resource;
        this.amount = amount;
    }
}