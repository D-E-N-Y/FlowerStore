using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private ResourceSystem resourceSystem;
    [SerializeField] private ClientsSystem clientsSystem;

    [SerializeField] private Store store;

    [SerializeField] private GameUI gameUI;
    
    private void Start() 
    {
        resourceSystem.Initialize();
        clientsSystem.Initialize();
        
        store.Initialize();

        gameUI.Initialize(store);
    }
}
