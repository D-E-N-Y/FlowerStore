using System;
using System.Collections.Generic;
using UnityEngine;

public class Shelve : MonoBehaviour 
{
    public Action updateData;
    
    [SerializeField] private List<SCost> buildCost;
    public bool isBuild { get; private set; }

    [SerializeField] private List<SCost> upgradeCost;
    [SerializeField, Range(0.1f, 5f)] float priceModificator;

    public int currentLevel { get; private set; }
    public int maxLevel { get; private set; }

    [SerializeField] private List<Transform> lookPositions;

    [SerializeField] private List<Transform> floors;
    public Flower flower { get; private set; }

    public void Initialize()
    {
        // initialize flowers in for each floor        
        flower = floors[0].GetComponentInChildren<Flower>();
        foreach(Transform floor in floors)
        {        
            foreach(Flower current in floor.GetComponentsInChildren<Flower>())
            {
                current.Initialize();
            }
        }

        currentLevel = 1;
        maxLevel = 10;
    }

    public void Upgrade()
    {
        currentLevel++;
        
        if(currentLevel >= maxLevel)
        {
            ResourceSystem.current.AddResource(EResourceType.Flowers, 1);

            maxLevel += 15;
        }

        updateData?.Invoke();
    }

    public List<SCost> GetPrice()
    {
        List<SCost> _price = new List<SCost>();
        
        foreach(SCost current in flower.GetPrice())
        {
            _price.Add(new SCost(
                current.resource,
                Mathf.RoundToInt(current.amount * (1 + priceModificator * (currentLevel - 1)))
                )
            );
        }

        return _price;
    }

    public List<Transform> Positions() => lookPositions;
}