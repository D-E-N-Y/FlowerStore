using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private UI_Resources ui_resources;
    [SerializeField] private UI_CountClients ui_countClients;

    public void Initialize(Store store)
    {
        ui_resources.Initialize();
        ui_countClients.Initialize(store);
    }
}
