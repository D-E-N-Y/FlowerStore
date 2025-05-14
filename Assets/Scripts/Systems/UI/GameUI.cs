using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private UI_Resources ui_resources;

    public void Initialize()
    {
        ui_resources.Initialize();
    }
}
