using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Client>(out Client client))
        {
            client.Final();
            ClientsSystem.current.AddClient(client);
        }
    }
}
