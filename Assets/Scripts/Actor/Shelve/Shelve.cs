using System.Collections.Generic;
using UnityEngine;

public class Shelve : Actor 
{
    public System.Action updateData;
    
    [SerializeField] private List<SCost> buildCost;
    public bool isBuild { get; private set; }

    [SerializeField] private List<SCost> upgradeCost;
    [SerializeField, Range(0.1f, 5f)] float priceModificator;
    [SerializeField, Range(0.1f, 5f)] float upgradeModificator; 

    public int currentLevel { get; private set; }
    public int maxLevel { get; private set; }

    [SerializeField] private List<Transform> lookPositions;

    [SerializeField] private List<Transform> floors;
    public PreviewFlower flower { get; private set; }

    [SerializeField] private GameObject buildMesh;
    [SerializeField] private GameObject nameplate;
    [SerializeField] private UI_World arrow;

    public override void Initialize()
    {
        base.Initialize();

        // initialize flowers in for each floor        
        flower = floors[0].GetComponentInChildren<PreviewFlower>();
        foreach (Transform floor in floors)
        {
            foreach (PreviewFlower current in floor.GetComponentsInChildren<PreviewFlower>())
            {
                current.Initialize();
            }
        }

        currentLevel = 1;
        maxLevel = 15;

        nameplate.SetActive(!isBuild);
        buildMesh.SetActive(isBuild);

        isBuild = false;
        arrow.SetHeight(2);

        ResourceSystem.current.updateData += PossiblePay;
        PossiblePay();
    }

    private void PossiblePay()
    {
        if (isBuild)
        {
            if (!ResourceSystem.current.CheckForBuy(GetUpgradeCost()))
            {
                arrow.gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            if (!ResourceSystem.current.CheckForBuy(buildCost))
            {
                arrow.gameObject.SetActive(false);
                return;
            }
        }

        arrow.gameObject.SetActive(true);
        arrow.Initialize();
    }

    public void Build()
    {
        isBuild = true;

        nameplate.SetActive(!isBuild);
        buildMesh.SetActive(isBuild);

        arrow.SetHeight(4);

        updateData?.Invoke();
    }

    public void Upgrade()
    {
        currentLevel++;

        if (currentLevel >= maxLevel)
        {
            ResourceSystem.current.AddResource(EResourceType.Flowers, 1);

            maxLevel += 15;
        }

        updateData?.Invoke();
    }

    public List<SCost> GetFlowerCost()
    {
        List<SCost> _modif = new List<SCost>();

        foreach (SCost current in flower.GetPrice())
        {
            int amount = Mathf.RoundToInt(current.amount * (1 + priceModificator * (currentLevel - 1)));

            _modif.Add(new SCost(
                current.resource,
                amount + (int)(amount * GameBalance.current.GetEffect(EEffectType.IncreaseValue))
            ));
        }

        return _modif;
    }

    public List<SCost> GetUpgradeCost()
    {
        List<SCost> _modif = new List<SCost>();

        foreach (SCost current in upgradeCost)
        {
            int amount = Mathf.RoundToInt(current.amount * (1 + upgradeModificator * (currentLevel - 1)));

            _modif.Add(new SCost(
                current.resource,
                amount
            ));
        }

        return _modif;
    }

    public List<SCost> GetBuildCost() => buildCost;

    private List<SCost> GetModifiedCost(List<SCost> costs, float modificator)
    {
        List<SCost> _modif = new List<SCost>();

        foreach (SCost current in costs)
        {
            int amount = Mathf.RoundToInt(current.amount * (1 + modificator * (currentLevel - 1)));

            _modif.Add(new SCost(
                current.resource,
                amount + (int)(amount * GameBalance.current.GetEffect(EEffectType.IncreaseValue))
            ));
        }

        return _modif;
    }

    public List<Transform> Positions() => lookPositions;
    public Vector3 RandomPosition() => lookPositions[Random.Range(0, lookPositions.Count)].position;
}