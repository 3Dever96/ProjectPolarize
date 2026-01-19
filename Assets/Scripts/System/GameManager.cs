using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState;

    public static GameManager instance;

    public delegate void GameOverDelegate();
    public GameOverDelegate gameOver;

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

    public void OnGameOver()
    {
        GameState = GameState.GameOver;
        if (gameOver != null)
        {
            gameOver.Invoke();
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
