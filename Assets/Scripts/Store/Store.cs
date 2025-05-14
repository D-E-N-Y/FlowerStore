using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private List<Shelve> shelves;

    public void Initialize()
    {
        shelves.ForEach(x => x.Initialize());
    }
}
