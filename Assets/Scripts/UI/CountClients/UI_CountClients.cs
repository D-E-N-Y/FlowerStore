using TMPro;
using UnityEngine;

public class UI_CountClients : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ui_current;
    [SerializeField] private TextMeshProUGUI ui_max;
    private Store store;

    public void Initialize(Store store)
    {
        this.store = store;
        store.updateData += UpdateData;

        UpdateData();
    }

    private void UpdateData()
    {   
        ui_current.text = store.GetCurrentClients().ToString();
        ui_max.text = store.GetMaxClients().ToString();
    }
}
