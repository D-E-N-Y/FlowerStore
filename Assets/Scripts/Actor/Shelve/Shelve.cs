using System.Collections.Generic;
using UnityEngine;

public class Shelve : Actor 
{
    public System.Action updateData;
    
    [SerializeField] private List<SCost> buildCost;
    public bool isBuild { get; private set; }

    [SerializeField] private List<SCost> upgradeCost;
    [SerializeField, Range(0.1f, 5f)] float priceModificator;

    public int currentLevel { get; private set; }
    public int maxLevel { get; private set; }

    [SerializeField] private List<Transform> lookPositions;

    [SerializeField] private List<Transform> floors;
    public PreviewFlower flower { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        // initialize flowers in for each floor        
        flower = floors[0].GetComponentInChildren<PreviewFlower>();
        foreach(Transform floor in floors)
        {        
            foreach(PreviewFlower current in floor.GetComponentsInChildren<PreviewFlower>())
            {
                current.Initialize();
            }
        }

        currentLevel = 1;
        maxLevel = 10;

        isBuild = true;
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

        foreach (SCost current in flower.GetPrice())
        {
            int amount = Mathf.RoundToInt(current.amount * (1 + priceModificator * (currentLevel - 1)));

            _price.Add(new SCost(
                current.resource,
                amount + (int)(amount * GameBalance.current.GetEffect(EEffectType.IncreaseValue))
            ));
        }

        return _price;
    }

    public List<Transform> Positions() => lookPositions;
    public Vector3 RandomPosition() => lookPositions[Random.Range(0, lookPositions.Count)].position;
}