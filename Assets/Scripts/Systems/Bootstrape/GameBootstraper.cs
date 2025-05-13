using UnityEngine;

public class Bootstrape : MonoBehaviour
{
    [SerializeField] private ResourceSystem resourceSystem;
    [SerializeField] private GameUI gameUI;
    
    private void Start() 
    {
        resourceSystem.Initialize();
        
        gameUI.Initialize();
    }
}
