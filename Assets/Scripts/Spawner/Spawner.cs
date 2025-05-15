using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour 
{
    [SerializeField] private BoxCollider goal;
    [SerializeField] private Vector3 startRotate;
    private BoxCollider boxCollider;

    public void Initialize()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    public Vector3 GetStartRotate() => startRotate;
    
    public Vector3 GetGoalPosition()
    {
        return GetRandomPositionOnCollider(goal);
    }
    
    public Vector3 GetSpawnPosition()
    {
        return GetRandomPositionOnCollider(boxCollider);
    }

    private Vector3 GetRandomPositionOnCollider(BoxCollider collider)
    {
        Bounds bounds = collider.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = 0f;
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}