using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour 
{
    private Vector3 goalPosition;
    
    [SerializeField] private List<GameObject> meshes;    
    [SerializeField] private UnitMovement movement;
    
    public void Initialize(Vector3 goalPosition, Vector3 spawnPosition, Vector3 spawnRotate)
    {
        this.goalPosition = goalPosition;
        transform.position = spawnPosition;
        transform.rotation = Quaternion.Euler(spawnRotate);

        RandomMesh();

        gameObject.SetActive(true);
        movement.MoveTo(goalPosition);
    }

    private void RandomMesh()
    {
        meshes.ForEach(x => x.SetActive(false));

        GameObject _mesh = meshes[Random.Range(0, meshes.Count)];
        movement.Initialize(_mesh.GetComponent<Animator>());
        _mesh.SetActive(true);
    }

    public void Final()
    {
        movement.Reset();
        gameObject.SetActive(false);
    }
}