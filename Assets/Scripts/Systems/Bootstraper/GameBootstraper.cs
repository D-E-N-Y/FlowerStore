using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private GameBalance gameBalancel;
    [SerializeField] private ResourceSystem resourceSystem;
    [SerializeField] private InteractionSystem interactionSystem;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private CraftSystem craftSystem;
    [SerializeField] private EquipSystem equipSystem;
    [SerializeField] private ClientsSystem clientsSystem;

    [SerializeField] private Store store;

    [SerializeField] private GameUI gameUI;
    [SerializeField] private TooltipSystem tooltipSystem;

    private void Start()
    {
        gameBalancel.Initiailize();

        resourceSystem.Initialize();
        interactionSystem.Initialize();
        inventorySystem.Initialize();
        craftSystem.Initialize();
        equipSystem.Initialize();
        clientsSystem.Initialize();

        store.Initialize();

        gameUI.Initialize(store);
        tooltipSystem.Initialize();
    }
}
