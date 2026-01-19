using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public Vector3 startingPosition;
    public SceneField startScene;

    [SerializeField] SceneField defaultScene;
    [SerializeField] SceneField playerScene;

    [SerializeField] List<SceneField> saveRooms = new List<SceneField>();

    void Start()
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

    public void CreateNewPlayerData()
    {
        startScene = defaultScene;

        List<SceneField> scenesToLoad = new List<SceneField>();

        scenesToLoad.Add(startScene);
        scenesToLoad.Add(playerScene);

        List<SceneField> scenesToUnload = new List<SceneField>();

        foreach (SceneField scene in RoomManager.instance.currentLoadedScenes) 
        {
            scenesToUnload.Add(scene);
        }

        RoomManager.instance.LoadNewScenes(scenesToLoad, scenesToUnload, null);
    }

    public bool LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadGame();

        if (data == null)
        {
            return false;
        }

        startingPosition = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        
        foreach (SceneField scene in saveRooms)
        {
            if (scene.SceneName == data.playerScene)
            {
                startScene = scene;
            }
        }

        List<SceneField> scenesToLoad = new List<SceneField>();

        scenesToLoad.Add(startScene);
        scenesToLoad.Add(playerScene);

        List<SceneField> scenesToUnload = new List<SceneField>();

        foreach (SceneField scene in RoomManager.instance.currentLoadedScenes)
        {
            scenesToUnload.Add(scene);
        }

        RoomManager.instance.LoadNewScenes(scenesToLoad, scenesToUnload, null);

        return true;
    }

    public void ResetPlayer()
    {
        List<SceneField> scenesToLoad = new List<SceneField>();

        scenesToLoad.Add(startScene);
        scenesToLoad.Add(playerScene);

        List<SceneField> scenesToUnload = new List<SceneField>();

        foreach (SceneField scene in RoomManager.instance.currentLoadedScenes)
        {
            scenesToUnload.Add(scene);
        }

        RoomManager.instance.LoadNewScenes(scenesToLoad, scenesToUnload, null);
    }
}
