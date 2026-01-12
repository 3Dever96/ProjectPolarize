using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Rendering")]
    [SerializeField] Image background;
    [SerializeField] TMPro.TMP_Text gameOverText;
    [SerializeField] TMPro.TMP_Text retryText;
    [SerializeField] TMPro.TMP_Text giveUpText;
    [SerializeField] Image cursorImage;

    [Header("Control")]
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] RectTransform cursorTransform;
    [SerializeField] GameObject startSelection;

    [SerializeField] float cursorOffset;

    GameObject currentSelection;
    RectTransform currentTransform;

    [Header("Scenes To Load")]
    [SerializeField] List<SceneField> scenesToLoad;

    private void Update()
    {
        if (GameManager.instance.GameState == GameState.GameOver)
        {
            if (currentSelection != EventSystem.current.currentSelectedGameObject)
            {
                currentSelection = EventSystem.current.currentSelectedGameObject;

                if (currentSelection != null)
                {
                    currentTransform = currentSelection.GetComponent<RectTransform>();
                }
                else
                {
                    currentTransform = null;
                }
            }

            if (currentTransform != null)
            {
                cursorTransform.localPosition = new Vector2(currentTransform.localPosition.x + cursorOffset, cursorTransform.localPosition.y);
            }
        }
    }

    void OnEnable()
    {
        GameManager.instance.gameOver += OnGameOver;
    }

    void OnDisable()
    {
        GameManager.instance.gameOver -= OnGameOver;
    }

    void OnGameOver()
    {
        gameOverMenu.SetActive(true);
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        float a = 0;
        float b = 0;

        background.color = new Color(0, 0, 0, 0);
        gameOverText.color = new Color(0, 0, 0, 0);
        retryText.color = new Color(0, 0, 0, 0);
        giveUpText.color = new Color(0, 0, 0, 0);
        cursorImage.color = new Color(0, 0, 0, 0);

        while (a < 1)
        {
            a += Time.deltaTime;

            background.color = new Color(0, 0, 0, a);
            gameOverText.color = new Color(1, 1, 1, a);

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (b < 1)
        {
            b += Time.deltaTime;

            retryText.color = new Color(1, 1, 1, b);
            giveUpText.color = new Color(1, 1, 1, b);
            cursorImage.color = new Color(1, 1, 1, b);

            yield return null;
        }

        EventSystem.current.SetSelectedGameObject(startSelection);
    }

    void ResetGameOver()
    {
        gameOverMenu.SetActive(false);
        RoomManager.instance.sceneDelegate -= ResetGameOver;
    }

    public void OnQuitGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RoomManager.instance.sceneDelegate += ResetGameOver;

        List<SceneField> oldScenes = new List<SceneField>();

        foreach (SceneField s in RoomManager.instance.currentLoadedScenes)
        {
            oldScenes.Add(s);
        }

        RoomManager.instance.LoadNewScenes(scenesToLoad, oldScenes, null);
    }
}
