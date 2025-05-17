using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance current;

    public EStartGame startGame { get; private set; }

    public void Initialize()
    {
        current = this;
    }

    public void NewGame() => startGame = EStartGame.NewGame;
    public void LoadGame() => startGame = EStartGame.LoadGame;
}