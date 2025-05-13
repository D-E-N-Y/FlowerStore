using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI current;

    [SerializeField] private UI_Resources ui_resources;

    public void Initialize()
    {
        ui_resources.Initialize();
    }
}
