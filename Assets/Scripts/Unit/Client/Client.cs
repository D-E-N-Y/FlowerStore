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
    
    private Store store;
    private CashierOffice cashierOffice;
    private List<SCost> priceItem;

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
            switch (state)// get item and go to cahier
            {
                case EClientState.Idle:
                    movement.MoveTo(goalPosition);
                    state = EClientState.Move;
                    break;

                case EClientState.OnStore:
                    Shelve _shelve = store.GetRandomShelve();

                    movement.MoveTo(_shelve.RandomPosition());
                    while (movement.isMoving) yield return null;

                    yield return new WaitForSeconds(lookingTime);

                    int choice = Random.Range(1, 11);
                    if (choice <= 5)
                    {
                        priceItem = _shelve.GetPrice();
                        cashierOffice = store.GetCashierOffice();
                        cashierOffice.AddToOrder(this);
                    }
                    if (choice == 10)
                    {
                        GoOut();
                    }
                    break;

                case EClientState.OnOrder:
                    while (movement.isMoving) yield return null;

                    if (!cashierOffice.isService)
                        inPlace?.Invoke();       
                    break;
            }

            yield return null;
        }
    }

    public void Pay()
    {
        foreach (SCost price in priceItem)
        {
            ResourceSystem.current.AddResource(price.resource, price.amount);
        } 
        priceItem = null;
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