using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        { 
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}

public enum GameState
{
    Menu,
    Play,
    Pause,
    GameOver
}
