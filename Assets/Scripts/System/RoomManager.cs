// 1/8/2026 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [SerializeField] Image fadeImage; // Assign a UI Image in the Inspector for fade effect
    [SerializeField] float fadeDuration = 1.0f; // Duration of fade effect
    [SerializeField] List<SceneField> startScenesToLoad;
    public List<SceneField> currentLoadedScenes;

    public delegate void LoadNewSceneDelegate();
    public LoadNewSceneDelegate sceneDelegate;

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

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            LoadNewScenes(startScenesToLoad, new List<SceneField>(), null);
        }
    }

    public void LoadNewScenes(List<SceneField> newScenes, List<SceneField> oldScenes, RoomEdge room)
    {
        StartCoroutine(LoadScenes(newScenes, oldScenes, room));
    }

    IEnumerator LoadScenes(List<SceneField> newScenes, List<SceneField> oldScenes, RoomEdge room)
    {
        Time.timeScale = 0f;
        yield return StartCoroutine(FadeOut());

        List<SceneField> newLoadedScenes = new List<SceneField>();

        foreach (SceneField sceneName in newScenes)
        {
            if (!newLoadedScenes.Contains(sceneName))
            {
                newLoadedScenes.Add(sceneName);
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        if (room != null)
        {
            room.AlignPosition();
        }

        if (sceneDelegate != null)
        {
            sceneDelegate.Invoke();
        }

        if (oldScenes.Count != 0)
        {
            foreach (SceneField sceneName in oldScenes)
            {
                for (var i = currentLoadedScenes.Count - 1; i >= 0; i--)
                {
                    if (currentLoadedScenes[i].SceneName == sceneName)
                    {
                        currentLoadedScenes.RemoveAt(i);
                    }
                }

                AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
                while (!asyncUnload.isDone)
                {
                    yield return null;
                }
            }
        }

        if (newLoadedScenes.Count > 0)
        {
            for (var i = 0; i < newLoadedScenes.Count; i++)
            {
                currentLoadedScenes.Add(newLoadedScenes[i]);
            }
        }

        yield return StartCoroutine(FadeIn());
        Time.timeScale = 1f;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = fadeDuration;
        Color color = fadeImage.color;

        while (elapsedTime > 0f)
        {
            elapsedTime -= Time.unscaledDeltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
