using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;

    public string GetName() => itemName;
    public string GetDescription() => description;
    public Sprite GetSprite() => sprite;
}