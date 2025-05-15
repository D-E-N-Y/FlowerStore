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
    private int currentClients;

    public void Initialize()
    {
        shelves.ForEach(x => x.Initialize());
        cashierOffice.Initialize();
    }

    public EStoreType Type() => type;
    public Shelve GetRandomShelve()
    {
        List<Shelve> available = shelves.Where(x => x.isBuild).ToList();
        return available[UnityEngine.Random.Range(0, available.Count)];
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
}
