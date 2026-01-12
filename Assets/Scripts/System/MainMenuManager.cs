using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] List<SceneField> roomsToLoad;
    [SerializeField] List<SceneField> roomsToUnload;

    [SerializeField] GameObject startSection;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startSection);
        GameManager.instance.GameState = GameState.Menu;
    }

    public void NewGame()
    {
        GameManager.instance.GameState = GameState.Play;
        EventSystem.current.SetSelectedGameObject(null);
        RoomManager.instance.LoadNewScenes(roomsToLoad, roomsToUnload, null);
    }

    public void LoadGame()
    {

    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
