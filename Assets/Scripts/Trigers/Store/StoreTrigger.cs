using UnityEngine;

public class StoreTrigger : MonoBehaviour
{
    [SerializeField] private Store store;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Client>(out Client client))
        {
            if (client.state == EClientState.OutStore)
            {
                client.SetState(EClientState.Move);
                store.OutClient();

                return;
            }

            if (!store.CanEnterClient()) return;

            int choice = Random.Range(1, 10);
            if (choice > 4) return;

            store.EnterClient();
            client.SetStore(store);
            client.SetState(EClientState.OnStore); 
        }
    }
}
