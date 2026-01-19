using UnityEngine;
using UnityEngine.EventSystems;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject defaultOption;
    [SerializeField] RectTransform cursor;
    RectTransform currentRect;
    GameObject currentSelection;

    SaveStation station;

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

        menu.SetActive(false);
    }

    void Update()
    {
        if (menu.activeInHierarchy)
        {
            if (currentSelection != EventSystem.current.currentSelectedGameObject)
            {
                currentSelection = EventSystem.current.currentSelectedGameObject;

                if (currentSelection != null)
                {
                    currentRect = currentSelection.GetComponent<RectTransform>();
                }
            }

            if (currentRect != null)
            {
                cursor.localPosition = new Vector2(currentRect.localPosition.x - 24f, currentRect.localPosition.y);
            }
        }
        else
        {
            currentSelection = null;
            currentRect = null;
        }
    }

    public void ShowMenu(SaveStation newStation)
    {
        menu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultOption);

        station = newStation;

        GameManager.instance.GameState = GameState.Menu;
        Time.timeScale = 0f;
    }

    void HideMenu()
    {
        menu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        station = null;

        GameManager.instance.GameState = GameState.Play;
        Time.timeScale = 1f;
    }

    public void OnYes()
    {
        if (station != null)
        {
            PlayerData data = new PlayerData(station.transform.position + new Vector3(0f, -0.5f, 0f), station.scene);
            SaveSystem.SaveGame(data);
            PlayerManager.instance.startingPosition = station.transform.position + new Vector3(0f, -0.5f, 0f);
            PlayerManager.instance.startScene = station.scene;
        }

        HideMenu();
    }

    public void OnNo()
    {
        HideMenu();
    }
}
