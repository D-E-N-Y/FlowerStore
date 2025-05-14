using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientsSystem : MonoBehaviour
{
    public static ClientsSystem current;
    
    [SerializeField] private Client clientPrefab;
    
    [SerializeField] private List<Client> clients;
    [SerializeField] private List<Spawner> spawners;

    [SerializeField, Range(0.1f, 10f)] private float timeSpawner; 
    private Coroutine spawningClients;

    public void Initialize()
    {
        spawners.ForEach(x => x.Initialize());

        spawningClients = StartCoroutine(nameof(SpawningClients));

        current = this;
    }

    private IEnumerator SpawningClients()
    {
        while(true)
        {
            Client _client = null;
            
            if(!clients.Any())
            {
                _client = Instantiate(clientPrefab);
            }
            else
            {
                _client = clients[Random.Range(0, clients.Count)];;
            }
            
            Spawner _spawner = spawners[Random.Range(0, spawners.Count)];

            _client.Initialize(_spawner.GetGoalPosition(), _spawner.GetSpawnPosition());
            
            if(clients.Contains(_client))
            {
                clients.Remove(_client);
            }
            
            yield return new WaitForSeconds(timeSpawner);
        }
    }

    public void AddClient(Client client)
    {
        clients.Add(client);
    }
}