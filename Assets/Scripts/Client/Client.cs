using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Client : MonoBehaviour 
{
    private Vector3 goalPosition;
    private NavMeshAgent agent;
    
    public void Initialize(Vector3 goalPosition, Vector3 spawnPosition)
    {
        this.goalPosition = goalPosition;
        transform.position = spawnPosition;
        agent = GetComponent<NavMeshAgent>();

        RandomFront();

        gameObject.SetActive(true);

        agent.SetDestination(goalPosition);
    }

    private void RandomFront()
    {

    }

    public void ContinueMainPath()
    {

    }

    public void Final()
    {
        agent.ResetPath();
        gameObject.SetActive(false);
    }
}