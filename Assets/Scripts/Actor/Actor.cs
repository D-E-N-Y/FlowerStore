using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private string nameActor;

    private string selectLayer;
    private string defaultLayer;

    public virtual void Initialize()
    {
        selectLayer = "SelectedActor";
        defaultLayer = "Interactable";
    }

    public virtual void Interaction() => SetLayerRecursively(gameObject, selectLayer);
    public virtual void DisInteraction() => SetLayerRecursively(gameObject, defaultLayer);
    void SetLayerRecursively(GameObject obj, string newLayer)
    {
        obj.layer = LayerMask.NameToLayer(newLayer);

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public string GetName() => nameActor;
}
