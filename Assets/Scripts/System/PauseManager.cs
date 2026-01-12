using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject firstSelection;
    [SerializeField] RectTransform cursor;
    GameObject current;
    RectTransform currentTransform;
    bool canPause;

    void Update()
    {
        if (GameManager.instance.GameState == GameState.Play)
        {
            if (InputManager.instance.Pause && canPause)
            {
                PauseGame();
            }

            if (!InputManager.instance.Pause && !canPause)
            {
                canPause = true;
            }
        }
        else if (GameManager.instance.GameState == GameState.Pause)
        {
            if (current != EventSystem.current.currentSelectedGameObject)
            {
                current = EventSystem.current.currentSelectedGameObject;
                currentTransform = current.GetComponent<RectTransform>();
            }

            if (currentTransform != null)
            {
                cursor.localPosition = new Vector2(cursor.localPosition.x, currentTransform.localPosition.y);
            }

            if (InputManager.instance.Pause && canPause)
            {
                UnpauseGame();
            }

            if (!InputManager.instance.Pause && !canPause)
            {
                canPause = true;
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
        GameManager.instance.GameState = GameState.Pause;
        canPause = false;
        EventSystem.current.SetSelectedGameObject(firstSelection);

        current = null;
        currentTransform = null;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        GameManager.instance.GameState = GameState.Play;
        canPause = false;
        EventSystem.current.SetSelectedGameObject(null);

        current = null;
        currentTransform = null;
    }

    public void QuitGame()
    {
        
    }
}
