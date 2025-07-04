using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private UI_Resources ui_resources;
    [SerializeField] private UI_CountClients ui_countClients;
    [SerializeField] private ManagerInteractablePanels managerInteractablePanels;
    [SerializeField] private UIProvider uiProvider;

    public void Initialize(Store store)
    {
        ui_resources.Initialize();
        ui_countClients.Initialize(store);
        managerInteractablePanels.Initialize();

        uiProvider.Initialize();
    }
}
