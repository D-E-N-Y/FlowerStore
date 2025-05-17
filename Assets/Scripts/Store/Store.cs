using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Store : MonoBehaviour
{
    public Action updateData;

    [SerializeField] private EStoreType type;
    [SerializeField] private List<Shelve> shelves;
    [SerializeField] private CashierOffice cashierOffice;
    [SerializeField, Range(1, 10)] private int maxClients;
    private int originMaxClient;
    private int currentClients;

    public void Initialize()
    {
        originMaxClient = maxClients;

        shelves.ForEach(x => x.Initialize());
        cashierOffice.Initialize();

        GameBalance.current.updateData += SetMaxClients;
    }

    public EStoreType Type() => type;
    public Shelve GetRandomShelve()
    {
        List<Shelve> available = shelves.Where(x => x.isBuild).ToList();

        if (available.Count > 0)
        {
            return available[UnityEngine.Random.Range(0, available.Count)];
        }
        else
        {
            return null;
        }
        
    }

    public CashierOffice GetCashierOffice() => cashierOffice;

    public void EnterClient()
    {
        currentClients++;
        currentClients = Math.Min(maxClients, currentClients);

        updateData?.Invoke();
    }

    public void OutClient()
    {
        currentClients--;
        currentClients = Math.Max(0, currentClients);

        updateData?.Invoke();
    }

    public bool CanEnterClient() => currentClients < maxClients;
    public int GetCurrentClients() => currentClients;
    public int GetMaxClients() => maxClients;

    private void SetMaxClients()
    {
        int boost = (int)GameBalance.current.GetEffect(EEffectType.IncreaseClientSlots);

        if (boost == 0)
        {
            maxClients = originMaxClient;
        }
        else
        {
            maxClients = originMaxClient + boost;
        }

        updateData?.Invoke();
    }
}
