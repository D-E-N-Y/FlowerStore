using UnityEngine;

public class StoreTrigger : MonoBehaviour
{
    [SerializeField] private Store store;
    [SerializeField, Range(1, 100)] private float changeEnterStore;

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

            if (!store.CanEnterClient() || !store.GetRandomShelve()) return;

            int choice = Random.Range(1, 100);
            if (choice > changeEnterStore + GameBalance.current.GetEffect(EEffectType.IncreaseEnterChance)) return;

            store.EnterClient();
            client.SetStore(store);
            client.SetState(EClientState.OnStore);
        }
    }
}
