using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierOffice : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] private float timeService;
    [SerializeField] private List<Transform> orderPositions;
    private List<Client> orderClients;

    private Coroutine servicing;
    public bool isService { get; private set; }

    public void Initialize()
    {
        orderClients = new List<Client>();
    }

    public void AddToOrder(Client client)
    {
        orderClients.Add(client);
        client.SetState(EClientState.OnOrder);
        client.MoveTo(orderPositions[orderClients.Count - 1].position);

        if(orderClients.Count == 1)
            orderClients[0].inPlace += StartService;
    }

    private void StartService()
    {
        if (servicing != null)
            StopCoroutine(servicing);

        servicing = StartCoroutine(nameof(Servicing));
    }

    private IEnumerator Servicing()
    {
        isService = true;

        yield return new WaitForSeconds(timeService - GameBalance.current.GetEffect(EEffectType.SpeedUpCheckout) * timeService);

        orderClients[0].inPlace -= StartService;

        orderClients[0].Pay();
        orderClients[0].GoOut();
        orderClients.RemoveAt(0);

        isService = false;

        RefreshOrder();
    }

    public void RefreshOrder()
    {
        if (orderClients.Count <= 0) return;

        orderClients[0].inPlace += StartService;
        for (int i = 0; i < orderClients.Count; i++)
        {
            orderClients[i].MoveTo(orderPositions[i].position);
        }
    }
}