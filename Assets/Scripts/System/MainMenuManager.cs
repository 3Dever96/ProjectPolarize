using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject startSection;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startSection);
        GameManager.instance.GameState = GameState.Menu;
    }

    public void NewGame()
    {
        PlayerManager.instance.CreateNewPlayerData();
        GameManager.instance.GameState = GameState.Play;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void LoadGame()
    {
        if (PlayerManager.instance.LoadPlayerData())
        {
            GameManager.instance.GameState = GameState.Play;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
