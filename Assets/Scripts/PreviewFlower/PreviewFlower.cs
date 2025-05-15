using System.Collections.Generic;
using UnityEngine;

public class PreviewFlower : MonoBehaviour
{
    [SerializeField] private EPreviewFlowerType type;
    [SerializeField] private List<SCost> price;
    
    [SerializeField] private List<MeshFilter> potVariants;
    [SerializeField] private List<MeshFilter> flowerVariants;

    [SerializeField] private MeshFilter pot;
    [SerializeField] private MeshFilter flower;

    public void Initialize()
    {
        pot.mesh = GetRandomMesh(potVariants);
        flower.mesh = GetRandomMesh(flowerVariants);
    }

    private Mesh GetRandomMesh(List<MeshFilter> meshes)
    {
        int index = Random.Range(0, meshes.Count);
        return meshes[index].sharedMesh;
    }

    public EPreviewFlowerType Type() => type;
    public List<SCost> GetPrice() => price;
}
