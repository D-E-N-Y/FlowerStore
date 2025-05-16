using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour 
{
    public System.Action inPlace;

    private Vector3 goalPosition;
    public EClientState state { get; private set; }
    
    [SerializeField] private List<GameObject> meshes;    
    [SerializeField] private UnitMovement movement;

    [SerializeField, Range(1, 10)] private float lookingTime;
    [SerializeField, Range(1, 100)] private float changePay;
    [SerializeField, Range(1, 100)] private float changeOut;

    [SerializeField, Range(1, 100)] private float changeGetTips;
    [SerializeField] private DropedItem pelt;

    private Store store;
    private Shelve shelve;
    private CashierOffice cashierOffice;

    private Coroutine live;

    public void Initialize(Vector3 goalPosition, Vector3 spawnPosition, Vector3 spawnRotate)
    {
        this.goalPosition = goalPosition;
        transform.position = spawnPosition;
        transform.rotation = Quaternion.Euler(spawnRotate);

        RandomMesh();

        gameObject.SetActive(true);

        state = EClientState.Idle;
        live = StartCoroutine(nameof(Live));
    }

    private void RandomMesh()
    {
        meshes.ForEach(x => x.SetActive(false));

        GameObject _mesh = meshes[Random.Range(0, meshes.Count)];
        movement.Initialize(_mesh.GetComponent<Animator>());
        _mesh.SetActive(true);
    }

    private IEnumerator Live()
    {
        while (true)
        {
            switch (state)
            {
                case EClientState.Idle:
                    yield return Idle();
                    break;

                case EClientState.OnStore:
                    yield return OnStore();
                    break;

                case EClientState.OnOrder:
                    yield return OnOrder();
                    break;
            }

            yield return null;
        }
    }

    private IEnumerator Idle()
    {
        movement.MoveTo(goalPosition);
        state = EClientState.Move;

        yield return null;
    }

    private IEnumerator OnStore()
    {
        shelve = store.GetRandomShelve();

        movement.MoveTo(shelve.RandomPosition());
        while (movement.isMoving) yield return null;

        yield return new WaitForSeconds(lookingTime);

        float _changePay = changePay + GameBalance.current.GetEffect(EEffectType.IncreaseBuyChance) * changePay;
        float _changeOut = changeOut - GameBalance.current.GetEffect(EEffectType.DecreaseLeaveChance) * changeOut + _changePay;

        int choice = Random.Range(1, (int)_changeOut);
        if (choice <= changePay)
        {
            cashierOffice = store.GetCashierOffice();
            cashierOffice.AddToOrder(this);
        }
        else if (choice <= _changeOut)
        {
            GoOut();
        }
    }

    private IEnumerator OnOrder()
    {
        while (movement.isMoving) yield return null;

        if (!cashierOffice.isService)
            inPlace?.Invoke();       
    }

    public void Pay()
    {
        foreach (SCost price in shelve.GetPrice())
        {
            ResourceSystem.current.AddResource(price.resource, price.amount);
        }
        shelve = null;

        // get tips
        int choice = Random.Range(1, 100);
        if (choice <= changeGetTips + GameBalance.current.GetEffect(EEffectType.IncreaseTipChance) * changeGetTips)
        {
            Vector3 _position = new Vector3(
                Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f),
                Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f),
                Random.Range(transform.position.z - 0.5f, transform.position.z + 0.5f)
            );
            Instantiate(pelt, _position, Quaternion.identity).Initialize();
        }
    }

    public void SetState(EClientState state) => this.state = state;
    public void SetStore(Store store) => this.store = store;

    public void MoveTo(Vector3 position)
    {
        movement.MoveTo(position);
    }

    public void GoOut()
    {
        state = EClientState.OutStore;
        movement.MoveTo(goalPosition);
    }

    public void Final()
    {
        movement.Reset();
        StopCoroutine(live);
        gameObject.SetActive(false);
    }
}